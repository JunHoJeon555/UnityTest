//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Globalization;
//using System.IO;
//using TMPro;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UI;

//public class RankPanel : MonoBehaviour
//{
//    //UI에서 표싷하는 랭킹 한줄들을 모아둔 배열
//    RankLine[] rankLines = null;

//    //랭킹별 최고점(0번째가 1등, 4번째가 5등)
//    int[] highScores = null;

//    //랭킹에 들어간 사람 이름 (0번째가 1등, 4번째가 5등)
//    string[] rankerNames = null;

//    int rankCount = 5;

//    //랭킹이 업데이트 되지 않았을믈 표시하는 상수
//    const int NotUpdated = -1;

//    //현제 업데이트 된 랭킹의 인덱스
//    int updatedIndex = NotUpdated;

//    //인풋 필드 컴포넌트
//    TMP_InputField inputField;
//    private void Awake()
//    {
//        inputField = GetComponentInChildren<TMP_InputField>(); //컴포넌트 찾고
//        inputField.onEndEdit.AddListener(OnNameInputEnd);       //입력이 끝났을 때 

//        //inputField.onSubmit;
//        //inputField.onValueChanged;
//        rankLines = GetComponentsInChildren<RankLine>(); //모든 랭킹라인 다 가져오기

//        int rankCount = rankLines.Length;   //UI갯수에 맞게
//        highScores = new int[rankCount];   //배열 확보
//        rankerNames = new string[rankCount];


//        LoadRankingData();  // 데이터 읽기(파일 없으면 디폴트)
//    }



//    private void Start()
//    {
//        inputField.gameObject.SetActive(false);             //시작할 때 인풋 필드 안보이게 만들기 
//        Player player = FindObjectOfType<Player>();
//        player.onDie += RankUpDate; //플레이어가 죽었을 때 업데이트시도
//    }
//    /// <summary>
//    /// 이름 입력이 완료되었을 때 실행되는 함수
//    /// </summary>
//    /// <param name="text">입력받은 이름</param>
//    private void OnNameInputEnd(string text)
//    {

//    }


//    private void RankUpDate(Player player)
//    {
//        bool isUpdated = false;             //랭킹이 업데이트 되었는지 표시용
//        int newScore = Player.Score;        //새 점수 
//        for (int i = 0; i < rankCount; i++)  //랭킹 1등부터 5등까지 확인
//        {
//            if (highScores[i] < newScore)   //새 점수가 현재 랭킹보다 높으면 
//            {
//                for (int j = rankCount - 1; j > i; j--) //현재 랭킹 부터 아래쪽을 한칸씩 아래로 밀기
//                {
//                    highScores[j] = highScores[j - 1];
//                    rankerNames[j] = rankerNames[j - 1];
//                }
//                highScores[i] = newScore;           //새 점수를 현재 랭킹에 끼워 넣기
//                rankerNames[i] = " ";
//                updatedIndex = i;
//                isUpdated = true;

//                Vector3 newPos = inputField;
//                newPos.y = rankerLine[i].transform.position.y;
//                inputField.gameObject.SetActive(true);//업데이트 되었음을 표시
//                break;              //랭킹 삽입 될 곳을 찾았으니 중지
//            }
//        }
//        if (isUpdated)               //랭킹이 업데이트 되었으면
//        {

//            SaveRankingData();      //새로 저장하고
//            RefreshRankLines();     //UI 갱신

//        }
//    }

//    void SaveRankingData()
//    {
//        //PlayerPrefs.SetInt("Score", 10); //컴퓨터에 Score라는 이름으로 10을 저장

//        //SaveData saveData = new SaveData();
//        SaveData saveData = new(); //윗줄과 같은 코드(타입을 알 수 있기 때문에 생략)
//        saveData.rankerNames = rankerNames; //생성한 인스턴스에 데이터 기록
//        saveData.highScores = highScores;
//        string json = JsonUtility.ToJson(saveData);// saveData에 있는 내용을 json 양식으로 설정된 string으로 변경
//        string path = $"{Application.dataPath}/Save/";
//        if (!Directory.Exists(path))                    // path에 저장된 폴더가 있는지 확인
//        {
//            Directory.CreateDirectory(path);            // 폴더가 없으면 그 폴더를 만든다.
//        }

//        string fullPath = $"{path}Save.json";           //(fullPath)전체경로 = 폴더 + 파일이름 + 파일확장자
//        File.WriteAllText(fullPath, json);              //fullPath에 json내용 파일로 기록하기
//    }

//    bool LoadRankingData()
//    {
//        bool result = false;                            //경로
//        string path = $"{Application.dataPath}/Save/";  //전체 경로
//        string fullPath = $"{path}Save.json";           //폴더와 파일이 있는지 확인


//        result = Directory.Exists(path) && File.Exists(fullPath);
//        if (result)
//        {
//            string json = File.ReadAllText(fullPath);
//            SaveData loadData = JsonUtility.FromJson<SaveData>(json);   // json문자열을 파싱해서 SaveData에 넣기
//            highScores = loadData.highScores;       // 실제로 최고 점수 넣기
//            rankerNames = loadData.rankerNames;      // 이름 넣기
//        }
//        else
//        {
//            int size = rankLines.Length;
//            for (int i = 0; i < size; i++)
//            {
//                int resultScore = 1;
//                for (int j = size - i; j > 0; j--)
//                {
//                    resultScore *= 10;
//                }
//                highScores[i] = resultScore; // 10만, 만, 천, 백, 십

//                char temp = 'A';
//                temp = (char)((byte)temp + i);
//                rankerNames[i] = $"{temp}{temp}{temp}";     // AAA,BBB,CCC,DDD,EEE
//            }

//        }
//        RefreshRankLines();     // 로딩이 되었으니 RankLines 갱신
//        return result;
//    }


//    //랭크 라인 화면 업데이트용 함수
//    void RefreshRankLines()
//    {
//        for (int i = 0; i < rankLines.Length; i++)
//        {
//            rankLines[i].SetData(rankerNames[i], highScores[i]);
//        }
//    }


//}
