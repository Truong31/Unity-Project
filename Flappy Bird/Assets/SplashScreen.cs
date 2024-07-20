using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public string mainScreen = "SampleScene";
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startScene()
    {
        audioSource.Stop();
        SceneManager.LoadScene(mainScreen);
    }
    
}
