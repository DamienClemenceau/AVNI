using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowardEnnemy : Entity {
    public float speed;
    public Vector3 axis;

    void FixedUpdate()
    {
        transform.Translate(axis * speed * Time.deltaTime);
    }
}
