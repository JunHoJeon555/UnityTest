using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
        /// <summary>
     /// 시작 이동속도
     /// </summary>
    public float initialSpeed = 20.0f;

    Rigidbody rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //transform.rotation = Quaternion.identity; //xyz 0
        //transform.rotation = transform.parent.rotation;
        rigid.angularVelocity = Vector3.zero;               //회전력은 전부 제거
        rigid.velocity = initialSpeed * transform.forward;  // 초기 운동량 결정
        StartCoroutine(LifeOver(10f)); //시작10초 뒤에 비활성화
    }
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(LifeOver(2f));  // 부딪치면 2초 뒤 비활성화

    }
}
