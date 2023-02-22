using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{   //총알 속도
    public float speed = 10.0f;
    //명중 이팩트
    public GameObject Hitprefab;
    
    Transform HitTransform;
    private void Start()
    {
        Destroy(gameObject, 5.0f ); // this 자기자신 Bullet스크립트를 뜻함 5초 뒤에 스크립트가 들어있는 게임오브젝트를 삭제해라
    }

    //매 프레임마다 계속 호출되는 함수()
    private void Update()
    {
        //Vector2 dir = new Vector2(1, 0);
        //dir = new(1, 0);

        //초당 speed의 속도로 오른쪽 방향으로 이동(로컬 좌표를 기준으로 한 방향)
        //transform.Translate(Time.deltaTime * speed * Vector2.right); // ,Space.Self안 적을 시 자동으로 적용됨. world도있음
        //transform.Translate(Time.deltaTime * speed * transform.right.Space.world);
        
        //transform.position += Time.deltaTime * speed * Vector3.right;

        transform.position += Time.deltaTime * speed * transform.right; //transform위치기반변수? // position은 Vector3이고 
       
        //local좌표와 world좌표
        //local좌표 : 각 오브젝트 별 기준으로 한 좌표계
        //World좌표 : 맵을 기준으로 한 좌표계

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.CompareTag == "Enemy") 매우매우 비효율적이다 아래코드가 Unity가 최적화 잘되게 만듬
        if (collision.gameObject.CompareTag("Enemy"))//만약 태그가 Enemy인 게임 오브젝트랑 부딪치면 실행
        {
            
            Debug.Log($"총알이 {collision.gameObject.name}에 충돌 되었다");
            GameObject obj = Instantiate(Hitprefab);                  
            obj.transform.position = collision.contacts[0].point;//collision.contacts[0].point;  //collision은 모든 충돌을 정보를 가지고 있다.
                                                                 //contacts[0]는 가장 먼저(0번째) 접촉한 곳//point좌표?

            // transform.position;
            //이거는Hit이팩트가 충돌했을 때 총알위치에서 나오게되는데
            //위에있는 collision.contacts는 충돌한 지점에서 이팩트가 생성하게되는것이다. 

            Destroy(gameObject);          

            

        }


    }



}