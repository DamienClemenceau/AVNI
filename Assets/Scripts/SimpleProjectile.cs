using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleProjectile : MonoBehaviour
{
    public float speed;
    public LayerMask targetMask;
    public GameObject creator;
    private GameManager gameManager;

	void Start ()
    {
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        if(gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        if (creator == null)
        {
            creator = new GameObject();
        }
	}

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Boundary" && creator != other.gameObject) { 
            if (targetMask == (targetMask | 1<<other.gameObject.layer))
            {
                Destroy(other.gameObject);
                gameManager.AddScore(1);
            }
            Destroy(gameObject);
        }
    }
}
