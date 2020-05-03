using System;
using System.Collections.Generic;
using UnityEngine;

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
            gameMenu.goldText.text = $"Золото {Mathf.Floor(value)}";
            _gold = value;
        }
    }

    private int _attention;

    public int attention
    {
        get => _attention;
        set
        {
            gameMenu.attentionText.text = $"{value}%\nвнимание инквизиции";
            _attention = value;
        }
    }

    private int _daysRemained;

    public int daysRemained
    {
        get => _daysRemained;
        set
        {
            if (value < 0)
            {
                gameOverPanel.SetActive(true);
                return;
            }
            gameMenu.daysRemainedText.text = $"Осталось {value} дней";
            _daysRemained = value;
        }
    }

    [HideInInspector] public Dictionary<Sinner.SocialStatus, Sinner> sinners = new Dictionary<Sinner.SocialStatus, Sinner>();
    [HideInInspector] public List<DayAgent> dayAgents = new List<DayAgent>();
    [HideInInspector] public List<NightAgent> nightAgents = new List<NightAgent>();

    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private IndulgenceMenu indulgenceMenu;
    [SerializeField] private GameObject gameOverPanel;

    public DayTime dayTime = DayTime.DAY;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            dayAgents.Add(new DayAgent());
            nightAgents.Add(new NightAgent());
        }

        foreach (Sinner.SocialStatus status in Enum.GetValues(typeof(Sinner.SocialStatus)))
        {
            sinners.Add(status, new Sinner());
        }

        attention = 0;
        gold = 0;
        daysRemained = 14;
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
                agent.task = DayAgent.DayTask.IDLE;
            }
            dayTime = DayTime.NIGHT;
        }
        else
        {
            foreach (NightAgent agent in nightAgents)
            {
                agent.DoTask(this);
                agent.task = NightAgent.NightTask.IDLE;
            }
            dayTime = DayTime.DAY;
            foreach (Sinner sinner in sinners.Values)
            {
                sinner.MorningUpdate();
            }

            daysRemained--;
            if (attention >= 100)
            {
                attention = 100;
                indulgenceMenu.ShowIndulgenceDropDown();
            }
        }
        gameMenu.UpdateDayTime();
    }
    
}
