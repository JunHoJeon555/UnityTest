using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseAlarm : MonoBehaviour
{
    //delegate선언

    /// <summary>
    /// 사용 가능한 아이템을 사용시도한다는 알람용 델리게이트
    /// </summary>
    public Action<IUserbleObject> onUseableItemUsed;


    /// <summary>
    /// 애니메이션에서ㅏ 트리거를 껐다켰다 하는데 켜진 시점에서 컬라이더가 들어오면 실행
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //아이템 사용을 알리는 것 
        IUserbleObject item = other.GetComponent<IUserbleObject>();

        //충졸한 컬라이더가 내가 원하는 스크립트가 있는 root가 아닐 수 있다,
        Transform target = other.transform; 
        if (target.parent != null)
            {
                target= target.parent;      //스크립트가 있는 치상단 부모가 나올 때까지 곗곡 부모타고 올라가기

            }
 


        //사용가능한 아이템인지 확인 (IUseableObject 인터페이스가 있다. == 사용가능한 오브젝트이다.)
        IUserbleObject obj = target.GetComponent<IUserbleObject>();
        if (obj != null)
        {
            onUseableItemUsed?.Invoke(obj); //사용 가능한 오브젝ㅌ이니까감자

        }
    }
}
