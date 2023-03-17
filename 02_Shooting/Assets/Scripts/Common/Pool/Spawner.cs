using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;




//게임 오브젝트를 주기적으로 생성할 클래스
public class Spawner : MonoBehaviour
{
    //생성할 게임 오브젝트 : 주체
    // public GameObject spawnPrefab;
    //생성할 오브젝트 타입
    public PoolObjectType objectType;

    //생성할 위치
    public float MinY = -4;     //(최솟값)MinY 거의 사용 할 일이 없다.
    public float MaxY = 4;      //(최댓값)
    //WaitForSeconds wait;      //wait 선언


    //게임내의 플레이어에 대한 참조
    protected Player player = null;

    //시간 간격
    public float interval = 1.0f;
    private void Start()
    {
        //wait = new WaitForSeconds(interval); //게임 실행 도중에 interval이 변핮 않는다면 미리 만들어 두는것도 나쁘지않다.
        
        player = FindObjectOfType<Player>(); // 플레이어 미리 찾아 놓는다.

        //시작할 때 Spawn 코루틴 시작
         StartCoroutine(Spawn());
        //Destroy(spawnPrefab , 4.0f);
    }




    //새로운 유니티 코드 
    //문자열하고 정수 비교는 안된다. 무조건 정수비교가 유리하다 그래서 숫자로 비교를 해야한다.
    //IEnumertor : 열거자를 생성한다. // 검색
    //yield return;
    //오브젝트를 주기적으로 생성하는 코루틴
    virtual protected IEnumerator Spawn() 
    {
        while(true)
        {
        
            yield return new WaitForSeconds(interval);       //인버털만큼 대기
        
            GameObject obj = Factory.Inst.GetObject(objectType);
            //obj.transform.position += transform.position;   
            //folat r에 Random Y값을 적용시킨다. //랜덤하게 높이 구함
           
            EnemyBase enemy = obj.GetComponent<EnemyBase>();              //생성한 게임오브젝트에서 Enemy 컴포넌트 가져오기 
            enemy.TargetPlayer = player;                                 //Enemy에 플레이어 설정

            enemy.transform.position = transform.position;

            OnSpawn(enemy);



        }
     
    }

    protected virtual void OnSpawn(EnemyBase enemy)
    {

        float r = Random.Range(MinY, MaxY);             // 랜덤하게 적용할 기준 높이 구하고
        enemy.transform.Translate(Vector3.up * r);      // 랜덤하게 높이 적용하기

        //yield return wait;
        

    }






    // secen 창에 개발용 정보를 그리는 함수
    //protected virtual void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    // Gizmos.color = new Color(0, 1, 0); // rgb값으로 색상을 만들 수도 있다

    //    // 스폰 영역을 큐브로 그리기
    //    Gizmos.DrawWireCube(transform.position,
    //            new Vector3(1, Mathf.Abs(MaxY) + Mathf.Abs(MinY) + 2, 1));


    //}


    //선택 안할 시 Scene에서 안보임.
    virtual protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.color = new Color(); // RGB로 내가 원하는 색도 정할 수 있다

        //스폰지점을 선으로 긋기
        Vector3 from = transform.position + Vector3.up * MinY;
        Vector3 to = transform.position + Vector3.down * MaxY;

        //Gizmos.DrawLine(from, to);

        Gizmos.DrawWireCube(transform.position, new Vector3(1, Mathf.Abs(MaxY) + Mathf.Abs(MinY) + 2, 1)); // MaxY +(-MinY) 
                                                                                                           //+2
    }


}
