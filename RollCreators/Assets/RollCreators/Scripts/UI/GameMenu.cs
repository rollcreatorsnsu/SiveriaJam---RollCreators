using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    private static Dictionary<DayAgent.DayTask, string> DAY_TASKS = new Dictionary<DayAgent.DayTask, string>();
    private static Dictionary<NightAgent.NightTask, string> NIGHT_TASKS = new Dictionary<NightAgent.NightTask, string>();
    private static bool __needTutorial = true;

    static GameMenu()
    {
        DAY_TASKS.Add(DayAgent.DayTask.IDLE, "бездействует");
        DAY_TASKS.Add(DayAgent.DayTask.CONDUCT_A_SERVICE, "проводит службу");
        DAY_TASKS.Add(DayAgent.DayTask.GIVE_ALMS, "раздает милостыню");
        DAY_TASKS.Add(DayAgent.DayTask.CONFESS_SINNERS, "исповедует грешников");
        DAY_TASKS.Add(DayAgent.DayTask.INTERPRETING_SACRED_TEXTS, "толкует священные тексты");
        DAY_TASKS.Add(DayAgent.DayTask.LISTEN_TO_GOSSIP, "слушает сплетни");
        DAY_TASKS.Add(DayAgent.DayTask.PREACH_IN_THE_CITY, "проповедует в городе");
        DAY_TASKS.Add(DayAgent.DayTask.SELL_INDULGENCE, "продает индульгенцию");
        NIGHT_TASKS.Add(NightAgent.NightTask.IDLE, "бездействует");
        NIGHT_TASKS.Add(NightAgent.NightTask.OPEN_FLAT, "неприкрыто льстит");
        NIGHT_TASKS.Add(NightAgent.NightTask.MUCHLY_PRAISE, "ехидно похваляется");
        NIGHT_TASKS.Add(NightAgent.NightTask.PROVOKE_TO_FIGHT, "провоцирует на драку");
        NIGHT_TASKS.Add(NightAgent.NightTask.COMPLAINT_ON_JUSTICE, "жалуется на несправедливость");
        NIGHT_TASKS.Add(NightAgent.NightTask.DICE, "играет в кости");
        NIGHT_TASKS.Add(NightAgent.NightTask.TAKE_A_BREAK, "закатывает пирушку");
        NIGHT_TASKS.Add(NightAgent.NightTask.DEVELOP, "развратничает");
    }
    
    public Game game;
    public SettingsMenu settingsPanel;
    public AgentMenu agentMenu;
    public AgentsMenu agentsMenu;
    public SinnersMenu sinnersMenu;
    public Results results;
    public List<Text> agentTexts;
    public Text goldText;
    public Text attentionText;
    public Slider attentionSlider;
    public Text daysRemainedText;
    public List<Button> agentButtons;
    public Sprite dayButtonSprite;
    public Sprite nightButtonSprite;
    public Image clock;
    public Sprite dayClockSprite;
    public Sprite nightClockSprite;
    public GameObject dayPrefabs;
    public GameObject nightPrefabs;
    public ConfirmationMenu tutorialConfirmation;
    public Tutorial tutorial;
    public Text aimText;
    public Slider progressSlider;
    public List<Image> ticks;
    public Sprite dayTick;
    public Sprite nightTick;

    void Start()
    {
        if (__needTutorial)
        {
            BeginTutorial();
        }

        UpdateAgentButtons();
    }
    
    public void ShowSettings()
    {
        settingsPanel.Show();
    }

    public void ShowAgentMenu(int index)
    {
        agentMenu.Show(game.dayTime == Game.DayTime.DAY ? (Agent)game.dayAgents[index] : (Agent)game.nightAgents[index]);
    }

    public void ShowAgentsMenu()
    {
        agentsMenu.Show();
    }

    public void ShowSinnerMenu()
    {
        sinnersMenu.Show();
    }

    public void ChangeDayTime()
    {
        game.ChangeDayTime();
        UpdateAgentButtons();
    }

    public void UpdateAgentButtons()
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            for (int i = 0; i < 4; i++)
            {
                agentTexts[i].text = $"{game.dayAgents[i].name}";
                ticks[i].gameObject.SetActive(game.dayAgents[i].task != DayAgent.DayTask.IDLE);
                ticks[i].sprite = dayTick;
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                agentTexts[i].text = $"{game.nightAgents[i].name}";
                ticks[i].gameObject.SetActive(game.nightAgents[i].task != NightAgent.NightTask.IDLE);
                ticks[i].sprite = nightTick;
            }
        }
    }

    public void UpdateDayTime()
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            foreach (Button button in agentButtons)
            {
                button.image.sprite = dayButtonSprite;
            }

            clock.sprite = dayClockSprite;
            dayPrefabs.SetActive(true);
            nightPrefabs.SetActive(false);
        }
        else
        {
            foreach (Button button in agentButtons)
            {
                button.image.sprite = nightButtonSprite;
            }

            clock.sprite = nightClockSprite;
            dayPrefabs.SetActive(false);
            nightPrefabs.SetActive(true);
        }
    }

    public void TutorialCallback()
    {
        tutorial._Start();
    }

    public void BeginTutorial()
    {
        tutorialConfirmation.Show("Желаете ли пройти обучение и узнать как работает игра?", TutorialCallback);
        __needTutorial = false;
    }

    public void UpdateAimBar(float progress)
    {
        progressSlider.value = progress;
    }

    public void UpdateAimText(string text)
    {
        aimText.text = $"Цель - {text}";
    }

    public void ShowResults()
    {
        results.Show();
    }

}
