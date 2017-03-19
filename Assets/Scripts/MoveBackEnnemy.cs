using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackEnnemy : Entity {
    public GameObject player;
    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate;
    public float weaponRange;
    private float nextFire;

    /* IA */
    private Transform targetPlayer;
    private Transform IA;
    float f_MoveSpeed = 2.0f;

    void Awake()
    {
        IA = transform;
    }

    void Start()
    {
        targetPlayer = player.transform;
    }

    void Update ()
    {
        if (player != null && shot != null)
        {
            /* IA */
            IA.position += IA.forward * f_MoveSpeed * Time.deltaTime;

            if (IA.position.z - player.transform.position.z <= weaponRange && Time.time > nextFire)
            {
                /* FIRE */
                nextFire = Time.time + fireRate;

                GameObject projectile = Instantiate(shot, shotSpawn.position, Quaternion.identity, IA);

                float distance = Vector3.Distance(IA.position, targetPlayer.position);
                float travelTime = distance / projectile.GetComponent<SimpleProjectile>().speed;
                Vector3 aimPoint = targetPlayer.position + player.GetComponent<Rigidbody>().velocity * travelTime;

                projectile.transform.LookAt(aimPoint);
                projectile.GetComponent<SimpleProjectile>().creator = gameObject;
            }
        }
    }
}