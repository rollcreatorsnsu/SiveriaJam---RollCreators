using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenu : MonoBehaviour
{
    public string menuSceneName = "Menu";
    public GameObject settingsPanel;

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
    }
    
    public void Mute(bool isOn)
    {
        AudioListener.volume = isOn ? 1.0f : 0.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
