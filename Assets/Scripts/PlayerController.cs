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

public delegate void PlayerAction();

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Entity
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

    public int maxMana = 10;
    public int manaShieldConsuption = 5;
    private int mana;
    private bool shielded;

    [HideInInspector]
    public bool canShoot = true;

    private Rigidbody rb;
    private Transform _transform;
    private float currentVelocity;
    private float nextFire;

    public Texture2D damageTexture;
    public GameObject shieldEffect;

    [HideInInspector]
    public float direction;

	public AudioClip hit;
	public AudioClip shoot;
	public AudioClip fly;
	public AudioClip rage;

	private AudioSource[] sounds;
    private GameObject shieldObject;
    private float downTime;
    private float shieldTime = 2;

    public static event PlayerAction OnTakeDamage;

    void Start()
    {
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        mana = maxMana;
        /*
		sounds = GetComponents<AudioSource> ();
		sounds [0].clip = fly;
		sounds [0].loop = true;
		sounds [0].Play ();
        */
    }

    void OnGUI()
    {
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Mathf.Abs(life / maxLife - 1));
        GUI.depth = -10;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), damageTexture);
    }

    void Update()
    {
        if (Input.GetButton("Fire2") && mana >= manaShieldConsuption)
        {
            if(!shielded)
            {
                mana -= manaShieldConsuption;
                shieldObject = Instantiate(shieldEffect, shotSpawn.position, Quaternion.identity, transform);
                downTime = Time.time  + shieldTime;
            }
            shielded = true;
            Debug.Log((!Input.GetButton("Fire2") && shielded) || (Time.time > downTime && shielded));
            /*
			sounds [1].clip = rage;
			sounds [1].Play ();
            */
            if(Time.time > downTime)
            {
                shielded = false;
                Destroy(shieldObject);
            }
        }
        else if((!Input.GetButton("Fire2") && shielded))
        {
            shielded = false;
            Destroy(shieldObject);
        }
        else if(Input.GetButton("Fire1") && Time.time > nextFire && !shielded)
        {
            nextFire = Time.time + fireRate;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 origin = ray.origin;
            origin.z = transform.position.z;
            ray.origin = origin;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                GameObject projectile = Instantiate(shot, shotSpawn.position, Quaternion.identity, _transform);
                projectile.transform.LookAt(hit.point);
                projectile.GetComponent<SimpleProjectile>().creator = gameObject;

                Debug.DrawRay(ray.origin, hit.point, Color.blue, 1.0f);
                /*
                sounds [1].clip = shoot;
				sounds [1].Play ();
                */
            }
        }
    }

    public override void TakeDamage()
    {
        if(!shielded)
        { 
			/*
			sounds [1].clip = hit;
			sounds [1].Play ();
            */
            life--;
            gameManager.score.hitTaken++;
            if (OnTakeDamage != null)
            {
                OnTakeDamage();
            }


            if (life <= 0)
            {
                StartCoroutine(gameManager.GameOver());
            }
        }
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

        rb.rotation = Quaternion.Euler(60, rb.rotation.y, rb.velocity.x * -tilt);
    }

    public void AddMana(int mana)
    {
        this.mana += mana;
    }
}
