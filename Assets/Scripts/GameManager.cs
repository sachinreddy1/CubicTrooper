using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameEnded;
    public GameObject gameOverUI;

    void Start () {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
            return;

        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if (PlayerStats.Lives <= 0) {
            EndGame();
        }
    }

    public void EndGame () {
        gameEnded = true;
        Debug.Log("GAME OVER.");
        gameOverUI.SetActive(true);
    }
}
