using UnityEngine;

public class Controller : MonoBehaviour {
    public float speed = 5;
    public GameObject meteor;
    public GameObject projectileSpawnPoint;

    void Update()
    {
        Vector3 v = Vector3.forward * speed * Time.deltaTime;

        transform.Translate(v);


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                GameObject projectile = (GameObject)Instantiate(meteor, projectileSpawnPoint.transform.position, Quaternion.identity);
                projectile.transform.LookAt(hit.point);
            }
        }
    }
}
