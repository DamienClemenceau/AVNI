using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieManager : MonoBehaviour
{
#if UNITY_WEBGL
    private WebGLMovieTexture movieText;
    public string videoPath;
    public float movieDuration;
    private float movieEnd;
#else
    private MovieTexture movieText;
#endif

    void Start()
    {
#if UNITY_WEBGL
        movieEnd = Time.time + movieDuration;

        movieText = new WebGLMovieTexture(videoPath);
        GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
        GetComponent<MeshRenderer>().material.mainTexture = movieText;
#else
        movieText = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        movieText.Play();
#endif
    }

    void Update()
    {
#if UNITY_WEBGL
        if(Time.time > movieEnd)
        {
            LevelManager.GetInstance().GoToMenu();
        }
#else
        if (!movieText.isPlaying)
        {
            LevelManager.GetInstance().GoToMenu();
        }
#endif
    }
}
