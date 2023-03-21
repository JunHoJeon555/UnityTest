using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAuto : DoorBase
{
    //자동문 


    private void OnTriggerEnter(Collider other)
    {
        //레이어를 사용해서 플레이어인지 아닌지 확인 할 필요x

        OnOpen();

    }



    private void OnTriggerExit(Collider other)
    {
        OnClose();
    }


}
