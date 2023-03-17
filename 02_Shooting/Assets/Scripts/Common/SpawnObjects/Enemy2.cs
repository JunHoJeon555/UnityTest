using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    
    public float speed = 5.0f; 
    public float bounceTime = 1.0f; //사용자가 편히 사용하기 위함.
    float currentTime = 0.0f;
    Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector2(-1,1);
        dir.Normalize();                               // 방향만 남길려고한다.(=길이가 1이다.) 
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.deltaTime;                       //시간을 계속 누적한다
        if (currentTime > bounceTime)                       // 지정된 시간이 bounceTime보다 크면 실행!
        {
            currentTime = 0.0f;                             //0으로 다시 초기화 시켜서 다시 실행하도록 한다.
            dir.y = -dir.y;                                 //반대로 한다.
        }
        transform.Translate(Time.deltaTime * speed * dir); //    
    }
    



}
