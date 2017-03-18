
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private static int previousScene = 1;
    public static string menuScene = "Menu"; 
    public static string gameOverScene = "GameOver";

    private static LevelManager instance = null;
    
    void Awake()
    {
        instance = this;
    }

    public static LevelManager GetInstance()
    {
        return instance;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver()
    {
        previousScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(gameOverScene);
    } 

    public void LastScene()
    {
        if(previousScene != -1)
        {
            SceneManager.LoadScene(previousScene);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
