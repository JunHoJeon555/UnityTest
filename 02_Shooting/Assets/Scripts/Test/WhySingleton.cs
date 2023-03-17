using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhySingleton<T> : MonoBehaviour where T : class
{
    //    private static WhySingleton instance;
    //    public static WhySingleton Inst
    //    {
    //        get
    //        {
    //            if (instance == null) //접근한 시점에서 instanse가 있는지 없는지 확인.
    //            {
    //                //없으면 만들어진적이 없다.

    //                WhySingleton obj = FindObjectOfType<WhySingleton>(); //에디터에서 만들어진 것이 있는지 확인
    //                if (obj == null)                                //null이면 에디터에서 만들어진것도 없다.
    //                {
    //                    GameObject gameObj = new GameObject(); //빈 오브젝트 생성 //MonoBehaviour가 아니여서 new 선언이 가능하다
    //                    gameObj.name = "WhySingleton";            //이름 변경.
    //                    obj = gameObj.AddComponent<WhySingleton>();     //싱글톤을 컴포넌트로 추가.
    //                }
    //                instance = obj;                    //없어서 새로 만든 것이든 에디터가 만들어 놓아던 것이든 instance에 저장
    //                DontDestroyOnLoad(obj.gameObject); // 씬이 닫히더라도 게임 오브젝트가 삭제되지 않게 설정. 
    //                //얘는 scene이 그대로 넘어가도 얘는 그대로 유지하라.
    //            }
    //            return instance; // instance 리턴한다. (없었으면 새로 만들었고 있었으면 있던 것, 그래서 무조건 null이 
    //        }
    //    }

    //    private void Awake()
    //    {
    //        if (instance == null) //처음 불러들였을 때
    //        {
    //            //instance가 null이면 처음 생성 완료된 싱글톤 게임 오브젝트이다.(씬에 배치되어있는 게임 오브젝트)
    //            instance = this; //instance에 이 싱글톤 객체 기록
    //            DontDestroyOnLoad(instance.gameObject); //씬이 닫히더라도 게임 오브젝트가 삭제되지않게 설정
    //        }
    //        else
    //        {
    //            //instance가 null이 아닌면 이미 만들어진 싱글톤 게임 오브젝트가 있는 상황    //Singleton이 여러개 배정되었을 때
    //            if (instance != this) //혹시나 다른 곳에서 지정될 가능성이 있으니까. if로 내가 아니고 
    //            {
    //                //Awake되기 전에 다른 코드에서 프로퍼티를 통해서 접근했고 그래서 생성이 된상황
    //                Destroy(this.gameObject); //나중에 만들어진 

    //            }
    //        }
    //    }


    //}

    ///// <summary>
    ///// 일반 싱글톤 예제
    ///// </summary>
    //// new 할때 뒤에 ()가 있다. 뉴할때 

    //public class TestSingleton
    //{

    //    //static변수를 만들어서 객체를 만들지 놓고 사용할 수 있게 만들기/
    //    private static TestSingleton instance; //instance; 객체 => new변수
    //    //다론 곳에서 instance를 수정하지 못하도록 읽지전용 프로퍼티 만들기
    //    public static TestSingleton Instance
    //    {
    //        get
    //        {
    //            if (instance == null) //처음 접근했을 때 new하기
    //            {
    //                instance = new TestSingleton();   //MonoBehaviour는 new를 받을 수 없다.?
    //            }
    //            return instance;  //항상 리턴 될 때 값을 존재한다.
    //        }
    //    }

    //    // 중복생성 방지 목적
    //    //private으로 생성자를 만들어 기본 public생성자가 생성되지 않게 막기
    //    private TestSingleton()
    //    {

    //    }
}
