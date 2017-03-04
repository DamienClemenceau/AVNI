using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStationnaryEnnemy : MonoBehaviour {
    public GameObject player;
    public Transform shotSpawn;
    public GameObject shot;
    public float fireRate;
    public float weaponRange;
    private float nextFire;
	
	void Update ()
    {
        if(player != null)
        {
            if (transform.position.z - player.transform.position.z <= weaponRange && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                GameObject projectile = Instantiate(shot, shotSpawn.position, Quaternion.identity, transform);
                projectile.transform.LookAt(player.transform.position);
                projectile.GetComponent<SimpleProjectile>().creator = gameObject;
            }
        }
    }
}
