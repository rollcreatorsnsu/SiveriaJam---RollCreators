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
            gameMenu.goldText.text = $"{Mathf.FloorToInt(value)}/{(currentAim == 0 ? 1000 : 2000)}";
            _gold = value;
        }
    }
    
    private int _attention;

    public int attention
    {
        get => _attention;
        set
        {
            gameMenu.attentionText.text = $"{value}%";
            gameMenu.attentionSlider.value = value / 100f;
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
                SceneManager.LoadScene(badOutroSceneName);
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
    [SerializeField] private AudioSource audioSource;

    public DayTime dayTime = DayTime.DAY;
    public string badOutroSceneName = "Outro_bad_2";
    public string goodOutoSceneName = "Outro_good";
    private delegate float Aim();

    private string[] aimsTexts =
    {
        "накопить 1000 золота",
        "накопить 2000 золота"
    };
    private List<Aim> aims = new List<Aim>();
    private int currentAim = 0;

    void Start()
    {
        aims.Add(Aim1);
        aims.Add(Aim2);
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
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameMenu.ShowSettings();
        }
        if (currentAim < aims.Count)
        {
            UpdateAim();
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
            gameMenu.ShowResults();
            foreach (DayAgent agent in dayAgents)
            {
                agent.task = DayAgent.DayTask.IDLE;
            }
            dayTime = DayTime.NIGHT;
        }
        else
        {
            foreach (NightAgent agent in nightAgents)
            {
                agent.DoTask(this);
            }
            gameMenu.ShowResults();
            foreach (NightAgent agent in nightAgents)
            {
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
        audioSource.Play();
    }

    private void UpdateAim()
    {
        float progress = aims[currentAim]();
        if (progress >= 1f)
        {
            currentAim++;
            if (currentAim == aims.Count)
            {
                SceneManager.LoadScene(goodOutoSceneName);
                return;
            }
            gameMenu.UpdateAimText(aimsTexts[currentAim]);
        }
        gameMenu.UpdateAimBar(aims[currentAim]());
    }

    private float Aim1()
    {
        return gold / 1000f;
    }

    private float Aim2()
    {
        return gold / 2000f;
    }
    
}
