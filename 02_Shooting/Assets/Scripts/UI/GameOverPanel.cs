using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{

    Animator anim;
    Button button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(onRestart);
    }

    private void onRestart()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.onDie += (_) => ShowPanel();           //플레이어의 onDie
        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void ShowPanel()
    {

        //gameObject.SetActive(true); //게임오버채널 전체 보이기

        anim.SetTrigger("GameOverStart");

    }



}
