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
    [HideInInspector] public List<DayAgent> dayAgents = new List<DayAgent>();
    [HideInInspector] public List<NightAgent> nightAgents = new List<NightAgent>();
    
    [SerializeField] private Text sinnersText;
    [SerializeField] private Text goldText;
    [SerializeField] private Text attentionText;
    [SerializeField] private Text agentsText;
    [SerializeField] private Text changeDayTimeText;

    public DayTime dayTime = DayTime.DAY;

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
            foreach (DayAgent agent in dayAgents)
            {
                agent.DoTask(this);
            }
            changeDayTimeText.text = "Change Day";
            dayTime = DayTime.NIGHT;
        }
        else
        {
            foreach (NightAgent agent in nightAgents)
            {
                agent.DoTask(this);
            }
            changeDayTimeText.text = "Change Night";
            dayTime = DayTime.DAY;
        }
    }
    
}
