using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraShake))]
public class CameraFollow : MonoBehaviour {
    public PlayerController target;
    private Transform _transform;
    private CameraShake cameraShake;

    public Vector3 focusAreaSize;
    private FocusArea focusArea;
    public float verticalOffset;

    public float lookAheadDist;
    public float lookSmoothTime;

    private float offset;
    private float targetLookAhead;
    private float currentLookAhead;
    private float lookAheadDir;
    private float smoothLookVelocity;
    private bool lookAheadStopped;

    void Start ()
    {
        _transform = GetComponent<Transform>();
        cameraShake = GetComponent<CameraShake>();
        if (target != null)
        {
            offset = _transform.position.z - target.transform.position.z;
            focusArea = new FocusArea(target.GetComponent<Collider>().bounds, focusAreaSize);
            PlayerController.OnTakeDamage += ScreenShake;
        }
    }
	
	void LateUpdate ()
    {
        if(target != null)
        {

            focusArea.Update(target.GetComponent<Collider>().bounds);

            Vector3 focusPosition = focusArea.center + Vector3.up * verticalOffset;

            if (focusArea.velocity.x != 0)
            {
                lookAheadDir = Mathf.Sign(focusArea.velocity.x);
                if (Mathf.Sign(target.direction) == lookAheadDir && focusArea.velocity.x != 0)
                {
                    lookAheadStopped = false;
                    targetLookAhead = lookAheadDir * lookAheadDist;
                }
                else if (!lookAheadStopped)
                {
                    lookAheadStopped = true;
                    targetLookAhead = currentLookAhead + (lookAheadDir * lookAheadDist - currentLookAhead) / 4f;
                }
            }
            currentLookAhead = Mathf.SmoothDamp(currentLookAhead, targetLookAhead, ref smoothLookVelocity, lookSmoothTime);

            focusPosition += Vector3.right * currentLookAhead;

            transform.position = focusPosition + Vector3.forward * offset;

        }
	}

    void ScreenShake()
    {
        cameraShake.shakeDuration = 0.2f;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.center, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector3 center;
        public Vector3 velocity;
        float left, right;
        float top, bottom;
        float near, far;

        public FocusArea(Bounds bounds, Vector3 size)
        {
            left = bounds.center.x - size.x / 2;
            right = bounds.center.x + size.x / 2;
            top = bounds.min.y + size.y;
            bottom = bounds.min.y;
            near = bounds.min.z;
            far = bounds.min.z + size.z; 

            velocity = Vector3.zero;
            center = new Vector3((left + right) / 2, (top + bottom) / 2, (near + far) / 2);
        }

        public void Update(Bounds target)
        {
            float shiftX = 0;
            if (target.min.x < left)
            {
                shiftX = target.min.x - left;
            }
            else if (target.max.x > right)
            {
                shiftX = target.max.x - right;
            }

            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (target.min.y < bottom)
            {
                shiftY = target.min.y - bottom;
            }
            else if (target.max.y > top)
            {
                shiftY = target.max.y - top;
            }

            bottom += shiftY;
            top += shiftY;

            float shiftZ = 0;
            if (target.min.z < near)
            {
                shiftZ = target.min.z - near;
            }
            else if (target.max.z > far)
            {
                shiftZ = target.max.z - far;
            }

            near += shiftZ;
            far += shiftZ;

            center = new Vector3((left + right) / 2, (top + bottom) / 2, (near + far) / 2);
            velocity = new Vector3(shiftX, shiftY, shiftZ);
        }
    }
}
