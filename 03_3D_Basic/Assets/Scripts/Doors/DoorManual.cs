using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorManual : DoorBase , IUserbleObject
{
    //사용되면 실행 

    /// <summary>
    /// 자동으로 닫힐떄까지 걸리는 시간
    /// </summary>
    public float closeTime = 3f;

    /// <summary>
    /// 코루틴용을 한번만 만들고 재활용하기 위한 용도
    /// </summary>
    WaitForSeconds closeWait;

    TextMeshPro text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TextMeshPro>();
    }
    
    void Start()
    {
        text.gameObject.SetActive(false);
        //시작할 때 한번만 만들기
        closeWait = new WaitForSeconds(closeTime);
    }




    /// <summary>
    /// 이 오브젝트가 실행되는 함수
    /// </summary>
    public void Used()
    {
        Debug.Log("사용됨");
        OnOpen();                       //문엵기
        //Invoke("OnClose", 3f);
        StartCoroutine(AutoClose());    //closeTime초 이후에 자동으로 닫히게 하기
    }

    IEnumerator AutoClose()
    {
        yield return closeWait; //closeWait초 만큼 대기
        OnClose();              //문 닫기
    }

    private void OnTriggerEnter(Collider other)
    {
        text.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
    }

}
