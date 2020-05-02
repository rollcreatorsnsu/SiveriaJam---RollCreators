﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndulgenceMenu : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public Game game;
    public GameObject gameOverPanel;
    
    public void GetIndulgenceListener()
    {
        game.gold -= 5000 * (slider.value + 1);
        game.attention -= 10 * ((int)slider.value + 1);
        gameObject.SetActive(false);
        if (game.attention >= 100)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void TextUpdate()
    {
        text.text = $"Убрать {10 * (slider.value + 1)} внимания за {5000 * (slider.value + 1)} золота";
    }
    public void ShowIndulgenceDropDown()
    {
        slider.minValue = 1;
        slider.maxValue = 10;
        gameObject.SetActive(true);
    }
}
