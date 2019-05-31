using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;

    public GameObject gameOverUI;   // 게임 오버 UI

    private void Start()
    {
        gameIsOver = false;
    }

    private void Update()
    {
        if (gameIsOver) return;  // EndGame() 한번만 호출합시다..

        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true); // 게임오버되면 활성화
    }
}
