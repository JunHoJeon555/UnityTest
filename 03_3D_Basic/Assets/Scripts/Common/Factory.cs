using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


public class Factory : Singleton<Factory> //모든 오브젝트 생성
{
    //생성할 오브젝트의 풀들
    BulletPool bulletPool;
       

    protected override void PreInitialize()
    {
        bulletPool = GetComponentInChildren<BulletPool>();     
    }

    protected override void Initialize()
    {
        bulletPool?.Initialize();       // ?.은 null이 아니면 실행, null이면 아무것도 하지 않는다.
        
    }

    

    /// <summary>
    /// Bullet풀에서 Bullet하나 꺼내는 함수
    /// </summary>
    /// <param name="parentT">기준 트랜스폼(이 트랜스폼의 위치, 회전, 스케일 사용)</param>
    /// <returns></returns>
    public Bullet GetBullet(Transform parentT = null) => bulletPool?.GetObject(parentT);
   
   
}
