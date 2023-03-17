using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PollObject만 죽을 수 있게 만든다. Player가 죽는걸 방지

        PoolObject comp = collision.GetComponent<PoolObject>();
        if (comp != null)
        {
            //Debug.Log("Killzone");
            comp.gameObject.SetActive(false);
            
        }
    }


}
