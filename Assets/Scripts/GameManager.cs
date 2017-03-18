using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private int score;
    private bool gameOver, levelFinished;
    private float timerTransition;
    private float timeTillTransition = 5;

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
            player.canShoot = false;

            if (timeTillTransition + timerTransition <= Time.time)
            {
                StartCoroutine(NextScene());
            }
        }
    }

    IEnumerator NextScene()
    {
        float time = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        timerTransition = Time.time;
    }
}
