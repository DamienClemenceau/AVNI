using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraShake))]
public class CameraFollow : MonoBehaviour {
    public GameObject follow;
    private Transform _transform;
    private float offset;
    private PlayerController playerController;
    private CameraShake cameraShake;

    void Start ()
    {
        _transform = GetComponent<Transform>();
        cameraShake = GetComponent<CameraShake>();
        if (follow != null)
        {
            offset = _transform.position.z - follow.transform.position.z;
            playerController = follow.GetComponent<PlayerController>();

            PlayerController.OnTakeDamage += ScreenShake;
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

    void ScreenShake()
    {
        cameraShake.shakeDuration = 0.2f;
    }


}
