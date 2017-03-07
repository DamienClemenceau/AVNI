using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleProjectile : MonoBehaviour
{
    public float speed;
    public LayerMask targetMask;
    public GameObject creator;

	void Start ()
    {
        if (creator == null)
        {
            creator = new GameObject();
        }
	}

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Boundary" && creator != other.gameObject) { 
            if (targetMask == (targetMask | 1<<other.gameObject.layer))
            {
                Entity entity = other.GetComponent<Entity>();
                entity.TakeDamage();
            }
            Destroy(gameObject);
        }
    }
}
