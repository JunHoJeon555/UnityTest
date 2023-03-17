using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 이동속도
    /// </summary>
    public float moveSpeed = 5f;
    
    /// <summary>
    /// 회전속도
    /// </summary>
    public float rotateSpeed = 180f;
    
    /// <summary>
    /// 점프력
    /// </summary>
    public float jumpForce = 5f;

    /// <summary>
    /// 현재이동방향
    /// </summary>
    float moveDir = 0f;   //-1 ~ 1 사이 (1:앞 -1:뒤)
    
    /// <summary>
    /// 회전방향
    /// </summary>
    float rotateDir = 0f; //-1 ~ 1 사이 (1:우 -1:좌)
    
    /// <summary>
    /// 현재 점프 여부 true면 점프, false면 점프 중 아님
    /// </summary>
    bool isJumping = false;
    
    
    Rigidbody rigid;
    PlayerInputActions inputActions;
    Animator anim;
    
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        inputActions = new PlayerInputActions();
        anim= GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();                      //플레이어 인풋 액션 맵 활성화
        inputActions.Player.Move.performed += OnMoveInput; // 액션들에게 함수 바인딩하기
        inputActions.Player.Move.canceled+= OnMoveInput;   
        inputActions.Player.Use.performed += OnUseInput;
        inputActions.Player.Jump.performed += OnJumpInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Jump.performed -= OnJumpInput; //액션에 연결된 함수들 바인딩 해제
        inputActions.Player.Use.performed -= OnUseInput;    
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Disable();                     //플레이어 액션맵 비활성화

    }


    private void FixedUpdate()
    {

        Move();         //이동처리
        Rotate();       //회전처리


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) //Ground와 충돌했을 때만 
        {
            OnGround(); //찾기 함수 실행
        }
    }


    //private void LateUpdate() // 카메라 같은거 처리할 때 아니면 모든 업데이트를 사용할 때 마지막으로 계산을 해줘야 할 때



    private void OnMoveInput(InputAction.CallbackContext context)
    {
       Vector2 input = context.ReadValue<Vector2>(); //context에 포함되는것들이 꽤 있다.
       rotateDir = input.x; //좌우  (좌:-1 우:+1)
       moveDir = input.y;   //앞뒤  (앞:+1 뒤:-1)
                            //Debug.Log(input); 값 확인

    //context.performed : 액션에 연결된 키 중 하나라도 입력 중이면 true 아니면 false
    //context.canceled  : 액션에 연결된 키가 모두 입력 중이지 않으면 true 아니면 false
        anim.SetBool("IsMove", !context.canceled);  //애니메이션 파라메터 변경(Idle, Move 중 선택)
        
    }


    private void OnUseInput(InputAction.CallbackContext context)
    {
        
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {

        Jump(); //점프 처리 함수 실행
        
    }   

    /// <summary>
    /// 이동처리 함수
    /// </summary>
    void Move()
    {
        //moveDir 방향으로 이동 시키기 (앞 아니면 뒤)    
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveSpeed * moveDir * transform.forward); // transform.forward z축방향   
        
    }

    /// <summary>
    /// 회전처리 함수
    /// </summary>
   void Rotate()
    {
        //rigid.AddTorque(); //회전력 추가
        //rigid.MoveRotation();// 특정회전으로 설정하기

        //Quaternion rotate = Quaternion.Euler(0,
        //    Time.fixedDeltaTime*rotateSpeed*rotateDir,
        //    0);

        //특정 축을 기준으로 회전 시키는 쿼터니언을 만드는 함수
        Quaternion rotate = Quaternion.AngleAxis(Time.fixedDeltaTime * 
                            rotateSpeed * rotateDir, transform.up); //transform.up = y축을 고정으로 회전//플레이어의 up방향을 기준이로 

        //위에서 만든 회전을 사용
        rigid.MoveRotation(rigid.rotation * rotate);

     
    }


    /// <summary>
    /// 점프 처리 함수
    /// </summary>
    void Jump()
    {
        if (!isJumping) //점프 중이 아닐때만
        {

            rigid.AddForce(jumpForce * Vector3.up, ForceMode.Impulse); ; //월드의 Up방향으로 힘을 즉시 가하기
            isJumping = true;   //점프 중이라고 표시
        }
    }

    /// <summary>
    /// 착지했을 때 처리 함수 
    /// </summary>
    private void OnGround()
    {
        isJumping = false; //점프가 끝났다고 표시
    }
}
