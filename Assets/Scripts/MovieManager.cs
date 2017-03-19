using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieManager : MonoBehaviour {
    private MovieTexture movieText;

	void Start () {
        movieText = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        movieText.Play();
    }
	
	void Update () {
		if(!movieText.isPlaying)
        {
            LevelManager.GetInstance().GoToMenu();
        }
	}
}
