using UnityEngine;

public class Ennemy : MonoBehaviour {
    public void OnCollisionEnter(Collision collision)
    {
        Score.UpdateScore();
        DestroyObject(gameObject);
        DestroyObject(collision.gameObject);
    }
}
