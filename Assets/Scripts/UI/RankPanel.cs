using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RankPanel : MonoBehaviour
{
    //UI에서 표싷하는 랭킹 한줄들을 모아둔 배열
    RankLine[] rankLines = null;

    //랭킹별 최고점(0번째가 1등, 4번째가 5등)
    int[] highScore = null;

    //랭킹에 들어간 사람 이름 (0번째가 1등, 4번째가 5등)
    string[] rankerNames = null;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>(); //모든 랭킹라인 다 가져오기
    
        int size = rankLines.Length;
        highScore= new int[size];   //배열 확보
        rankerNames= new string[size];

        if (!LoadRankingData())
        {
         for(int i = 0; i<size; i++)
         {
            //10만 , 만 , 천 ,백 , 십
            int result = 10;
            for(int j = size-i; j >0; j--)
            {
                result *= 10;
            }
            highScore[i] = result; //10만 만 천 백 십

            char temp = 'A';
            temp = (char)((byte)temp + i);
            rankerNames[i] = $"{temp}{temp}{temp}";  //AAA BBB CCC

         }

        }

        
    }

    void SaveRankingData()
    {

    }

    bool LoadRankingData()
    {
        return false;
    }
    
    void RefreshRankLine()
    {
        //rankLives;
        //highScores;
        //rankerNames;
    }


}
