using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public ScoreInfo score;
    private bool gameOver, levelFinished;
    private float timerTransition;
    private float timeTillTransition = 5;
    
    public ScoreUI scoreUI;
    public PlayerController player;

    void Update()
    {
        if(levelFinished)
        {
            scoreUI.UpdateScoreScreen(score);
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
        LevelManager.GetInstance().NextScene();
    }

    public void AddScore(int score)
    {
        this.score.score += score;
        player.AddMana(score);
    }

    public IEnumerator GameOver()
    {
        float time = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(time);
        LevelManager.GetInstance().GameOver();
    }

    public void LevelFinish()
    {
        levelFinished = true;
        timerTransition = Time.time;
    }
}
