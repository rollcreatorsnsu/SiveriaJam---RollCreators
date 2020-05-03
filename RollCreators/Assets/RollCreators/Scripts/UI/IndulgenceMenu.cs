using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IndulgenceMenu : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public Game game;
    public string badOutroSceneName = "Outro_bad_1";
    
    public void GetIndulgenceListener()
    {
        if (game.gold >= 5000 * (slider.value))
        {
            game.gold -= 5000 * (slider.value);
            game.attention -= 10 * ((int) slider.value);
            gameObject.SetActive(false);
        }

        if (game.attention >= 100)
        {
            SceneManager.LoadScene(badOutroSceneName);
        }
    }

    public void TextUpdate()
    {
        text.text = $"Убрать {10 * (slider.value)} внимания за {5000 * (slider.value)} золота";
    }
    public void ShowIndulgenceDropDown()
    {
        slider.minValue = 1;
        slider.maxValue = 10;
        gameObject.SetActive(true);
    }
}
