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

    private float _gold;

    public float gold
    {
        get => _gold;
        set
        {
            gameMenu.goldText.text = $"Золото {value}";
            _gold = value;
        }
    }

    private int _attention;

    public int attention
    {
        get => _attention;
        set
        {
            gameMenu.attentionText.text = $"Внимание {value}/100";
            _attention = value;
        }
    }

    [HideInInspector] public Dictionary<Sinner.SocialStatus, Sinner> sinners = new Dictionary<Sinner.SocialStatus, Sinner>();
    [HideInInspector] public List<DayAgent> dayAgents = new List<DayAgent>();
    [HideInInspector] public List<NightAgent> nightAgents = new List<NightAgent>();

    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private IndulgenceMenu indulgenceMenu;

    [SerializeField] private GameObject dayAgentPrefab;
    [SerializeField] private GameObject nightAgentPrefab;
    [SerializeField] private GameObject sinnerPrefab;

    public DayTime dayTime = DayTime.DAY;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            dayAgents.Add(Instantiate(dayAgentPrefab).GetComponent<DayAgent>());
            nightAgents.Add(Instantiate(nightAgentPrefab).GetComponent<NightAgent>());
        }

        foreach (Sinner.SocialStatus status in Enum.GetValues(typeof(Sinner.SocialStatus)))
        {
            sinners.Add(status, Instantiate(sinnerPrefab).GetComponent<Sinner>());
        }

        attention = 0;
        gold = 0;
    }

    public void ChangeDayTime()
    {
        if (dayTime == DayTime.DAY)
        {
            foreach (Sinner sinner in sinners.Values)
            {
                sinner.Hide();
            }
            foreach (DayAgent agent in dayAgents)
            {
                agent.DoTask(this);
            }
            dayTime = DayTime.NIGHT;
        }
        else
        {
            foreach (NightAgent agent in nightAgents)
            {
                agent.DoTask(this);
            }
            dayTime = DayTime.DAY;
            foreach (Sinner sinner in sinners.Values)
            {
                sinner.MorningUpdate();
            }
            if (attention >= 100)
            {
                indulgenceMenu.ShowIndulgenceDropDown();
            }
        }
    }
    
}
