using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : Spawner
{


    protected override void OnSpawn(EnemyBase enemy)
    {
        Fighter fighter = enemy as Fighter;
        if(fighter != null )
        {
            float r = Random.Range(MinY, MaxY); //랜덤하게 적용할 기준높이 구하기
            fighter.BaseY = transform.position.y+r; // 기준 높이가 적용

        }
        else
        {
            Debug.Log("SpawnerEnemy : 적 비행기가 아닌데 스폰하려고 함");
        }
    
    
    
    
    
    
    }
























    //override protected IEnumerator Spawn()
    //{
    //    while (true)
    //    {
    //        //생성하고 생성한 오브젝트를 스포너의 자식으로 만들기
    //        // GameObject obj = Instantiate(spawnPrefab, transform);            // 밑에 있는 코드를 ,transform

    //        GameObject obj = Factory.Inst.GetObject(PoolObjectType.Enemy);
    //        //obj.transform.position += transform.position;   
    //        //folat r에 Random Y값을 적용시킨다. //랜덤하게 높이 구함
    //        Fighter enemy = obj.GetComponent<Fighter>();              //생성한 게임오브젝트에서 Enemy 컴포넌트 가져오기 
    //        enemy.TargetPlayer = player;                          //Enemy에 플레이어 설정

    //        enemy.transform.position = transform.position;
    //        float r = Random.Range(MinY, MaxY);                   //Vector3가 아닌 Vector2이다. 2차원 좌표일 때 사용할 수 있는

    //        enemy.BaseY = transform.position.y + r;               //기준 높이 적용


    //        yield return new WaitForSeconds(interval);            //인버털만큼 대기
    //    }
    //}

}
