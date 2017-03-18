using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour {
    public GUIText scoreUI;
    private float progress;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position = Vector3.left * 4;
    }

    public void UpdateScore(int score)
    {
        scoreUI.text = score.ToString();
    }

    public void Appear()
    {
        transform.position = new Vector3(
            Mathf.Lerp(startPosition.x, 0, progress),
            transform.position.y,
            transform.position.z
        );

        if(progress <= 1)
        {
            progress += 0.08f;
        }
    }
}
