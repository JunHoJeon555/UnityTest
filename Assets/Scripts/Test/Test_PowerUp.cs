using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_PowerUp : Test_Base
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        //player.Power = 1;
        Factory.Inst.GetObject(PoolObjectType.PowerUp);
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        //player.Power = 2;
        Factory.Inst.GetPowerUp();
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        //player.Power = 3;
    }

    protected override void Test4(InputAction.CallbackContext _)
    {
        //player.Power = 4;
    }

    protected override void Test5(InputAction.CallbackContext _)
    {
        //player.Power = 0;
    }




}
