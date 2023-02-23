using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float fireInterval = 0.5f;
    public float speed = 10.0f;  
    public GameObject Bullet;    //퍼블릭으로 게임오브젝트라는 변수 선언, Bullet이라는 변수 생성

    Transform fireTransform;     // Transform변수 선언 fireTransform 변수 생성
    private GameObject fireFlash;


    Animator anim;

    //입력철용 InputAction
    PlayerInputActions inputActions; 
    Vector3 inputDir = Vector3.zero;

    /// 연사용 코루틴을 저장할 변수
    IEnumerator fireCoroutine;

    //플레이어 점수
    int score = 0;

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
        inputActions = new PlayerInputActions();
        fireTransform = transform.GetChild(0);  //fireTransform이라는 변수가 자식넘버가 0이다. 즉, 1번째 자식.
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
        inputActions.Player.Bomb.performed += OnBomb;
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
       

    }

    



    //이겜임 오브젝트가 비활성화 될 때 실행되는 함수 
    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Bomb.performed -= OnBomb;
        inputActions.Player.Fire.canceled -= OnFireStop;
        inputActions.Player.Fire.performed -= OnFireStart;
        inputActions.Player.Disable();
    }



    //업데이트가 실행하기 전. 시작할 때 실행되는 함수 
    void Start()
    {
       // Debug.Log("Start");
    }

    //매 프레임마다 계속 호출되는 함수()
    void Update()
    {

        //transform.position += Time.deltaTime *speed *inputDir; // 곱하기 4번 
        transform.Translate(Time.deltaTime *speed *inputDir); 
                                                               //초당 Speed의 속도로 inputDir방향으로 이동
        //Time.deltaTime : 이전 프레임에서 현재 프레임까지의 시간
        
        //30프레임 deltaTime = 1/30초 = 0.33
        //120프레임 

        //inputDir(1,2,0) 
        //inputDir *0.5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"충돌영역에서 들어감 : 충돌 대상 : {collision.gameObject.name}");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log($"충돌영역에서 나감 : {collision.gameObject.name}");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("충돌영역에 접촉해 있으면서 움직이는 것");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"트리거 안에 들어감 : 대상 트리거 : {collision.gameObject.name}");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       // Debug.Log($"트리거에서 나감 : 대상 트리거 : {collision.gameObject.name}");
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("트리거 안에서 움직임");
    //}

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
            
            GameObject obj = Instantiate(Bullet);                 //총알생성
            obj.transform.position = fireTransform.position;
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
  
    private void OnBomb(InputAction.CallbackContext context)
    {
    // Debug.Log("Bomb");
    }

   
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();               //결정되는곳
        anim.SetFloat("InputY",dir.y);                           // 애니메이터에 있는 InputY 파라메터에 dir.y값을 준다. 
        //Debug.Log (dir);
        inputDir = dir;
        
    }

    //Score에 점수를 추가하는 함수
    //plus는 추가할 점수
    public void AddScore(int plus)
    {
        Score += plus;
    }
    

}


    







