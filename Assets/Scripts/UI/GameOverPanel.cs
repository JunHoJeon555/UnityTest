using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.onDie += ShowPanel;
        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void ShowPanel()
    {
        gameObject.SetActive(true);
    }

}
