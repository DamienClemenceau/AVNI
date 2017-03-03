using UnityEngine;
using System.Collections;

public class Visor : MonoBehaviour {
    
	void Start () {
        Cursor.visible = false;
    }

    void Update () {
        transform.position = Input.mousePosition;
	}
}
