using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TurretHunter : Turret
{
    
    /// <summary>
    /// 이 터렛의 시야 범위
    /// </summary>
    public float sightRange = 10.0f;

    /// <summary>
    /// 터렛이 발사를 시작하는 각도
    /// </summary>
    public float fireAngle = 10.0f;

    //1초에 한바퀴 //회전속도
    public float turnSpeed = 360f;
 
    /// <summary>
    /// 발사 중인지 아닌지 표시하는 변수. true면 발사 중, fasle 발사하지 않는 중이다.
    /// </summary>
    bool isFiring = false;
    
    /// <summary>
    /// 이 터렛이 가지고 있는 시야범위를 표시하는 트리거
    /// </summary>
    SphereCollider sightTrigger;
   
    /// <summary>
    /// 이 터렛이 추적할 대상
    /// </summary>
    Transform target = null;



    protected override void Awake()
    {
        base.Awake();
        sightTrigger = GetComponent<SphereCollider>();  // 컬라이더 찾기
    }

    protected override void Start()
    {
        base.Start();
        sightTrigger.radius = sightRange;   // 시야 범위로 컬라이더 크기 변경
        
    }

    private void Update()
    {
        LookTarget();
    }


    //플레이어가 일정범위 안에 들어오면 플레이어 방향을 바라본다. 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        target = other.transform;   // 트리거에 플레이어가 들어오면 타겟으로 설정

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;              // 트리거에서 플레이어가 나가면 타겟은 null로 설정
        }
    }

    /// <summary>
    /// 타겟이 있고 테렛이 볼 수 있으면 해당 방향으로 터렛의 고개를 돌리는 함수
    /// </summary>
    private void LookTarget()
    {
        if (IsVisibleTarget())  // 타겟이 있고 볼수 있는지 확인
        {
            Vector3 dir = target.position - barrelBodyTransform.position;
            dir.y = 0;                          // 높낮이는 영향을 안끼치게 0으로 설정
            //barrelBodyTransform.forward = dir;  // 대상을 바라보기

            //Vector3 lookAt = other.transform.position + Vector3.up * barrelBodyTransform.position.y;
            //barrelBodyTransform.LookAt(lookAt);
            

            //터렛이 forward와 플레이어를 향하는 방향벡터의 사이각을 구함
            float angle = Vector3.SignedAngle(barrelBodyTransform.forward, dir,barrelBodyTransform.up);

            if( angle < 1 && angle > -1)
            {
                barrelBodyTransform.rotation = Quaternion.LookRotation(dir);
            }
            else
            { 
                //정방향 회전인지 역방향 회전인지 결정
                float rotateDir = angle > 0 ? 1.0f : -1.0f;

                barrelBodyTransform.Rotate(0, Time.deltaTime*turnSpeed *rotateDir,0);

            }
            //위에 삼항연사자와 같은 코드 성능차이x  
            //if (angle > 0) 
            //{
            //    rotateDir = 1f;
            //}
            //else
            //{
            //    rotateDir = -1f;
            //}

            //발사각 안이면 총알 발사, 밖이면 발사 정지 (=fireAngle ~fireAngle 사이 인지 확인)
          
            if (angle < fireAngle && angle > -fireAngle)
            {
                //발사 
                if(!isFiring)
                {
                    StartCoroutine(fireCoroutine);
                    isFiring= true;
                }

            }
            else
            {
                //발사 정지
                if(isFiring)
                {
                    StopCoroutine(fireCoroutine);
                    isFiring= false;
                }

            }
        }
        else
        {
            if (isFiring)
            {
                StopCoroutine(fireCoroutine);
                isFiring = false;
            }
        }
    
    
    }

    /// <summary>
    /// 타겟이 존재하고 볼 수 있는지 확인하는 함수
    /// </summary>
    /// <returns>타겟이 있고 볼 수 있으면 true, 타겟이 없거나 다른 물체에 가려져 있으면 false</returns>
    bool IsVisibleTarget()
    {
        bool result = false;
        if (target != null) // 타겟이 있는지 확인
        {
            // 터렛의 barrelBodyTransform에서 target 방향으로 나가는 레이 생성
            Vector3 toTargetDir = target.position - barrelBodyTransform.position;

            Ray ray = new Ray(barrelBodyTransform.position, toTargetDir);

            // 레이케스트 시도
            if (Physics.Raycast(ray, out RaycastHit hitInfo, sightTrigger.radius))
            {
                // 레이케스트에 성공했으면 hitInfo에 각종 충돌 정보들이 들어옴                
                //Debug.Log($"충돌 : {hitInfo.collider.gameObject.name}");
                if (hitInfo.transform == target)
                {
                    // 충돌 한 것의 transform과 target이 같으면. 충돌한 것은 플레이어.
                    // 그러면 true를 리턴
                    result = true;
                }
            }
        }

        return result;
    }


    protected override void Onfire()
    {
        Factory.Inst.GetBullet(fireTransform);
    }




    //기즈모 line 2개로 사이각을 만들기



    /// <summary>
    /// 씬창에서 보이는 테스트용 정보 그리는 함수
    /// </summary>
    private void OnDrawGizmos()
    {

        if(barrelBodyTransform == null)     //에디터에서 플레이를 안했을 때 찾기 위해서 사용
        {
            barrelBodyTransform = transform.GetChild(2);    //없으면 찾아 놓기
        }

        Gizmos.color = Color.yellow;                        //기본 색 노랑
        Vector3 from = barrelBodyTransform.position;        //시작 위치 설정
        Vector3 to;                                         //도착지점인 to를 선언을 해놓는다.

        bool isFire = false;
 
        //to는 barrelBodyTransform.position 위치에서
        //barrelBodyTramsform.forward 방향으로 sightRange만큼 위치

        

        Ray ray = new Ray(barrelBodyTransform.position, barrelBodyTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, sightRange, LayerMask.GetMask("Player","Wall")))   //선이 충돌하는지 체크
        {
            
            //충돌한 위치
            to = hitInfo.point;                                 //충돌했으면 도착지점은 충돌한 이치
            Gizmos.color = Color.blue;                                  //충돌하면 파랑색으로 설정 
            Gizmos.DrawSphere(to, 0.1f);                                //충동한 지점을 파랑원으로 강조
            isFire= true;
            
            //Gizmos.DrawLine(from, to);                                //층돌한 라인 그리기
            
        }
            
        else
        {
            // barrelBodyTransform.position 위치에서
            // barrelBodyTransform.forward 방향으로 sightRange만큼 이동한 위치

            to = barrelBodyTransform.position + barrelBodyTransform.forward * sightRange;
            //Gizmos.DrawLine(from, to);
        }
        Gizmos.DrawLine(from, to);

        Vector3 dir1 = Quaternion.AngleAxis(-fireAngle, barrelBodyTransform.up) * barrelBodyTransform.forward;
        Vector3 dir2 = Quaternion.AngleAxis(fireAngle, barrelBodyTransform.up)* barrelBodyTransform.forward;
        
        Gizmos.color = isFire ? Color.red : Color.green;

        to = barrelBodyTransform.position + dir1 * sightRange;
        Gizmos.DrawLine(from, to);
        to = barrelBodyTransform.position + dir2 * sightRange;
        Gizmos.DrawLine(from, to);


    }

   



}
