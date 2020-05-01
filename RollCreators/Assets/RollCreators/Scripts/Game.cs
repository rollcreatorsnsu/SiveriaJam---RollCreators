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
    
    public float gold;
    public int attention;

    [HideInInspector] public List<Sinner> sinners = new List<Sinner>();
    [HideInInspector] public List<DayAgent> dayAgents = new List<DayAgent>();
    [HideInInspector] public List<NightAgent> nightAgents = new List<NightAgent>();
    
    [SerializeField] private Text sinnersText;
    [SerializeField] private Text goldText;
    [SerializeField] private Text attentionText;
    [SerializeField] private Text agentsText;
    [SerializeField] private Text changeDayTimeText;

    [SerializeField] private AgentMenu agentMenu;

    [SerializeField] private GameObject dayAgentPrefab;
    [SerializeField] private GameObject nightAgentPrefab;

    public DayTime dayTime = DayTime.DAY;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            dayAgents.Add(Instantiate(dayAgentPrefab).GetComponent<DayAgent>());
            nightAgents.Add(Instantiate(nightAgentPrefab).GetComponent<NightAgent>());
        }
    }

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
            foreach (Sinner sinner in sinners)
            {
                sinner.Hide();
            }
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
            foreach (Sinner sinner in sinners)
            {
                sinner.Update();
            }
            if (attention >= 100)
            {
                agentMenu.ShowIndulgenceDropDown();
            }
        }
    }
    
}
