using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public DoorBase target;

    Transform KeyModel;

    private void Awake()
    {
        KeyModel = transform.GetChild(0);
    }

    
    private void FixedUpdate()
    {
        Ymove();
    }

    private void Ymove()
    {
        KeyModel.transform.rotation = Quaternion.Euler(0f, 1f, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }



}
