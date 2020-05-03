using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentMenu : MonoBehaviour
{
    private static string[] DAY_TASK_NAMES =
    {
        "Проводить службу", "Раздавать милостыню", "Исповедовать грешников", "Толковать священные тексты", "Слушать сплетни", "Проповедовать в городе", "Продавать индульгенцию"
    };

    private static string[] NIGHT_TASK_NAMES =
    {
        "Неприкрыто льстить", "Ехидно похваляться", "Провоцировать на драку", "Жаловаться на несправедливость", "Играть в кости", "Закатывать пирушку", "Разватничать"
    };

    private static DayAgent.DayTask[] DAY_TASKS =
    {
        DayAgent.DayTask.CONDUCT_A_SERVICE,
        DayAgent.DayTask.GIVE_ALMS,
        DayAgent.DayTask.CONFESS_SINNERS,
        DayAgent.DayTask.INTERPRETING_SACRED_TEXTS,
        DayAgent.DayTask.LISTEN_TO_GOSSIP,
        DayAgent.DayTask.PREACH_IN_THE_CITY,
        DayAgent.DayTask.SELL_INDULGENCE,
        DayAgent.DayTask.CHANGE_AGENT,
        DayAgent.DayTask.TRAIN_AGENT
    };

    private static NightAgent.NightTask[] NIGHT_TASKS =
    {
        NightAgent.NightTask.OPEN_FLAT,
        NightAgent.NightTask.MUCHLY_PRAISE,
        NightAgent.NightTask.PROVOKE_TO_FIGHT,
        NightAgent.NightTask.COMPLAINT_ON_JUSTICE,
        NightAgent.NightTask.DICE,
        NightAgent.NightTask.TAKE_A_BREAK,
        NightAgent.NightTask.DEVELOP,
        NightAgent.NightTask.CHANGE_AGENT,
        NightAgent.NightTask.TRAIN_AGENT
    };

    private static string[] DESCRIPTIONS1_DAY =
    {
        "Страх рождает в людях веру",
        "Малая цена за спасение своей шкуры",
        "Пусть поведают о своих пороках",
        "Насколько сильна их вера?",
        "Даже у стен храма есть уши",
        "Больше паства, больше грешников",
        "Кто же не жаждет искупления?"
    };

    private static string[] DESCRIPTIONS2_DAY =
    {
        "Увеличить богобоязненность выбранной группы Грешников",
        "Снизить подозрение инквизиции, потратив немного золота",
        "Узнать величину грехов выбранной группы Грешников",
        "Узнать о богобоязненности выбранной группы Грешников",
        "Узнать о богатстве выбранной группы Грешников",
        "Увеличить численность выбранной группы Грешников",
        "Отпустить людям грехи в обмен на золото"
    };

    private static string[] DESCRIPTIONS1_NIGHT =
    {
        "\"Ох, я спутал вас с маркизом\"",
        "\"Смотри что я прикупил на днях\"",
        "\"А ну иди сюда, ублюдок!\"",
        "\"Опять долгоносик сожрал посевы!\"",
        "\"Ещё партию и точно по домам!\"",
        "\"Садись к столу, давай выпьем!\"",
        "\"Девочки тебя уже заждались!\""
    };

    private static string[] DESCRIPTIONS2_NIGHT =
    {
        "Повысить тщеславие у выбранной группы Грешников",
        "Повысить зависть у выбранной группы Грешников",
        "Повысить гнев у выбранной группы Грешников",
        "Повысить уныние у выбранной группы Грешников",
        "Повысить алчность у выбранной группы Грешников",
        "Повысить чревоугодие у выбранной группы Грешников",
        "Повысить блуд у выбранной группы Грешников"
    };
    
    [HideInInspector] public Agent currentAgent;
    public GameObject dayMenu;
    public GameObject nightMenu;
    public Game game;
    [HideInInspector] public DayAgent.DayTask dayTask;
    [HideInInspector] public NightAgent.NightTask nightTask;
    public Text nameText;
    public Text eloquenceText;
    public Text cunningText;
    public Text wisdomText;
    public Text insightText;
    public Text charmText;
    public Text persuasivenessText;
    public Text pressureText;
    public Text taskText;
    public Text description1;
    public Text description2;
    public GameMenu gameMenu;

    public GameObject dropDownPanel;
    public Dropdown dropdown;

    public GameObject sliderPanel;
    public Slider slider;

    public List<Button> dayButtons;
    public Sprite activeDayButton;
    public Sprite inactiveDayButton;
    public List<Button> nightButtons;
    public Sprite activeNightButton;
    public Sprite inactiveNightButton;

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Show(Agent agent)
    {
        currentAgent = agent;
        gameObject.SetActive(true);
        if (game.dayTime == Game.DayTime.DAY)
        {
            dayMenu.SetActive(true);
            nightMenu.SetActive(false);
            ShowDayPanel(0);
        }
        else
        {
            nightMenu.SetActive(true);
            dayMenu.SetActive(false);
            ShowNightPanel(0);
        }
        nameText.text = $"Имя: {currentAgent.name}";
        eloquenceText.text = $"Красноречие {currentAgent.skills[Agent.Skills.ELOQUENCE]}";
        cunningText.text = $"Хитрость {currentAgent.skills[Agent.Skills.CUNNING]}";
        wisdomText.text = $"Мудрость {currentAgent.skills[Agent.Skills.WISDOM]}";
        insightText.text = $"Проницательность {currentAgent.skills[Agent.Skills.INSIGHT]}";
        charmText.text = $"Обаяние {currentAgent.skills[Agent.Skills.CHARM]}";
        persuasivenessText.text = $"Убедительность {currentAgent.skills[Agent.Skills.PERSUASIVENESS]}";
        pressureText.text = $"Напор {currentAgent.skills[Agent.Skills.PRESSURE]}";
        gameMenu.UpdateAgentButtons();
    }

    public void Apply()
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            DayAgent agent = (DayAgent) currentAgent;
            agent.task = dayTask;
            if (dayTask == DayAgent.DayTask.GIVE_ALMS)
            {
                agent.tempInt = (int)slider.value;
            }
            else if (dayTask != DayAgent.DayTask.SELL_INDULGENCE)
            {
                agent.tempSocialStatus = (Sinner.SocialStatus) Enum.Parse(typeof(Sinner.SocialStatus),
                    dropdown.options[dropdown.value].text);
            }
        }
        else
        {
            NightAgent agent = (NightAgent) currentAgent;
            agent.task = nightTask;
            agent.tempSocialStatus = (Sinner.SocialStatus) Enum.Parse(typeof(Sinner.SocialStatus),
                dropdown.options[dropdown.value].text);
        }
        gameMenu.UpdateAgentButtons();
        Close();
    }

    public void ShowDayPanel(int index)
    {
        description1.text = DESCRIPTIONS1_DAY[index];
        description2.text = DESCRIPTIONS2_DAY[index];
        taskText.text = DAY_TASK_NAMES[index];
        dayTask = DAY_TASKS[index];
        sliderPanel.SetActive(index == 1);
        dropDownPanel.SetActive(index != 1 && index != 6);
        for (var i = 0; i < dayButtons.Count; i++)
        {
            if (i == index)
            {
                dayButtons[i].image.sprite = activeDayButton;
                dayButtons[i].GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1, 1);
            }
            else
            {
                dayButtons[i].image.sprite = inactiveDayButton;
                dayButtons[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }

        }
    }

    public void ShowNightPanel(int index)
    {
        description1.text = DESCRIPTIONS1_NIGHT[index];
        description2.text = DESCRIPTIONS2_NIGHT[index];
        taskText.text = NIGHT_TASK_NAMES[index];
        nightTask = NIGHT_TASKS[index];
        sliderPanel.SetActive(false);
        dropDownPanel.SetActive(true);
        for (var i = 0; i < nightButtons.Count; i++)
        {
            if (i == index)
            {
                nightButtons[i].image.sprite = activeNightButton;
                nightButtons[i].GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1, 1);
            }
            else
            {
                nightButtons[i].image.sprite = inactiveNightButton;
                nightButtons[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }
    
}
