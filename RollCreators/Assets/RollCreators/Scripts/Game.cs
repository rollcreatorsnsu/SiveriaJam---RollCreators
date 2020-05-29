using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            float delta = value - _gold;
            if (daysHighGold > 0)
            {
                delta += delta * highGoldLevel;
            }
            _gold += delta;
        }
    }

    private float _attention;

    public float attention
    {
        get => _attention;
        set
        {
            float delta = value - _attention;
            if (daysLowAttention > 0)
            {
                delta -= delta * lowAttentionLevel;
            }
            _attention += delta;
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
                SceneManager.LoadScene(badOutroSceneName);
                return;
            }
            _daysRemained = value;
       }
    }
    
    [HideInInspector] public int daysLowAttention = 0;
    [HideInInspector] public float lowAttentionLevel = 0;
    [HideInInspector] public int daysHighGold = 0;
    [HideInInspector] public float highGoldLevel = 0;

    [HideInInspector] public Dictionary<Sinner.SocialStatus, Sinner> sinners = new Dictionary<Sinner.SocialStatus, Sinner>();
    [HideInInspector] public List<DayAgent> dayAgents = new List<DayAgent>();
    [HideInInspector] public List<NightAgent> nightAgents = new List<NightAgent>();

    [SerializeField] private GameObject dayPrefabs;
    [SerializeField] private GameObject nightPrefabs;

    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private IndulgenceMenu indulgenceMenu;
    [SerializeField] private AudioSource audioSource;

    public DayTime dayTime = DayTime.DAY;
    public string badOutroSceneName = "Outro_bad_2";
    public string goodOutoSceneName = "Outro_good";
    public delegate float Aim();

    public List<Aim> currentAims = new List<Aim>();

    void Start()
    {
        currentAims.Add(Aim1);
        currentAims.Add(Aim2);
        Agent.ClearBusyNames();
        for (int i = 0; i < 4; i++)
        {
            dayAgents.Add(new DayAgent());
            nightAgents.Add(new NightAgent());
        }

        foreach (Sinner.SocialStatus status in Enum.GetValues(typeof(Sinner.SocialStatus)))
        {
            sinners.Add(status, new Sinner(status));
        }

        attention = 0;
        gold = 0;
        daysRemained = 14;
        gameMenu.UpdateData();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameMenu.ShowSettings();
        }
    }

    public void ChangeDayTime()
    {
        if (dayTime == DayTime.DAY)
        {
            foreach (DayAgent agent in dayAgents)
            {
                agent.DoTask(this);
            }
            foreach (DayAgent agent in dayAgents)
            {
                agent.task = DayAgent.DayTask.IDLE;
            }
            dayTime = DayTime.NIGHT;
            dayPrefabs.SetActive(false);
            nightPrefabs.SetActive(true);
        }
        else
        {
            foreach (NightAgent agent in nightAgents)
            {
                agent.DoTask(this);
            }
            foreach (NightAgent agent in nightAgents)
            {
                agent.task = NightAgent.NightTask.IDLE;
                if (agent.daysHighSkill > 0)
                {
                    agent.daysHighSkill--;
                }

                if (agent.perks.Contains(Agent.Perks.PERK_6))
                {
                    agent.experience++;
                }
            }

            foreach (DayAgent agent in dayAgents)
            {
                if (agent.perks.Contains(Agent.Perks.PERK_6))
                {
                    agent.experience++;
                }
            }
            dayTime = DayTime.DAY;
            foreach (Sinner sinner in sinners.Values)
            {
                sinner.MorningUpdate();
            }

            if (daysLowAttention > 0)
            {
                daysLowAttention--;
            }

            daysRemained--;
            if (attention >= 100)
            {
                attention = 100;
                indulgenceMenu.ShowIndulgenceDropDown();
            }
            dayPrefabs.SetActive(true);
            nightPrefabs.SetActive(false);
        }
        gameMenu.UpdateData();
        audioSource.Play();
        if (gold >= Aim2())
        {
            SceneManager.LoadScene(goodOutoSceneName);
        }
    }

    private float Aim1()
    {
        return 1000;
    }

    private float Aim2()
    {
        return 2000;
    }
    
}
