using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Rotation1 : Test_Base
{
    public Transform objBase;
    public Transform objChild1;
    public Transform objChild2;

    private void Start()
    {
        //쿼터니언은 회전을 저장해 놓은 구조체
        Quaternion q = Quaternion.identity; //identity x,y,z 축 모두 회전하지 않는 회전을 저장해 놓은 것

        //q = Quaternion.Euler(0,0,0); //오일러 앵글과 같은 회전을 하는 쿼터니언 만들기. 위에 코드와 똑같다.
        //objChild2.rotation = q;
    }


    protected override void Test1(InputAction.CallbackContext _)
    {
        Quaternion q = Quaternion.identity;
        objBase.rotation = q;
        objChild1.rotation = q;
        objChild2.rotation = Quaternion.Euler(0,90,0); //y축으로 90eh(deree)
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        //월드의 위를 바라보기
        //objChlid1.rotation = Quaternion.LookRotation(Vector3.up) ;
       
        //onjChild1이 월드의 위를 바라보고 objChild1의 위쪽 방향은 월드의 오른쪽로 설정한다
        objChild1.rotation = Quaternion.LookRotation(Vector3.up, Vector3.right);
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        //두 회전의 사이각 구하기(A회전에서 B회전으로 가는데 필요한 각 구하기)
        float angle = Quaternion.Angle(objChild1.rotation,objChild2.rotation);
        Debug.Log($"Angle {angle}");
    }

    protected override void Test4(InputAction.CallbackContext _)
    {
        //특정 축을 기준으로 회전을 시킨 함수 
        objChild2.rotation = Quaternion.AngleAxis(45, Vector3.forward);
    
        //objCild2의 원래 회전에 추가 
        objChild2.rotation = objChild2.rotation * Quaternion.AngleAxis(45, Vector3.forward);
        //objCild2/rotation = objclid.rotation ** Quaternion.AngleAxis(l


    }


    //protected override void Test5(InputAction.CallbackContext _)
    //{
    //    //시작방향에서 도착방향으로 바라보게만드는 회전을만드슨ㄴ 함수
    //    objBase.rotation *= Quaternion.FromToRotationobjBase(objBase.forward.objChild2.forward);
    //}




}
