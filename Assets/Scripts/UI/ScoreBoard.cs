using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //textMeshPro
using UnityEngine.SocialPlatforms.Impl;
using System.Transactions;

public class ScoreBoard : MonoBehaviour
{
    //목표 점수 따로 현재 점수 따로 만약 현재 점수가 목표점수보다 작으면계속 증가!
    TextMeshProUGUI score;

    float currentScore;
    int targetScore;
    public float scoreSpeed = 10.0f;
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
            currentScore += Time.deltaTime*scoreSpeed;                  //currentScore를 ㅊ당 1씩 증가한다.
            currentScore = Mathf.Min(currentScore, targetScore);        //currentScore의 최대치는 targetScore
            score.text = $"{ currentScore:f0}";              //UI에 출력할 때 소수점은 0개만 출력한다.(소수점 출력안함:f0)
        }
    }
    void ScoreUpdate(int newSCore)
    {

        targetScore = newSCore;
    }

    

    
}
