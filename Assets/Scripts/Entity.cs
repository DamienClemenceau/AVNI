using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected float maxLife = 1;
    public float life = 1;
    public int scoreValue;
    protected GameManager gameManager;

    void Awake()
    {
        maxLife = life;
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
    }

    public virtual void TakeDamage()
    {
        life--;
        if(life <= 0)
        {
            if(gameManager != null)
            {
                gameManager.score.ennemyDestroyed += 1;
                gameManager.AddScore(scoreValue);
            }
            Destroy(gameObject);
        }
    }
}
