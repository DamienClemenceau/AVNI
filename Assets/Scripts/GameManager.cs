using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private int score;
    private bool gameOver, levelFinished;

    public GameObject gameOverUI;
    public ScoreUI scoreUI;
    public PlayerController player;

    void Update()
    {
        if (gameOver)
        {
            gameOverUI.SetActive(true);
        }
        if(levelFinished)
        {
            scoreUI.UpdateScore(score);
            scoreUI.Appear();
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        player.AddMana(score);
    }

    public void GameOver()
    {
        gameOver = true;
    }

    public void LevelFinish()
    {
        levelFinished = true;
    }
}
