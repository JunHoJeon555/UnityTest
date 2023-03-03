using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidBase : EnemyBase
{
    [Header("운석 기본 데이터-----------------0")]

    //이동 속도(초 당 이동거리) //최소 최대를 설정
    public float minMoveSpeed = 1.0f;
    public float maxMoveSpeed = 4.0f;

    //회전 속도(초 당 회전각도(도:degree ))
    public float minRotateSpeed = 30.0f;
    public float maxRotateSpeed = 360.0f;

    //파괴 이팩트
    //public PoolObjectType destroyEffect = PoolObjectType.Explosion;

    //public int MaxHitPoint = 3;

    //운석 점수
    //public int score = 30;
    //스프라이트렌더러 적용변수 생성
    SpriteRenderer spriteRenderer;

    //이동속도 회전속도
    //float moveSpeed = 1.0f;
    float rotateSpeed = 1.0f;
    //dir 방향은 left
    Vector3 dir = Vector3.left;
    //운석의 hp   
    //public int hitPoint = 3;

    //bool isAlive = true;


    //운석의 이동방향 설정용프로퍼티
    public Vector3 Direction
    {
        set => dir = value;
    
    }

    //플레이어 참조

    //Player player = null;
    //public Player TargetPlayer
    //{
    //    set
    //    {
    //        if (player == null)
    //        {
    //            player = value;
    //        }
    //    }
    //}



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnEnable()
    {
        //랜덤으로 이동속도 결정
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        //이동속도에 따라 회전속도 변경하기
        //최저일때 0이 되고 최고일때 1이 되는 수식만들기
        //moveSpeed가 min일 때 ratio는 0 , moveSpeed 가 max이면 ratio는 1 
        float ratio = (moveSpeed - minMoveSpeed) / (maxMoveSpeed - minRotateSpeed);

        //ratio가 0이면 minRotateSpeed로  1이면 maxRotateSpped로 간다.
        //Mathf.Lerp는 중간을 정해서 값이 ratio값을 정해놓은 기준으로 
        rotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, ratio); //보간(Interpolate)함수 

        //랜덤으로 좌우반전시키기
        int flip = Random.Range(0, 4);
        spriteRenderer.flipX = (flip & 0b_01) != 0;
        spriteRenderer.flipY = (flip & 0b_10) != 0;

        

    }



    private void Update()
    {
        
        transform.Translate(Time.deltaTime * moveSpeed * dir, Space.World); //속도*방향 , 월드

        //transform.Rotate(0,0, Time.deltaTime *-rotateSpeed); //시계방향으로 초당30도씩 움직이는 운석
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed); //반시계방향으로 초당30도씩 움직이는 운석

        //

    }

    private void OnDrawGizmosSelction()
    {
        //흰색으로 오브잭트 위치에서 dir의 1.5배 만큼 이동한 지점까지 선 그리기
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position+dir * 1.5f );
        

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bullet"))
    //    {
    //      OnHit();
    //    }

    //}

    //void OnHit()
    //{
    //    hitPoint--;         //맞으면 hitPoint 감소
    //    if (hitPoint <= 0)   //0보다 작아지면
    //    {
    //        OnDie();        //OnDie 실행
    //    }
    //}

    //void OnDie()
    //{
    //    if (isAlive)
    //    {
    //        isAlive= false;
    //        player.AddScore(score);
            
    //        gameObject.SetActive(false);

    //        GameObject obj = Factory.Inst.GetObject(destroyEffect);
    //        obj.transform.position = transform.position;

    //        gameObject.SetActive(false);
    //    }
    //}



}

