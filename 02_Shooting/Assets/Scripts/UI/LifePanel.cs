using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LifePanel : MonoBehaviour
{
    TextMeshProUGUI lifeText;


    private void Awake()
    {
        Transform textTransform = transform.GetChild(2);        // 세번째 자식 가져오기
        lifeText = textTransform.GetComponent<TextMeshProUGUI>();  // 텍스트메시 프로 찾아놓기
    }

    private void Start()
    {

        Player player = FindObjectOfType<Player>();  //플레이어 찾기
        //Player player = GameObject.FindObhectWithTag("Player").GetComponent<Player>(); s
        player.onLifeChange += Refresh; //델리게이트에 함수 등록
        
    }


    

    void Refresh(int life)
    {

        lifeText.text = life.ToString(); //파라메터 그대로 글자 찍기
   
    }

}
