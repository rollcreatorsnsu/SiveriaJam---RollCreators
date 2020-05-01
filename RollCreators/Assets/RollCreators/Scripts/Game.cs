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
    
    public int gold;
    public int attention;

    [HideInInspector] public List<Sinner> sinners = new List<Sinner>();
    [HideInInspector] public List<Agent> dayAgents = new List<Agent>();
    [HideInInspector] public List<Agent> nightAgents = new List<Agent>();
    
    [SerializeField] private Text sinnersText;
    [SerializeField] private Text goldText;
    [SerializeField] private Text attentionText;
    [SerializeField] private Text agentsText;
    [SerializeField] private Text changeDayTimeText;
    [SerializeField] private GameObject dayDoingsPanel;
    [SerializeField] private GameObject nightDoingsPanel;

    private DayTime dayTime = DayTime.DAY;

    void Update()
    {
        sinnersText.text = $"Sinners: {sinners.Count}";
        goldText.text = $"Gold: {gold}";
        attentionText.text = $"Attention: {attention}";
        agentsText.text = $"Agents: {(dayTime == DayTime.DAY ? dayAgents.Count : nightAgents.Count)}";
    }

    public void ChangeDayTime()
    {
        if (dayTime == DayTime.DAY)
        {
            changeDayTimeText.text = "Change Day";
            dayTime = DayTime.NIGHT;
        }
        else
        {
            changeDayTimeText.text = "Change Night";
            dayTime = DayTime.DAY;
        }
    }
    
}
