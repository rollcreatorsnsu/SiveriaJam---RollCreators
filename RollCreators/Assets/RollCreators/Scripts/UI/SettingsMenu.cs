using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public string menuSceneName = "Menu";
    public ConfirmationMenu confirmationMenu;

    public void Mute(bool isOn)
    {
        AudioListener.volume = isOn ? 1.0f : 0.0f;
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
}
