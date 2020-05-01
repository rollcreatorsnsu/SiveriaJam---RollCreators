using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public enum DayTime
    {
        DAY,
        NIGHT
    }
    
    public int sinners;
    public int gold;
    public int attention;
    public int agentsDay;
    public int agentsNight;
    public Text sinnersText;
    public Text goldText;
    public Text attentionText;
    public Text agentsText;
    public GameObject dayDoingsPanel;
    public GameObject nightDoingsPanel;
    private DayTime dayTime = DayTime.DAY;

    void Update()
    {
        sinnersText.text = $"Sinners: {sinners}";
        goldText.text = $"Gold: {gold}";
        attentionText.text = $"Attention: {attention}";
        agentsText.text = $"Agents: {(dayTime == DayTime.DAY ? agentsDay : agentsNight)}";
    }

    public void ChangeDayTime()
    {
        if (dayTime == DayTime.DAY)
        {
            dayDoingsPanel.SetActive(false);
            nightDoingsPanel.SetActive(true);
            dayTime = DayTime.NIGHT;
        }
        else
        {
            dayDoingsPanel.SetActive(true);
            nightDoingsPanel.SetActive(false);
            dayTime = DayTime.DAY;
        }
    }
    
}
