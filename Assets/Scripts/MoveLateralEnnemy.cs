using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLateralEnnemy : Entity {
    public GameObject player;
    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate;
    public float weaponRange;
    private float nextFire;

    public int x_deplacement;
    public float delay_deplacement;
    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", x_deplacement, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", delay_deplacement));
    }

    void Update ()
    {
        if (player != null && shot != null)
        {
            if (transform.position.z - player.transform.position.z <= weaponRange && Time.time > nextFire)
            {
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
