using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Enemy : MonoBehaviour
{
    public GameObject Exploprefab; //이팩트
    
    public float speed = 1.0f;     // 적 이속
    [Range(0.1f, 3.0f)] //슬라이더 생성// 변수 범위를 MIN MAX사이로 변경 가능
    public float amplitude = 1.0f; // 사인 결과 값을 증폭시킬 변수(위아래 차이 결정)
    public float frequency = 1.0f; //사인 그래프가 한번 도는 데 걸리는 시간


    // 시간 누적이 필요하다 
    float timeElapsed = 0.0f;
    //처음 등장위치변수 설정
    float baseY;
    // Start is called before the first frame update
    void Start()
    {
        baseY = transform.position.y; //월드 좌표
        //transform.localpsition.y 로컬좌표
    }

    // Update is called once per frame
    void Update()
    {
        // 시간 누적 문단// frequency에 비례해서 시간 증가가 빠르게 된다.
        timeElapsed += Time.deltaTime* frequency; 
        //x는 현재 위치에서 약간 왼쪽으로 이동//tansform.position.x에서 - speed *Tim.deltaTime을 해주니 왼쪽 +로 변경 시 오른쪽.
        float x = transform.position.x - speed * Time.deltaTime;
        // y는 시작위치에서 sin 결과값만큼 변경
        float y = baseY + Mathf.Sin(timeElapsed)*amplitude;    

        transform.position = new Vector3(x, y, 0); //합쳐서 움직이는 코드
    }
    //collsion 충돌정보
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))       //충돌 했을 때 실행
        {
            Debug.Log($"적은 {collision.gameObject.name}"); //문구출력 
            GameObject obj = Instantiate(Exploprefab);      // 오브젝트 생성하고 obj변수에 저장
            obj.transform.position = transform.position;    // 적의 위치로 설정 
            Destroy(gameObject);                            // 적삭제
        }
        
    }

}
