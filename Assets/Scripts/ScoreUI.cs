using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour {
    public GUIText scoreUI;

    void Start()
    {
        transform.position = Vector3.left * 3;
    }

    public void UpdateScore(int score)
    {
        scoreUI.text = score.ToString();
    }

    public void Appear()
    {
        transform.position = Vector3.zero;
    }
}
