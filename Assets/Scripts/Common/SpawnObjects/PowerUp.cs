using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : PoolObject
{
    //이동 속도
    public float moverSpeed = 0.5f;
    //방향전환하는 시간간격
    public float dirChangeInterval = 1.0f;

    //플레리어의 트랜스폼
    Transform playerTransform;
    //이동방향
    Vector2 dir;
    //코루틴으로 미리 계산해 놓은 변수
    WaitForSeconds changeInterval;


    private void Awake()
    {
        changeInterval = new WaitForSeconds(dirChangeInterval);
    }

    private void OnEnable()

    {

        if (playerTransform == null) //없을 떄만 찾기
        {

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerTransform= player.transform;
            //playerTransform = FindObjectOfType<Player>().transform;
        }
        SetRandomDirection(true); //시작할 때 랜덤 방향 설정하기

        StopAllCoroutines();
        StartCoroutine(DirChange());



    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * dir); //방향대로 이동시키기
    }
    private void SetRandomDirection(bool allRandom = false)
    {
        dir = Random.insideUnitCircle; //반지름이 1인 원 안의 랜덤한 위치 가져오기
        if(!allRandom && Random.value < 0.3f)
        {
            // 완전 랜덤이 아니고 40%의 확률에 당첨이 되면 플레이어의 반대 방향으로 이동시키기
            Vector2 playerToPowerUp =transform.position - playerTransform.position;  // 플레이어에게 가는 방향벡터 
            // 플레이어에서 파워업으로 가는 방향 벡터를 z축 기준으로 +-90도를 랜덤으로 회전
            dir = Quaternion.Euler(0,0,Random.Range(-90.0f,90.0f)) * playerToPowerUp;
            //Debug.Log("도망");
        }
        else
        {
            // 완전 랜덤이거나 40% 확률에 당첨되지 않았을 때
            dir = Random.insideUnitCircle;
            

        }

            dir = dir.normalized;          // 길이를 1로 만들어서 항상동일한 속도가 되게 만들기

    }

    IEnumerator DirChange()
    {
        while (true)
        {
            yield return changeInterval;
            SetRandomDirection();           //랜덤하게 방향변경하기
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            dir = Vector2.Reflect(dir, collision.contacts[0].normal); //보더에 부딪치면 방향전환하기
        }
    }
}
