using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public string menuSceneName = "Menu";
    public ConfirmationMenu confirmationMenu;
    public Text muteText;
    public Tutorial tutorial;

    public void Mute()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            muteText.text = "Выключить звук";
        }
        else
        {
            AudioListener.volume = 0;
            muteText.text = "Включить звук";
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitMenuCallback()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void ExitMenu()
    {
        confirmationMenu.Show("Вы действительно хотите выйти в меню?", ExitMenuCallback);
    }

    private void ExitGameCallback()
    {
        Application.Quit();
    }

    public void ExitGame()
    {
        confirmationMenu.Show("Вы действительно хотите выйти из игры?", ExitGameCallback);
    }

    public void ReturnToGame()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void BeginTutorial()
    {
        ReturnToGame();
        tutorial._Start();
    }

}
