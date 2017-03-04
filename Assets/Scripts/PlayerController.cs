using UnityEngine;

[System.Serializable] 
public class Boundary
{
    public float xMax, xMin, yMax, yMin;
}

[System.Serializable]
public class Speed
{
    public float lateral, foward;
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float tilt;
    public Speed speed;
    public Boundary boundary;
    public float deadZone;

    public LayerMask mask;
    public Transform pointer;
    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate;
    public float weaponRange;

    private Rigidbody rb;
    private Transform _transform;
    private GameManager gameManager;
    private float direction;
    private float currentVelocity;
    private float nextFire;

    public int life;

    void Start()
    {
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, weaponRange, mask))
            {
                GameObject projectile = Instantiate(shot, shotSpawn.position, Quaternion.identity, _transform);
                projectile.transform.LookAt(hit.point);
                projectile.GetComponent<SimpleProjectile>().creator = gameObject;

                Debug.DrawRay(ray.origin, hit.point, Color.blue, 1.0f);
            }

        }

        if(life <= 0)
        {
            gameManager.GameOver();
        }
    }

    void OnDestroy()
    {
        gameManager.GameOver();
    }

	void FixedUpdate()
    {
        if(pointer.position.x - deadZone > _transform.position.x)
        {
            direction = 1;
        }
        else if (pointer.position.x + deadZone < _transform.position.x)
        {
            direction = -1;
        } else
        {
            direction = 0;
        }

        float targetXMovement = direction * speed.lateral;
        float xMovement = Mathf.SmoothDamp(rb.velocity.x, targetXMovement, ref currentVelocity, 0.1f);
        rb.velocity = new Vector3(xMovement, Mathf.Sin(Time.time) * Time.deltaTime, speed.foward);
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
            rb.position.z
        );
        rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
	}
}
