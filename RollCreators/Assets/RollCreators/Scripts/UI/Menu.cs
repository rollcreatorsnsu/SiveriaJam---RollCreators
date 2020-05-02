using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string gameSceneName = "Game";
    
    public void _Start()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
