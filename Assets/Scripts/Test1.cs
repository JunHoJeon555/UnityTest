using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test1 : MonoBehaviour
{
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {   
        inputActions.Player.Enable();
        inputActions.Player.Fire.performed += OnFire;
        inputActions.Player.Bomb.performed += OnBomb;
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    

    private void OnDisable()
    {
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Bomb.performed -= OnBomb;
        inputActions.Player.Fire.performed -= OnFire;
        inputActions.Player.Disable();
    }





    void Start()
    {
        Debug.Log("Start");
    }

    
    void Update()
    {
        
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }
    private void OnBomb(InputAction.CallbackContext context)
    {
        Debug.Log("Bomb");
    }

    private void OnMove(InputAction.CallbackContext comtext)
    {
        

    }
}
