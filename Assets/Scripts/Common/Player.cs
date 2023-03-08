using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //-------------------------------------------------------------------------------------------
    [Header("플레이어 데이터")]
    //player 속도
    public float speed = 10.0f;

    public float invincibleTime = 2.0f;

    //무적 상태인지 아닌지 
    private bool isInvincibleMode = false;

    // 무적일 때 시간 누적용(cos에서 사용할 용도)
    private float timeElapsed = 0.0f;
    /// <summary>
    /// 시작할 때 생명 
    /// </summary>
    public int initialLife = 3;
    //현재 생명
    private int life = 3;
    //사망
    private bool isDead = false;
    

    private int Life
    {
        get => life;
        set
        {
            if(!isDead)
            {
                if (life > value)
                {
                    //라이프가 감소한 상황이면
                    OnHit();    //맞았을 때의 동작이 있는 함수 실행
                    
                }
                life= value; //value라는것은 프로퍼티에 넣은 값
                if(life <= 0)
                {
                    
                    OnDie();    //죽었을 때의 동작이 있는 함수 실행
                }
                onLifeChange?.Invoke(life);
            }
        }
    }

   

    public Action<int> onLifeChange;


    //퍼블릭으로 게임오브젝트라는 변수 선언, Bullet이라는 변수 생성
    public PoolObjectType bulletType;

    //플레이어 점수
    int score = 0;

    //현재 파워
    public int power = 0;
    

    //파워가 최대치일 때 파워업 아잍메 먹으며ㅓㄴ 보너스
    int extraPowerBonus = 300;
    //--------------------------------------------------------------------------------------------
    float fireAngle = 30.0f;
    [Header("총알 데이터")]
    //총알 연사간격시간                         // Transform클래스 선언 fireTransform 변수 생성
    public float fireInterval = 0.5f;          
   
    private GameObject fireFlash;              // GameObject클래스 선언 fireFlash를 생성

    // 연사용 코루틴을 저장할 변수
    IEnumerator fireCoroutine;

    [Header("컴포넌트")]
    private Rigidbody2D rigid;
    //private Transform fireRoot;

    private SpriteRenderer spriteRenderer;

    private Transform [] fireTransforms;

    Animator anim;                             

    //입력철용 InputAction
    PlayerInputActions inputActions;           //PlayerInputActions 클래스 선언 inputActions 변수할당 
    Vector3 inputDir = Vector3.zero;           //Vector3클래스에 inputDir 은 0,0,0 좌표를 얻었다.

    
    
    

    //파워를 증감시키기 위한 프로퍼티 (설정시 추가처리 잇음)
    int Power
    {
        get => power;
        set
        {
            power= value;
            if (power > 3) //3을 넘어가면 
            {
                AddScore(extraPowerBonus); //보너스 추가
                power = Mathf.Clamp(power, 1, 3); //1~3사이로 설정되게 Clmp처리
            }
            RefreshFirePostions(power);         //FireTransform의 위치와 회전 처리 
        }

    }

   

    

    //델리게이트 //delegate : 신호를 보내는 것, 함수를 등록할 수 있다
    //점수가 변경되면 실행될 델리게이트 . 파라메터가 int 하나이고 리턴타입이 void인 함수를 등록할 수 있다.
    public Action<int> onScoreChange;


    //property : 값을 넣거나 읽을 때 추가적으로 할일이 많을 때 사용한다.
    //플레이어의 점수를 확인할 수 있다.
    public int Score
    {
        //get   // 특정 값을 다른 곳에서  확인할 때 사용될 수 있다.//
        //set   //특정 값을 다른 곳에서 설정할 때 사용됨
        //{   
        //    return score;
        //}

        get => score; //주석한 코드를 요약한거

        private set  //그 값이 value에 들어가고 그것이 score에 저장된다.
        {
           
                score = value;
            
                             // score = score + value
            //if(onScoreChage != null)
            //{
            //    onScoreChange?.Invoke(score);
            //}
            onScoreChange?.Invoke(score); //4줄을 요약한거.
            Debug.Log($"점수 : {Score}");
        }
        
        //앞에 private을 붙이면 자신만 사용가능하다.
    }
    

    //이 게임 오브젝트가 생성완료 되었을 때 실행되는 함수
    private void Awake()
    {
        anim = GetComponent<Animator>();        //GetComponent는 성능문제가 있기 때문에 한번 만 찾도록 코드를 짠다.
                                                //Update함수에 절대 넣으면 안된다.
        spriteRenderer = GetComponent<SpriteRenderer>();   
        rigid = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions();
        Transform fireRoot = transform.GetChild(0);  //fireTransform이라는 변수가 자식넘버가 0이다. 즉, 1번째 자식.
        fireTransforms = new Transform[fireRoot.childCount];
        for(int i = 0; i < fireRoot.childCount; i++)
        {
            fireTransforms[i] = fireRoot.GetChild(i);
        }

        fireFlash = transform.GetChild(1).gameObject;

        fireFlash.SetActive(false);


        fireCoroutine = FireCoroutine();
        //transform.Find("FireTransform");      //동일하나 추천하지 않는다.
    }



    //이 게임 오브젝트가 완성된 이후에 활성화 할 때 실행되는 함수
    private void OnEnable()
    {
        inputActions.Player.Enable();
        //inputActions.Player.Fire.started();//버튼을 누른 직후
        //inputActions.Player.Fire.performed();//버튼을 충분히 눌렀을 때
        //inputActions.Player.Fire.canceled();// 버튼을 땐 직후

        inputActions.Player.Fire.performed += OnFireStart;
        inputActions.Player.Fire.canceled += OnFireStop;
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
       
        //inputActions.Player.Bomb.performed += OnBomb;

    }

    



    //이겜임 오브젝트가 비활성화 될 때 실행되는 함수 
    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Fire.canceled -= OnFireStop;
        inputActions.Player.Fire.performed -= OnFireStart;
        inputActions.Player.Disable();
        //inputActions.Player.Bomb.performed -= OnBomb;
    }



    //업데이트가 실행하기 전. 시작할 때 실행되는 함수 
    void Start()
    {
        power = 1; //power는 1로 시작 

        life = initialLife;
       // Debug.Log("Start");
    }

    //매 프레임마다 계속 호출되는 함수()
    void Update()
    {
        if (isInvincibleMode)
        {
            timeElapsed += Time.deltaTime*30;  //초당으로 처리하면 한번 깜박이는데 3.141592... 초가 필요하다
            float alpha = (MathF.Cos(timeElapsed)+1.0f) * 0.5f; //cos결과를 101 로 변경
            spriteRenderer.color = new Color(1,1,1,alpha);

        }


    //    transform.position += Time.deltaTime *speed *inputDir; // 곱하기 4번 
    //    transform.Translate(Time.deltaTime *speed *inputDir); 
    //                                                           초당 Speed의 속도로 inputDir방향으로 이동
    //    Time.deltaTime : 이전 프레임에서 현재 프레임까지의 시간

    //    30프레임 deltaTime = 1/30초 = 0.33
    //    120프레임 

    //    inputDir(1,2,0) 
    //    inputDir *0.5;

    //    여기는 3번실행
    }











    private void FixedUpdate()
    {
        //항상 일정한 시간 간격으로 실행되는 업데이트
        //물리 연산이 들어가는 
        //Debug.Log(Time.fixedDeltaTime);
        
       //rigid.MovePosition(); // 특정 위치로 이동시키기 .
                               // 움직일 때 물리적으로 막히면 거기서부터는 진행하지 안흐는다.
                               // 관성이 없는 움직임을 시킬 때 유영


        //rigid.AddForce();
        //특정  방향을 힘을 가하는 것
        //관성이 있다.
        //움직일 때 물리적으로 막히면 거기서부터는 진행을 하지 않는다.

        rigid.MovePosition(transform.position + Time.fixedDeltaTime * speed * inputDir);    
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"충돌영역에서 들어감 : 충돌 대상 : {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Life--;
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            Power++;
            collision.gameObject.SetActive(false);
        }
    }
   
    
    private void OnFireStart(InputAction.CallbackContext context)
    {
        StartCoroutine(fireCoroutine);
        // Debug.Log("Fire");
    }

    private void OnFireStop(InputAction.CallbackContext context)
    {
        StopCoroutine(fireCoroutine);
    }

    //코루틴 함수 
    IEnumerator FireCoroutine()
    {
        while(true)
        {
            //obj.transform.position = fireTransform.position;
            for(int i = 0; i<power; i++)
            {

               GameObject obj = Factory.Inst.GetObject(bulletType);                 //총알생성
               Transform firePos = fireTransforms[i];
               obj.transform.position = firePos.position;
               obj.transform.rotation = firePos.rotation;
            }
            StartCoroutine(FlashEffect());
            yield return new WaitForSeconds(fireInterval);       //연사간격만큼 대기
        }
    }
    IEnumerator FlashEffect()
    {
        fireFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fireFlash.SetActive(false);
    }
  

   
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();               //결정되는곳
        anim.SetFloat("InputY",dir.y);                           // 애니메이터에 있는 InputY 파라메터에 dir.y값을 준다. 
        //Debug.Log (dir);
        inputDir = dir;
        
    }

    //맞았을 때 실행되는 함수
    private void OnHit()
    {
        Power--;                                //파워1줄이고
        StartCoroutine(EnsterInvincibleMode());   //코루틴 실행
    }
    
    IEnumerator EnsterInvincibleMode()
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        isInvincibleMode = true;
        timeElapsed = 0.0f;
        
        yield return new WaitForSeconds(invincibleTime); // invincibleTime만큼 기다리기

        spriteRenderer.color = Color.white;             //색이 변한 상태에서 무적모드가 끝날 때를 대비해서 색상도 초기화
        isInvincibleMode = false;                       // 무적모드 끝났다고 표시
        gameObject.layer = LayerMask.NameToLayer("Player"); //레이어 되돌리기



    }



    private void OnDie()
    {
        isDead= true;
        life = 0;
    }

    //Score에 점수를 추가하는 함수
    //plus는 추가할 점수
    public void AddScore(int plus)
    {
        Score += plus;
    }

    /// <summary>
    /// 파워에 따라 발사위치 조정하는 함수
    /// </summary>
    /// <param name="power"></param>
    private void RefreshFirePostions(int power)
    {
        //기존fireRoot에 자식 비활성화 하기

        //fireRoot.childCount;
        for(int i =0; i< fireTransforms.Length; i++)
        {
            fireTransforms[i].gameObject.SetActive(false);
        }

       //fireRoot에 power 숫자에 맞게 자식 활성화
       for(int i=0; i<power; i++)
        {
            //파워1 :  0 오른쪽 벡터 0도만큼 회전
            //파워2 : -15 15 1)오른쪽 벡터를 fireAngle * -0.5 2) 오른쪽 벡터를 fireAngle *0.5
            //파워3 : -30 0 30 1)오른쪽 벡터 fireAngle*-1 2)오른쪽 벡터 fireAngle*0 3)오른쪽 벡터 fireAngle*1 

            Transform firePos = fireTransforms[i];
            firePos.localPosition = Vector3.zero;
            firePos.rotation = Quaternion.Euler(0,0,(power-1)*(fireAngle*0.5f) + i * -fireAngle);
            firePos.Translate(1, 0, 0);

            

            fireTransforms[i].gameObject.SetActive(true) ;
        }
    }






    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    //Debug.Log($"충돌영역에서 나감 : {collision.gameObject.name}");
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    //Debug.Log("충돌영역에 접촉해 있으면서 움직이는 것");
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Debug.Log($"트리거 안에 들어감 : 대상 트리거 : {collision.gameObject.name}");
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    // Debug.Log($"트리거에서 나감 : 대상 트리거 : {collision.gameObject.name}");
    //}

    ////private void OnTriggerStay2D(Collider2D collision)
    ////{
    ////    Debug.Log("트리거 안에서 움직임");
    ////}
    //private void OnBomb(InputAction.CallbackContext context)
    //{
    //// Debug.Log("Bomb");
    //}
}










