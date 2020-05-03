using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public string menuSceneName = "Menu";
    public AudioSource story;
    
    public void BeginStory()
    {
        story.Play();
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
