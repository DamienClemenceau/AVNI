using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
    public LayerMask mask;

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Demon")
        {
            Destroy(other.gameObject);
        }
    }
}
