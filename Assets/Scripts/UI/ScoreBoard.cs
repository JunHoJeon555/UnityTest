using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //textMeshPro
using UnityEngine.SocialPlatforms.Impl;
using System.Transactions;

public class ScoreBoard : MonoBehaviour
{
    //// 점수를 출력할 UI text
    TextMeshProUGUI score;
  
    // 현재 UI에서 보이는 점수
    float currentScore;

    // 실제 플레이어가 가지고 있는 점수
    int targetScore;
    public float minScoreSpeed = 50.0f;
    Player player;                                     //player를 지정해줌

    //이 게임 오브젝트가 생성완료 되었을 때 실행되는 함수// 
    private void Awake()
    {
        //Score= GetComponent<TextMeshProUGUI>();      //이 스크립트에 있는 게임 오브젝트 
        Transform child = transform.GetChild(1);       // 두번째 자식 가져오기
        score = child.GetComponent<TextMeshProUGUI>(); //두번째 자식의 컴포넌트 가져오기
        
    }




    //이 게임 오브젝트가 완성된 이후에 활성화 할 때 실행되는 함수
    private void Start()
    {
        /* score.text = $"{score}";*/ //문자이다.
                                      //여기 문자는 최종적으로 받은 score를 여기에다가 입력을 해야한다.
                                      //하지만 score은 정수형이다. 
        
        player = FindObjectOfType<Player>();  //
        player.onScoreChange += ScoreUpdate;  //plater의 onScoreChange 델리게이트가 실행될 때 RefreshScore를 실행해라

        score.text = currentScore.ToString();
    }

    private void Update()
    {                                                        //currentScore가 targerScore보다 작으면 
        if(currentScore < targetScore)
        {
            //currebtScore를 점수 차이에 비례해서 증가시킨다.(최저 minScoreSpeed)
            float speed = Mathf.Max((targetScore-currentScore)*5.0f, minScoreSpeed);
            currentScore += Time.deltaTime*speed;                  //currentScore를 ㅊ당 1씩 증가한다.
            currentScore = Mathf.Min(currentScore, targetScore);        //currentScore의 최대치는 targetScore
            score.text = $"{ currentScore:f0}";              //UI에 출력할 때 소수점은 0개만 출력한다.(소수점 출력안함:f0)
        }
    }
    void ScoreUpdate(int newSCore)
    {

        targetScore = newSCore;
    }

    

    
}
