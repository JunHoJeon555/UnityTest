using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : AsteroidBase
{
    //[Header("큰 운석 데이터-----------------0")]

    /// <summary>
    /// 파괴 될 때 생성할 오브젝트의 종류
    /// </summary>
    public PoolObjectType childType = PoolObjectType.AsteroidSmall;

    /// <summary>
    /// 최대 최소 수명 그리고 현재 남아있는 수명 변수 선언
    /// </summary>
    public float minLifeTime = 4.0f;
    public float maxLifeTime = 7.0f;

    /// <summary>
    /// 파괴 될 때 생성할 오브젝트이 갯수
    /// </summary>
    int splitCount = 3;

    //자폭여부 표시
    bool isSelfCrush = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        isSelfCrush = false;
        float lifeTime = Random.Range(minLifeTime, maxLifeTime);
       
        StopAllCoroutines();                //이전 코루틴 제거
        StartCoroutine(SelfCrush(lifeTime));// 새 자폭 코루틴 시작
    }


    /// <summary>
    /// 자폭용 코루틴
    /// </summary>
    /// <param name="lifeTime"></param>
    /// <returns></returns>
    IEnumerator SelfCrush(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        isSelfCrush = true;
        Crush();
    }


    protected override void OnCrush()
    {
       if(!isSelfCrush)
        {
            TargetPlayer?.AddScore(score); 
        }
        splitCount= Random.Range(3, 8); //3~7개 생성

        float angleGap = 360.0f / splitCount;               //작은 운석간의 사이각 계산 
        float seed = Random.Range(0.0f, 360.0f);            //처음 적용할 오차 랜덤으로 구하기
      
        for(int i=0;i<splitCount; i++)
        {

    
            GameObject obj = Factory.Inst.GetObject(childType);
            obj.transform.position=transform.position;          
            AsteroidBase small = obj.GetComponent<AsteroidBase>();  //splitCount만큼 반복
            small.TargetPlayer = TargetPlayer;                      //점수 추가를 위해 플레이어 설정 
            //Up(0,1,0) 벡터를 일단 z축에서 seed만큼 회전시키고 추가로 angleGap *i 만큼 더 회전 시키고 small의 방향을 지정한는 코드
            small.Direction = Quaternion.Euler(0, 0, angleGap*i+seed)*Vector3.up; ; 
            
            

            
        }
    }



}

