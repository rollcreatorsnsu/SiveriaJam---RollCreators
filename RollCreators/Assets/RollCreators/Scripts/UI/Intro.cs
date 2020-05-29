using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public string menuSceneName = "Menu";
    public AudioSource story;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GoToMenu();
        }
    }
    
    public void BeginStory()
    {
        story.Play();
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
