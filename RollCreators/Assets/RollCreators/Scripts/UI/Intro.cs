using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public string menuSceneName = "Menu";

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
