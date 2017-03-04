using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public int depth;
    public Boundary boundary;

	void Start ()
    {
        Cursor.visible = false;
	}
	
	void Update ()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 wantedPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
        transform.position = new Vector3
        (
            Mathf.Clamp(wantedPosition.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(wantedPosition.y, boundary.yMin, boundary.yMax),
            transform.position.z
        );
    }
}
