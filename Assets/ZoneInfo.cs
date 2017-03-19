using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneInfo : MonoBehaviour {
    public RectTransform up, down;
    public RectTransform textPosition;
    public float timeInOut;
    public float visibleTime;
    private int dir = -1;
    private float progress;
    public string title;
    private float timeToEnd;

    void OnTriggerEnter(Collider collider)
    {
        dir = 1;
        textPosition.GetComponent<Text>().text = title;
        timeToEnd = visibleTime + Time.time; 
    }

    void FixedUpdate()
    {
        progress += dir * timeInOut * Time.deltaTime;
        progress = Mathf.Clamp01(progress);

        up.anchoredPosition = Vector2.up * Mathf.Lerp(40, -40, progress);
        down.anchoredPosition = Vector2.down * Mathf.Lerp(40, -40, progress);
        textPosition.anchoredPosition = new Vector2(Mathf.Lerp(1000 * -dir, 0, progress), 45);

        if(timeToEnd < Time.time && dir == 1)
        {
            dir = -1;
        }
    }
}
