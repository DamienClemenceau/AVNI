using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashToPlayerEnnemy : Entity {
    public GameObject player;
    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate;
    public float weaponRange;
    private float nextFire;

    /* IA */
    Transform tr_Player;
    float f_RotSpeed = 2.0f, f_MoveSpeed = 0.5f;

    void Start()
    {
        tr_Player = GameObject.FindGameObjectWithTag("Demon").transform;
    }

    void Update ()
    {
        if (player != null && shot != null)
        {
            /* IA */
            /* Look at Player*/
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (tr_Player.position - transform.position), f_RotSpeed * Time.deltaTime);
            /* Move at Player*/
            transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
 

            if (transform.position.z - player.transform.position.z <= weaponRange && Time.time > nextFire)
            {
                /* FIRE */
                nextFire = Time.time + fireRate;

                GameObject projectile = Instantiate(shot, shotSpawn.position, Quaternion.identity, transform);

                float distance = Vector3.Distance(transform.position, player.transform.position);
                float travelTime = distance / projectile.GetComponent<SimpleProjectile>().speed;
                Vector3 aimPoint = player.transform.position + player.GetComponent<Rigidbody>().velocity * travelTime;

                projectile.transform.LookAt(aimPoint);
                projectile.GetComponent<SimpleProjectile>().creator = gameObject;
            }
        }
    }
}