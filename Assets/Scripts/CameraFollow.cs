using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform follow;
    private Transform _transform;
    private Vector3 offset;
	void Start ()
    {
        _transform = GetComponent<Transform>();
        offset = _transform.position - follow.position;
	}
	
	void LateUpdate ()
    {
        _transform.position = follow.position + offset;
	}
}
