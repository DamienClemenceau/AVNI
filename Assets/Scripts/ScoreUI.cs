using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ScoreInfo
{
    public float score;
    public float hitTaken;
    public float ennemyDestroyed, ennemyTotal;
    public string sceneTitle;
    public string sceneSubtitle;
}

public class ScoreUI : MonoBehaviour {
    public Text scoreUI;
    public Text hitTakenUI;
    public Text ennemyDestroyUI;
    public Text sceneTitle;
    public Text sceneNumber;

    public void UpdateScoreScreen(ScoreInfo score)
    {
        scoreUI.text = score.score.ToString();
        hitTakenUI.text = score.hitTaken.ToString();
        ennemyDestroyUI.text = score.ennemyDestroyed.ToString() + "/" + score.ennemyTotal.ToString();
        sceneTitle.text = score.sceneTitle;
        sceneNumber.text = score.sceneSubtitle;
    }

    public void Appear()
    {
        gameObject.SetActive(true);
    }
}
