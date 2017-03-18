using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject follow;
    private Transform _transform;
    private float offset;
    private PlayerController playerController;

    void Start ()
    {
        _transform = GetComponent<Transform>();
        if (follow != null)
        {
            offset = _transform.position.z - follow.transform.position.z;
            playerController = follow.GetComponent<PlayerController>();
        }
	}
	
	void LateUpdate ()
    {
        if(follow != null)
        {
            _transform.position = new Vector3
            (
                _transform.position.x,
                _transform.position.y,
                follow.transform.position.z + offset
            );
        }
	}


}
