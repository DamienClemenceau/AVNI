using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Demon")
        {
            GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
            gameManagerObject.GetComponent<GameManager>().LevelFinish();
        }
    }
}
