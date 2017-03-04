using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform follow;
    private Transform _transform;
    private float offset;
	void Start ()
    {
        _transform = GetComponent<Transform>();
        offset = _transform.position.z - follow.position.z;
	}
	
	void LateUpdate ()
    {
        _transform.position = new Vector3
        (
            _transform.position.x,
            _transform.position.y,
            follow.position.z + offset
        );
	}
}
