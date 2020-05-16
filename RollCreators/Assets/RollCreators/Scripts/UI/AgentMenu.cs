using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
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
        DayAgent.DayTask.SELL_INDULGENCE
    };

    private static NightAgent.NightTask[] NIGHT_TASKS =
    {
        NightAgent.NightTask.OPEN_FLAT,
        NightAgent.NightTask.MUCHLY_PRAISE,
        NightAgent.NightTask.PROVOKE_TO_FIGHT,
        NightAgent.NightTask.COMPLAINT_ON_JUSTICE,
        NightAgent.NightTask.DICE,
        NightAgent.NightTask.TAKE_A_BREAK,
        NightAgent.NightTask.DEVELOP
    };

    private static string[] DESCRIPTIONS1_DAY =
    {
        "\"Страх рождает в людях веру\"",
        "\"Малая цена за спасение своей шкуры\"",
        "\"Пусть поведают о своих пороках\"",
        "\"Насколько сильна их вера?\"",
        "\"Даже у стен храма есть уши\"",
        "\"Больше паства, больше грешников\"",
        "\"Кто же не жаждет искупления?\""
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
        "Повысить алчность у выбранной группы Грешников\nЦена: 25 золота",
        "Повысить чревоугодие у выбранной группы Грешников\nЦена: 50 золота",
        "Повысить блуд у выбранной группы Грешников\nЦена: 50 золота"
    };
    
    private static Agent.Skills[] SKILLS_INDEX =
    {
        Agent.Skills.ELOQUENCE, Agent.Skills.CUNNING, Agent.Skills.WISDOM, Agent.Skills.INSIGHT, Agent.Skills.CHARM, Agent.Skills.PERSUASIVENESS, Agent.Skills.PRESSURE
    };

    private static string[] __socialStatus =
    {
        "Дворяне", "Горожане", "Крестьяне", "Отбросы"
    };

    private static Sinner.SocialStatus[] __statusMap =
    {
        Sinner.SocialStatus.NOBLEMAN, Sinner.SocialStatus.CITIZEN, Sinner.SocialStatus.PEASANT, Sinner.SocialStatus.GARBAGE
    };

    private static string[] __dayParameter =
    {
        "Богобоязненность", "", "Вероятность открытия грехов", "Вероятность открытия богобоязнености", "Вероятность открытия богатства", "Численность", ""
    };

    private static string[] __nightParameter =
    {
        "Тщеславие", "Зависть", "Гнев", "Уныние", "Алчность", "Чревоугодие", "Блуд"
    };

    [HideInInspector] public Agent currentAgent;
    public GameObject dayMenu;
    public GameObject nightMenu;
    public Game game;
    [HideInInspector] public DayAgent.DayTask dayTask;
    [HideInInspector] public NightAgent.NightTask nightTask;
    public Text nameText;
    public Text nameText2;
    public Text eloquenceText;
    public Text cunningText;
    public Text wisdomText;
    public Text insightText;
    public Text charmText;
    public Text persuasivenessText;
    public Text pressureText;
    public Text taskText;
    public Text description1;
    public GameMenu gameMenu;

    public List<Button> dayButtons;
    public Sprite activeDayButton;
    public Sprite inactiveDayButton;
    public List<Button> nightButtons;
    public Sprite activeNightButton;
    public Sprite inactiveNightButton;

    public GameObject agentPanel;
    public GameObject agentsPanel;

    public Text trainButtonText;
    public ConfirmationMenu confirmationMenu;
    public SinnersMenu sinnersMenu;
    public List<Image> sinnersTicks;
    public Sprite dayTick;
    public Sprite nightTick;
    public List<Text> buttonsTexts;
    public GameObject sinnersPanel;
    public Slider slider;
    public Text sliderButtonText;
    public Image sliderTick;
    public GameObject sliderPanel;
    public Image applyTick;
    public GameObject applyPanel;
    
    private Dictionary<DayAgent.DayTask, Agent.Skills> daySkills = new Dictionary<DayAgent.DayTask, Agent.Skills>();
    private Dictionary<NightAgent.NightTask, Agent.Skills> nightSkills = new Dictionary<NightAgent.NightTask, Agent.Skills>();
    private int currentMark = 0;

    void Awake()
    {
        daySkills.Add(DayAgent.DayTask.CONDUCT_A_SERVICE, Agent.Skills.ELOQUENCE);
        daySkills.Add(DayAgent.DayTask.GIVE_ALMS, Agent.Skills.CUNNING);
        daySkills.Add(DayAgent.DayTask.CONFESS_SINNERS, Agent.Skills.INSIGHT);
        daySkills.Add(DayAgent.DayTask.INTERPRETING_SACRED_TEXTS, Agent.Skills.WISDOM);
        daySkills.Add(DayAgent.DayTask.LISTEN_TO_GOSSIP, Agent.Skills.CHARM);
        daySkills.Add(DayAgent.DayTask.PREACH_IN_THE_CITY, Agent.Skills.PERSUASIVENESS);
        daySkills.Add(DayAgent.DayTask.SELL_INDULGENCE, Agent.Skills.PRESSURE);
        nightSkills.Add(NightAgent.NightTask.OPEN_FLAT, Agent.Skills.ELOQUENCE);
        nightSkills.Add(NightAgent.NightTask.MUCHLY_PRAISE, Agent.Skills.WISDOM);
        nightSkills.Add(NightAgent.NightTask.PROVOKE_TO_FIGHT, Agent.Skills.PRESSURE);
        nightSkills.Add(NightAgent.NightTask.COMPLAINT_ON_JUSTICE, Agent.Skills.PERSUASIVENESS);
        nightSkills.Add(NightAgent.NightTask.DICE, Agent.Skills.CUNNING);
        nightSkills.Add(NightAgent.NightTask.TAKE_A_BREAK, Agent.Skills.INSIGHT);
        nightSkills.Add(NightAgent.NightTask.DEVELOP, Agent.Skills.CHARM);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        Show(currentAgent);
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
        UpdateText();
        UpdateTicks();
        UpdateSliderText();
    }

    public void Apply(int index)
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
                agent.tempSocialStatus = __statusMap[index];
            }
        }
        else
        {
            NightAgent agent = (NightAgent) currentAgent;
            agent.task = nightTask;
            agent.tempSocialStatus = __statusMap[index];
        }

        UpdateTicks();
        gameMenu.UpdateAgentButtons();
    }

    private string CheckDayValues(int index, Sinner.SocialStatus status)
    {
        switch (index)
        {
            case 0:
                return
                    $"{-20 + 5 * currentAgent.skills[Agent.Skills.ELOQUENCE]} - {5 * currentAgent.skills[Agent.Skills.ELOQUENCE]}";
            case 2:
                return game.sinners[status].fearOfGodOpened ? $"{game.sinners[status].fearOfGod + 40 + 5 * currentAgent.skills[Agent.Skills.INSIGHT]}%" : "???%";
            case 3:
                return $"{40 + 5 * currentAgent.skills[Agent.Skills.WISDOM]}%";
            case 4:
                return $"{40 + 5 * currentAgent.skills[Agent.Skills.CHARM]}%";
            case 5:
                return $"{game.sinners[status].strength * 5 * currentAgent.skills[Agent.Skills.PERSUASIVENESS]}";
        }
        return "";
    }

    private static Agent.Skills[] __nightAgentSkills =
    {
        Agent.Skills.ELOQUENCE, Agent.Skills.WISDOM, Agent.Skills.PRESSURE, Agent.Skills.PERSUASIVENESS, Agent.Skills.CUNNING, Agent.Skills.INSIGHT, Agent.Skills.CHARM
    };

    private static Sinner.Sins[] __nightSins =
    {
        Sinner.Sins.VANITY, Sinner.Sins.ENVY, Sinner.Sins.ANGER, Sinner.Sins.GLOOM, Sinner.Sins.GREED, Sinner.Sins.GLUTTONY, Sinner.Sins.FORNICATION
    };

    private string CheckNightValues(int index, Sinner.SocialStatus status)
    {
        return game.sinners[status].sinsOpened ? $"{-30 + 2 * (currentAgent.skills[__nightAgentSkills[index]] + game.sinners[status].sins[__nightSins[index]] / 10)} - {10 + 2 * (currentAgent.skills[__nightAgentSkills[index]] + game.sinners[status].sins[__nightSins[index]] / 10)}" : "???";
    }

    public void ShowDayPanel(int index)
    {
        currentMark = index;
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
        if (index == 0)
        {
            agentsPanel.SetActive(true);
            agentPanel.SetActive(false);
            
            trainButtonText.text = $"Повысить уровень {Agent.EXPERIENCE[currentAgent.level] * 50} золота";
            return;
        }
        agentPanel.SetActive(true);
        agentsPanel.SetActive(false);
        if (index == 2)
        {
            sliderPanel.SetActive(true);
            sinnersPanel.SetActive(false);
            applyPanel.SetActive(false);
        }
        else if (index == 7)
        {
            applyPanel.SetActive(true);
            sliderPanel.SetActive(false);
            sinnersPanel.SetActive(false);
        }
        else
        {
            sinnersPanel.SetActive(true);
            applyPanel.SetActive(false);
            sliderPanel.SetActive(false);
        }
        description1.text = DESCRIPTIONS1_DAY[index - 1];
        taskText.text = DAY_TASK_NAMES[index - 1];
        dayTask = DAY_TASKS[index - 1];
        for (int i = 0; i < buttonsTexts.Count; i++)
        {
            buttonsTexts[i].text = $"{__socialStatus[i]}\n{__dayParameter[index - 1]}: {CheckDayValues(index - 1, __statusMap[i])}";
        }
    }

    private void UpdateText()
    {
        nameText.text = $"{currentAgent.name}";
        nameText2.text = $"{currentAgent.name}";
        eloquenceText.text = $"Красноречие {currentAgent.skills[Agent.Skills.ELOQUENCE]}";
        cunningText.text = $"Хитрость {currentAgent.skills[Agent.Skills.CUNNING]}";
        wisdomText.text = $"Мудрость {currentAgent.skills[Agent.Skills.WISDOM]}";
        insightText.text = $"Проницательность {currentAgent.skills[Agent.Skills.INSIGHT]}";
        charmText.text = $"Обаяние {currentAgent.skills[Agent.Skills.CHARM]}";
        persuasivenessText.text = $"Убедительность {currentAgent.skills[Agent.Skills.PERSUASIVENESS]}";
        pressureText.text = $"Напор {currentAgent.skills[Agent.Skills.PRESSURE]}";
    }
    
    public void ImproveSkill(int index)
    {
        if (currentAgent.skillPoints == 0 || currentAgent.skills[SKILLS_INDEX[index]] == 10) return;
        currentAgent.skillPoints--;
        currentAgent.skills[SKILLS_INDEX[index]]++;
        UpdateText();
    }
    
    private void NewAgentCallback()
    {
        currentAgent.NewAgent(game);
        gameMenu.UpdateAgentButtons();
        UpdateText();
    }

    public void NewAgent()
    {
        confirmationMenu.Show("Вы действительно хотите нанять нового агента за 200 монет?", NewAgentCallback);
    }

    private void TrainAgentCallback()
    {
        currentAgent.TrainAgent(game, Agent.EXPERIENCE[currentAgent.level]);
        gameMenu.UpdateAgentButtons();
        UpdateText();
    }

    public void TrainAgent()
    {
        confirmationMenu.Show($"Вы действительно хотите {trainButtonText.text}?", TrainAgentCallback);
    }

    public void ShowNightPanel(int index)
    {
        sinnersPanel.SetActive(true);
        sliderPanel.SetActive(false);
        applyPanel.SetActive(false);
        currentMark = 0;
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
        if (index == 0)
        {
            agentsPanel.SetActive(true);
            agentPanel.SetActive(false);
            trainButtonText.text = $"Повысить уровень {Agent.EXPERIENCE[currentAgent.level] * 50} золота";
            return;
        }
        agentPanel.SetActive(true);
        agentsPanel.SetActive(false);
        description1.text = DESCRIPTIONS1_NIGHT[index - 1];
        taskText.text = NIGHT_TASK_NAMES[index - 1];
        nightTask = NIGHT_TASKS[index - 1];
        for (int i = 0; i < buttonsTexts.Count; i++)
        {
            buttonsTexts[i].text = $"{__socialStatus[i]}\n{__nightParameter[index - 1]}: {CheckNightValues(index - 1, __statusMap[i])}";
        }
    }

    public void ShowSinners()
    {
        sinnersMenu.Show();
        Close();
    }

    private void UpdateTicks()
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            DayAgent agent = (DayAgent) currentAgent;
            for (int i = 0; i < 4; i++)
            {
                if (agent.tempSocialStatus == __statusMap[i] && agent.task != DayAgent.DayTask.IDLE && agent.task != DayAgent.DayTask.GIVE_ALMS && agent.task != DayAgent.DayTask.SELL_INDULGENCE)
                {
                    sinnersTicks[i].sprite = dayTick;
                    sinnersTicks[i].gameObject.SetActive(true);
                }
                else
                {
                    sinnersTicks[i].gameObject.SetActive(false);
                }
            }

            if (agent.task == DayAgent.DayTask.GIVE_ALMS)
            {
                sliderTick.gameObject.SetActive(true);
            }
            else
            {
                sliderTick.gameObject.SetActive(false);
            }

            if (agent.task == DayAgent.DayTask.SELL_INDULGENCE)
            {
                applyTick.gameObject.SetActive(true);
            }
            else
            {
                applyTick.gameObject.SetActive(false);
            }
        }
        else
        {
            NightAgent agent = (NightAgent) currentAgent;
            for (int i = 0; i < 4; i++)
            {
                if (agent.tempSocialStatus == __statusMap[i] && agent.task != NightAgent.NightTask.IDLE)
                {
                    sinnersTicks[i].sprite = nightTick;
                    sinnersTicks[i].gameObject.SetActive(true);
                }
                else
                {
                    sinnersTicks[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void SwapLeft()
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            for (int i = 0; i < game.dayAgents.Count - 1; i++)
            {
                if (game.dayAgents[i + 1] == currentAgent)
                {
                    currentAgent = game.dayAgents[i];
                    ShowDayPanel(currentMark);
                    UpdateText();
                    UpdateTicks();
                    return;
                }
            }

            currentAgent = game.dayAgents[game.dayAgents.Count - 1];
            ShowDayPanel(currentMark);
            UpdateText();
            UpdateTicks();
        }
        else
        {
            for (int i = 0; i < game.nightAgents.Count - 1; i++)
            {
                if (game.nightAgents[i + 1] == currentAgent)
                {
                    currentAgent = game.nightAgents[i];
                    ShowNightPanel(currentMark);
                    UpdateText();
                    UpdateTicks();
                    return;
                }
            }

            currentAgent = game.nightAgents[game.nightAgents.Count - 1];
            ShowNightPanel(currentMark);
            UpdateText();
            UpdateTicks();
        }
    }

    public void SwapRight()
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            for (int i = game.dayAgents.Count - 1; i > 0; i--)
            {
                if (game.dayAgents[i - 1] == currentAgent)
                {
                    currentAgent = game.dayAgents[i];
                    ShowDayPanel(currentMark);
                    UpdateText();
                    UpdateTicks();
                    return;
                }
            }

            currentAgent = game.dayAgents[0];
            ShowDayPanel(currentMark);
            UpdateText();
            UpdateTicks();
        }
        else
        {
            for (int i = game.nightAgents.Count - 1; i > 0; i--)
            {
                if (game.nightAgents[i - 1] == currentAgent)
                {
                    currentAgent = game.nightAgents[i];
                    ShowNightPanel(currentMark);
                    UpdateText();
                    UpdateTicks();
                    return;
                }
            }

            currentAgent = game.nightAgents[0];
            ShowNightPanel(currentMark);
            UpdateText();
            UpdateTicks();
        }
    }

    public void UpdateSliderText()
    {
        sliderButtonText.text = $"Раздавать милостыню\n{slider.value * 10} золота";
    }
}
