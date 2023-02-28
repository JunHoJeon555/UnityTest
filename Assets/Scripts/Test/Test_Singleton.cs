using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Test_Singleton : Test_Base
{

    private void Start()
    {
        //Player = new Player; MonoBehaviour는 new를 받을 수 없다. 
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        Factory.Inst.GetBullet();
    }
    protected override void Test2(InputAction.CallbackContext _)
    {
        Factory.Inst.GetHitEffect();
    }
    protected override void Test3(InputAction.CallbackContext _)
    {
        Factory.Inst.GetEnemy();
    }
    protected override void Test4(InputAction.CallbackContext _)
    {
        Factory.Inst.GetExplosionEffect();
    }



}
