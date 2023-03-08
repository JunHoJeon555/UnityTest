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
    
    protected override void OnSpawn(EnemyBase enemy)
    {
        base.OnSpawn(enemy);
        Vector3 destPos = destination.position;
        destPos.y = Random.Range(MinY, MaxY);

        Asteroid asteroid = enemy as Asteroid;
        if(asteroid != null) 
        { 
        
            asteroid.Direction = (destPos - enemy.transform.position).normalized;
        }
        else
        {
            Debug.LogError("SpawnerAsteroid : 운석이 아닌데 스폰하려고 함");
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
