using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerAsteroid : Spawner
{

    /// <summary>
    /// 목적지 영역
    /// </summary>
    Transform destination;

    
    private void Awake()
    {
        destination = transform.GetChild(0);
    }


    override protected IEnumerator Spawn()
    {
        while (true)
        {
            //생성하고 생성한 오브젝트를 스포너의 자식으로 만들기
            // GameObject obj = Instantiate(spawnPrefab, transform);            // 밑에 있는 코드를 ,transform

            GameObject obj = Factory.Inst.GetObject(PoolObjectType.Asteroid);
            //obj.transform.position += transform.position;   
            //folat r에 Random Y값을 적용시킨다. //랜덤하게 높이 구함
            Asteroid asteroid = obj.GetComponent<Asteroid>();              //생성한 게임오브젝트에서 Enemy 컴포넌트 가져오기 
            asteroid.TargetPlayer = player;                          //Enemy에 플레이어 설정

            asteroid.transform.position = transform.position;       //목적지 중심지 저장
            float r = Random.Range(MinY, MaxY);                     //목적지의 y값만 랜덤으로 조정

            asteroid.transform.Translate(r * Vector3.up);             


            Vector3 destPos = destination.position;
            destPos.y = Random.Range(MinY, MaxY);
            asteroid.Direction = (destPos - transform.position).normalized;   //방향만 남긴 nomalize
                                    
            yield return new WaitForSeconds(interval);            //인버털만큼 대기
        }
    }



    //protected override void OnDrawGizmos()
    //{
    //    base.OnDrawGizmos();
            //목적지 영역을 큐브로 그리기
    //    Gizmos.color = Color.blue;
    //    if (destination == null)      //destination이 자식 transform이기 때문에 editor상에는 없음 플레이전에는 없음

    //    {
    //        destination = transform.GetChild(0);      //플레이 전인 상황이라면 찾아서 넣기
    //    }

    //    Gizmos.DrawWireCube(destination.position,
    //    new Vector3(1, Mathf.Abs(MaxY) + Mathf.Abs(MinY) + 2, 1));    //큐브 그리기
    //}

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //Gizmos.color = new Color(); // RGB로 내가 원하는 색도 정할 수 있다

        //스폰지점을 선으로 긋기
        Vector3 from = transform.position + Vector3.up * MinY;
        Vector3 to = transform.position + Vector3.down * MaxY;

        //Gizmos.DrawLine(from, to);

        Gizmos.DrawWireCube(transform.position, new Vector3(1, Mathf.Abs(MaxY) + Mathf.Abs(MinY) + 2, 1)); // MaxY +(-MinY) 
                                                                                                           //+2
    }


}
