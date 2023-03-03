using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 기본 원리
// 자식으로 붙어있는 BG_Slot이 계속 왼쪽으로 움직임
// 하나의 BG_Slot이 화면에 보이지 않을 때까지 왼쪽으로 이동하면 BG_Slot의 가장 오른쪽으로 이동 시킴


public class Scroller : MonoBehaviour
{
    //자식으로 붙어있는 BG_Slot이 계속 왼쪽으로 움직임
    //하나의 BG_Slot이 화면에 보이지 않을 때까지 왼쪽으로 이동하면 BG_Slot의 가장 오른쪽으로 이동시킴

    //이동속도 
    public float scrollingSpeed = 2.5f;
    //배경이미지가 두개 불어있는 슬롯
    Transform[] bgSlots = null;
    //이미지 길이
    float slot_Width = 13.6f;

    
    protected virtual void Awake()
    {   
        
        bgSlots = new Transform[transform.childCount]; //슬롯이 들어갈 배열 생성. 
        for (int i = 0; i<transform.childCount; i++)    
        {
            bgSlots[i] = transform.GetChild(i);     
        }

        slot_Width = bgSlots[1].position.x - bgSlots[0].position.x; //이미지 한변의 길이 계산
        

    }

    private void Update()
    {
        //아래 foreach와 같은 코드. 하지만 foreach가 더 빠르다.
        for (int i = 0; i < bgSlots.Length; i++)
        {
            Transform slot = bgSlots[i];
            slot.Translate(Time.deltaTime * scrollingSpeed * -transform.right);
            if (slot.localPosition.x < 0)                         //슬롯이 부모위치보다 왼쪽으로 갔을 때
            {
                MoveRightEnd(i);
            }
                
        }


        //foreach (Transform slot in bgSlots)// 모든 슬롯을 돌면서
        //{
        //    slot.Translate(Time.deltaTime * scrollingSpeed * -transform.right); //슬롯을 왼쪽으로 이동시키고
        //    if(slot.localPosition.x <0)                         //슬롯이 부모위치보다 왼쪽으로 갔을 때
        //    {
                        //      슬롯을 오른쪽 끝으로 이동 시키기
        //        slot.Translate(slot_Width * bgSlots.Length * transform.right);
        //    }
        //}


    }


    protected virtual void MoveRightEnd(int index)
    {
        bgSlots[index].Translate(slot_Width * bgSlots.Length * transform.right);
    }

}
