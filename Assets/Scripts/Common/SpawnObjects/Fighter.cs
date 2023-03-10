using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Fighter : EnemyBase
{

    [Header("적 데이터----------------0")]

    // public int score = 10; //점수가 있는 적
    //public float speed = 1.0f;     // 적 이속
   
    [Range(0.1f, 3.0f)] //슬라이더 생성// 변수 범위를 MIN MAX사이로 변경 가능
    public float amplitude = 1.0f; // 사인 결과 값을 증폭시킬 변수(위아래 차이 결정)
    public float frequency = 1.0f; //사인 그래프가 한번 도는 데 걸리는 시간

    // 시간 누적이 필요하다 
    float timeElapsed = 0.0f;
    //처음 등장위치변수 설정
    float baseY;

    //살아있는지 여부를 나타네는 플래그(Flag), true면 살아있고 false면 죽음
    //bool isAlive = true;

    //public PoolObjectType explosionType; //이팩트
    //플레이러 참조
    //Player player = null;
    //player에 처음 한번만 값을 설정 가능한 프로퍼티. 쓰기 전용.
    //public Player TargetPlayer
   // {
   //     set
   //     {
   //         if (player == null)
    //        {
   //             player = value;
   //         }
   //     }
   // }

    //적이 위아래로 움직이는 기준위치
    public float BaseY
    {
        set 
        {   
            //적이 위아래로 움직이는 기준위치 설정하는 프로퍼티(월드기준, 쓰기전용)
            baseY = value;
            //Vector3 newPos =transform.position;
            //newPos.y = baseY;
            //transform.position = newPos;
        } 
        
        
    }
    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable(); //부모클래스 

        transform.localPosition = Vector3.zero;
        baseY = 0.0f; //월드 좌표
        //transform.localposition.y 로컬좌표
        //isAlive = true;
    }

    
    void Update()
    {
        // 시간 누적 문단// frequency에 비례해서 시간 증가가 빠르게 된다.
        timeElapsed += Time.deltaTime* frequency; 
       
        //x는 현재 위치에서 약간 왼쪽으로 이동//tansform.position.x에서 - speed *Tim.deltaTime을 해주니 왼쪽 +로 변경 시 오른쪽.
        float x = transform.position.x - moveSpeed * Time.deltaTime;
       
        // y는 시작위치에서 sin 결과값만큼 변경
        float y = baseY + Mathf.Sin(timeElapsed)*amplitude;

        //합쳐서 움직이는 코드
        transform.position = new Vector3(x, y, 0); 
    }


    //collsion 충돌정보
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        
    //    if(collision.gameObject.CompareTag("Bullet"))       //충돌 했을 때 실행
    //    {
            
    //      // Debug.Log($"적은 {collision.gameObject.name}"); //문구출력 
     //       Die();
         
     //   }
        
 //   }

   // void Die()
   // {
        //1.//GameObject Plyer = GameObject.Find("Player"); //이름 찾기 하지만 문자로 찾는다. 그러므로 
        //2.//GameObject Plyer = GameObject.FindGameObjectWithTag("Player");    //태그로 찾기
        //3.//Player player = FindObjectOfType<Player>();                       //타임으로 찾기
        //ㄴ2,3번 둘 다 Scene 전체 다 찾아봐서 무겁다
      
     //   if(isAlive)
     //   {
        //    isAlive = false;
        //    player.AddScore(score);
        

        //    GameObject obj = Factory.Inst.GetObject(explosionType);      // 오브젝트 생성하고 obj변수에 저장
        //    obj.transform.position = transform.position;    // 적의 위치로 설정 
            
        //    gameObject.SetActive(false);                    // 적삭제
       
        //}
        
        
   // }

}
