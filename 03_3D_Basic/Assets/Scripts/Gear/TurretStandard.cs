using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretStandard : Turret
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(fireCoroutine);


        //Time.timeScale = 0.1f; //게임씬에 모든 영향을 준다.
    }


    protected override void Onfire()
    {
        Factory.Inst.GetBullet(fireTransform);    
    }
}
