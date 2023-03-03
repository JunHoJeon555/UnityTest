using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class EnemyBase : PoolObject
{
    [Header("기본 데이터------------------0")]
    
    //적의 hp   
    public int hitPoint = 1;
    public int MaxHitPoint = 3;
    //속도
    public float moveSpeed = 1.0f;
   
    /// <summary>
    ///파괴 점수
    /// </summary>
    public int score = 30;

    //파괴 이팩트
    public PoolObjectType destroyEffect = PoolObjectType.Explosion;

    //파괴가 안되었다// true면 파괴된 상황 
    bool isCrushed = false;
    
    //플레이어 참조
    Player player = null;



    //plyer에 처음 한번만  
    public Player TargetPlayer
    {
        protected get => player;
        set
        {
            if (player == null)
            {
                player = value;
            }
        }
    }

    protected virtual void OnEnable()
    {
        //정상이 된 것 표시
        isCrushed = false;
        hitPoint = MaxHitPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Attacked();
        }

    }

    /// <summary>
    /// 공격 당하먄 무조건 실행해야하는 일들
    /// </summary>
    protected  void Attacked()
    {
        OnHit();
        hitPoint--;         //맞으면 hitPoint 감소
        if (hitPoint <= 0)   //0보다 작아지면
        {
            Crush();        //OnCrush 실행
        }
    }

    /// <summary>
    /// 공격
    /// </summary>

    protected void OnHit()
    {

    }

    /// <summary>
    /// 부서지면 무조건 실행할 일들 처리 무조건 
    /// </summary>
    protected void Crush()
    {
        if (!isCrushed)
        {
            isCrushed = true;    //파괴되었다는 표시

            player?.AddScore(score);    //클래스별 별도 파괴 처리

            OnCrush();
            
            GameObject obj = Factory.Inst.GetObject(destroyEffect);  //터지는 이팩트 생성
            obj.transform.position = transform.position;            //

            gameObject.SetActive(false);
        }
    }



    //상속받은 클래스별로 따로 처리할 일들
    protected virtual void OnCrush()
    {
      
            
    }
    
    

}
