using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Rotation2 : Test_Base
{
    public Transform objBase;
    public Transform objChild1;
    public Transform objChild2;


    protected override void Test1(InputAction.CallbackContext _)
    {
        Quaternion q = Quaternion.identity;
        objBase.rotation = q;
        objChild1.rotation = q;
        objChild2.rotation = objChild2.rotation * Quaternion.AngleAxis(90, Vector3.up);
    }
    protected override void Test2(InputAction.CallbackContext _)
    {
        
    }
    protected override void Test3(InputAction.CallbackContext _)
    {
        //선형 보간
        //Quaternion.Lerp
        Quaternion a = Quaternion.LookRotation(Vector3.forward);
        Quaternion b = Quaternion.LookRotation(Vector3.right);

        //곡면용 보간 계산량이 많다.
        // a회전에서 b회전으로 지행된다고 했을 때 절반만큼 진행되었을 때의 회전 구하기
        objBase.rotation = Quaternion.Slerp(a,b,0.5f);
    }


    private void Update()
    {
        //from회전에서 to회전으로 maxDefrreDetal만큼 회전시키, 일저완 속도로 회전한다.
        //objBase.rotation = Quaternion.RotateTowards(objBase.rotation, to Time.deltaTime * 90f);

        //from회전에서 to회전으로 점ㅈ머 속도가 줄어들면서 회전한다.
        //objBase.rotation = Quaternion.Slerp(objBase,.rotation, Time.deltaTime * 0.5f);
    }
}
