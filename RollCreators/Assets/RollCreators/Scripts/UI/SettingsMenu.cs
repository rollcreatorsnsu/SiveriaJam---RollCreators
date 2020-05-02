using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public string menuSceneName = "Menu";

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

    public void ReturnToGame()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
