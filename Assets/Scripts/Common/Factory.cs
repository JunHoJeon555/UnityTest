using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

//enum(이넘) type
public enum PoolObjectType
{
    Bullet = 0,
    Hit,
    Enemy,
    Explosion
}
public class Factory : Singleton<Factory> //모든 오브젝트 생성
{
        //생성할 오브젝트의 풀들
        BulletPool bulletPool;
        EnemyPool enemyPool;
        ExplosionEffectPool explosionPool;
        HitEffectPool hitPool;

    protected override void PreInitialize()
    {
        bulletPool = GetComponentInChildren<BulletPool>();
        enemyPool = GetComponentInChildren<EnemyPool>();
        explosionPool = GetComponentInChildren<ExplosionEffectPool>();
        hitPool = GetComponentInChildren<HitEffectPool>();
    }

    protected override void Initialize()
    {
        bulletPool?.Initialize();       // ?.은 null이 아니면 실행, null이면 아무것도 하지 않는다.
        enemyPool?.Initialize();
        explosionPool?.Initialize();
        hitPool?.Initialize();
    }

    public GameObject GetObject(PoolObjectType type)
    {
        GameObject result = null;
        switch (type) //타입에 맞게 꺼내서 result에 저장
        {
            case PoolObjectType.Bullet:
                result = GetBullet().gameObject;
                break;
            case PoolObjectType.Hit:
                result = GetHitEffect().gameObject;
                break;
            case PoolObjectType.Enemy:
                result = GetEnemy().gameObject;
                break;
            case PoolObjectType.Explosion:
                result = GetExplosionEffect().gameObject;
                break;

        }
       
        return result;
    }


    // Bullet풀에서 Bullet하나 꺼내는 함수
    public Bullet GetBullet() => bulletPool?.GetObject();
   
    // Bullet풀에서 Bullet하나 꺼내는 함수
    public Effect GetHitEffect() => hitPool?.GetObject();
    
    // Bullet풀에서 Bullet하나 꺼내는 함수
    public Enemy GetEnemy() => enemyPool?.GetObject();
    
    // Bullet풀에서 Bullet하나 꺼내는 함수
    public Effect GetExplosionEffect() => explosionPool?.GetObject();
}
