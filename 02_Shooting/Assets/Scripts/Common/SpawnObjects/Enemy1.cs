using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy1 : MonoBehaviour
{
    float speed = 5.0f; //속도 

    float baseY; // 기준높이 
    float dir = 1.0f; //
    public float heght = 3.0f; //기획자가 편하게 변경하기위해 public
    
    // Start is called before the first frame update
    void Start()
    {
        baseY = transform.position.y; //시작할 때 y 값 기록한다.
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * -transform.right);  //왼쪽으로 가다 오른쪽이 반대이므로 -를 넣어준다.

   
        transform.Translate(Time.deltaTime * speed * dir * transform.up); //위아래로 움직임
       
        //이 게임오브젝트의 Y위치가 일정 이상 올라가거나 내려가면 방향변경
        if(transform.position.y > baseY || transform.position.y < baseY - heght) //논리 연산자 
        {                                                                        //&& = 양 변이 모두 true일때만 true이다.
                                                                                 //|| : 양 변 중 하나만 true면 true이다.
            dir *= -1.0f;
        }                                                                        


    }

}
