using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialFighter : EnemyBase
{
    //템 드랍 시스템

    public float startSpeed = 10.0f;
    public float waitSpeed = 5.0f;

    public float appearTime = 0.5f;
    //public float Speed = 20.0f;
    protected override void OnEnable()
    {
        base.OnEnable(); // 기존초기화 작업진행

        // 처음에 빠르게 움직이고 기다리다가 다시 빠르게 움직인다.
        // 일정시간을 정할때 코루틴
        //transform.Translate(Time.deltaTime*moveSpeed*Vector3.left);
        StopAllCoroutines();    
        StartCoroutine(SpawnProduce());

    }

    IEnumerator SpawnProduce()
    {
        moveSpeed = startSpeed;                 //처움에는 속도를 빠르게
        yield return new WaitForSeconds(0.5f); //등장에 걸리는 시간은 0.5ch
        moveSpeed = 0.0f;                       //정지시키긱
        //
        yield return new WaitForSeconds(waitSpeed);
        moveSpeed= 10.0f;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector3.left);
    }



    protected override void OnCrush()
    {
        base.OnCrush(); //기존 폭파 기능들 가져오기
        GameObject obj = Factory.Inst.GetObject(PoolObjectType.PowerUp); //파워업 생성

        obj.transform.position = transform.position; // 현재 내 위치로 옮기다
    }


}
