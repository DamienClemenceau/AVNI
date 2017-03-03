using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 20;
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
