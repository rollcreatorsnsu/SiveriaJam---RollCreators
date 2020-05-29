using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentMenu : MonoBehaviour
{
    private static Agent.Skills[] SKILLS =
    {
        Agent.Skills.CUNNING,
        Agent.Skills.MIND,
        Agent.Skills.SPIRIT
    };

    private static string[] DAY_LEVELS =
    {
        "клирик",
        "диакон",
        "священник"
    };

    private static string[] NIGHT_LEVELS =
    {
        "мошенник",
        "скользкий тип",
        "тайный агент"
    };
    
    private static Dictionary<Agent.Perks, string> PERKS = new Dictionary<Agent.Perks, string>();

    private static string[] D1 =
    {
        "Привлекать приследователей",
        "Раздавать милостыню",
        "Продавать чудодейственные бальзамы"
    };

    private static string[] D2 =
    {
        "Продавать индульгенции",
        "Провести службу",
        "Отвлечь инквизицию"
    };

    private static string[] D3 =
    {
        "Проповедь",
        "Совместные воспевания",
        "Принятие избранных"
    };
    
    private static string[] N1 =
    {
        "Манипулирование",
        "Клевета",
        "Грабёж"
    };

    private static string[] N2 =
    {
        "Коррупция",
        "Тренировка",
        "Придать искушению"
    };

    private static string[] N3 =
    {
        "Закалка духа",
        "Грехопадение",
        "Пропаганда"
    };
    
    private static DayAgent.DayTask[] DD1 =
    {
        DayAgent.DayTask.ATTRACT_FOLLOWERS,
        DayAgent.DayTask.GIVE_ALMS,
        DayAgent.DayTask.SELL_MIRACULOUS_BALMS
    };

    private static DayAgent.DayTask[] DD2 =
    {
        DayAgent.DayTask.SELL_INDULGENCES,
        DayAgent.DayTask.CONDUCT_A_SERVICE,
        DayAgent.DayTask.DISTRACT_THE_INQUISITION
    };

    private static DayAgent.DayTask[] DD3 =
    {
        DayAgent.DayTask.PREACHING,
        DayAgent.DayTask.JOINT_CHANTING,
        DayAgent.DayTask.ACCEPTANCE_OF_THE_ELECT
    };
    
    private static NightAgent.NightTask[] NN1 =
    {
        NightAgent.NightTask.MANIPULATION,
        NightAgent.NightTask.SLANDER,
        NightAgent.NightTask.ROBBERY
    };

    private static NightAgent.NightTask[] NN2 =
    {
        NightAgent.NightTask.CORRUPTION,
        NightAgent.NightTask.TRAINING,
        NightAgent.NightTask.TEMPT
    };

    private static NightAgent.NightTask[] NN3 =
    {
        NightAgent.NightTask.TEMPERING_OF_SPIRIT,
        NightAgent.NightTask.THE_FALL,
        NightAgent.NightTask.PROPAGANDA
    };

    private static string[][] DAY_TASKS_TEXTS = {D1, D2, D3};
    private static string[][] NIGHT_TASKS_TEXTS = {N1, N2, N3};
    private static DayAgent.DayTask[][] DAY_TASKS = {DD1, DD2, DD3};
    private static NightAgent.NightTask[][] NIGHT_TASKS = {NN1, NN2, NN3};

    private static Sinner.SocialStatus[] SOCIALS =
    {
        Sinner.SocialStatus.NOBLEMAN,
        Sinner.SocialStatus.CITIZEN,
        Sinner.SocialStatus.PEASANT,
        Sinner.SocialStatus.GARBAGE
    };

    static AgentMenu()
    {
        PERKS.Add(Agent.Perks.PERK_1, "Кадый раз когда этот агент получает монеты, увеличивайте их количество на 10%");
        PERKS.Add(Agent.Perks.PERK_2, "Все лительные эфекты налженые этим агнтом длятся на 1 день больше");
        PERKS.Add(Agent.Perks.PERK_3, "Если агнет использует стандартное действие Хитрости, добавьте +1 к его Хитрости");
        PERKS.Add(Agent.Perks.PERK_4, "Если агнет использует стандартное действие Разума, добавьте +1 к его Разуму");
        PERKS.Add(Agent.Perks.PERK_5, "Если агнет использует стандартное действие Воли, добавьте +1 к его Воли");
        PERKS.Add(Agent.Perks.PERK_6, "В начаел каждого дня агент получает 1 опыт");
        PERKS.Add(Agent.Perks.PERK_7, "Любое действие агета в дополнение к эффекту снижает Внимание инквииции на 5%");
        PERKS.Add(Agent.Perks.PERK_8, "Кадый раз когда этот агент увеличивает какой-либо параметр Греников, дполнтельо увеличьте его на 5%");
        PERKS.Add(Agent.Perks.PERK_9, "Стоимость покупи опыа для агента ниже на 30%");
    }

    private string[] TASKS_SKILL =
    {
        "Действия хитрости",
        "Действия разума",
        "Действия духа"
    };

    public Game game;
    public GameMenu gameMenu;
    public ConfirmationMenu confirmationMenu;
    public SinnersMenu sinnersMenu;
    public List<GameObject> activeButtons;
    public List<GameObject> inactiveButtons;
    public List<Text> buttonsSkillsTexts;
    public Text name;
    public GameObject firstPageLeftPage;
    public GameObject firstPageRightPage;
    public GameObject mainLeftPage;
    public GameObject mainRightPage;
    public GameObject agentsRightPage;
    public GameObject inquisitionRightPage;
    public GameObject moneyRightPage;
    public GameObject firstPageRightButton;
    public GameObject mainRightButton;
    public Text trainButtonText;
    public Text agentInfoText;
    public List<Text> perksTexts;
    public Text tasksSkillText;
    public List<Text> buttonsTasksText;
    public List<GameObject> buttonsTasksLocks;
    public List<Image> buttonsTasksImages;
    public List<Button> buttonsTasks;
    public List<GameObject> buttonsTasksTicks;
    public Sprite activeButton;
    public Sprite inactiveButton;
    public Sprite lockedButton;
    public Sprite activeDayBookmark;
    public Sprite inactiveDayBookmark;
    public Sprite activeNightBookmark;
    public Sprite inactiveNightBookmark;
    public List<GameObject> socialsTicks;
    public List<Text> leftTexts;
    public List<Text> rightTexts;
    public List<Text> topTexts;
    public List<Image> addImages;
    public List<Image> leftImages;
    public List<Image> rightImages;
    public List<Image> topImages;
    public List<Sprite> wealthSprites;
    public List<Sprite> faithSprites;
    public List<Sprite> sinsSprites;
    public List<Sprite> strengthSprites;
    public List<Sprite> goldSprites;
    public Sprite attentionSprite;
    public Sprite dayAgentSprite;
    public Sprite nightAgentSprite;
    public Sprite lockSprite;
    
    private int currentAgentIndex;
    private int currentPanelIndex;
    private Agent agent;

    public void Show(int currentAgent)
    {
        gameObject.SetActive(true);
        currentAgentIndex = currentAgent;
        bool isDay = game.dayTime == Game.DayTime.DAY;
        for (int i = 0; i < 4; i++)
        {
            activeButtons[i].GetComponent<Image>().sprite = isDay ? activeDayBookmark : activeNightBookmark;
            inactiveButtons[i].GetComponent<Image>().sprite = isDay ? inactiveDayBookmark : inactiveNightBookmark;
        }
        ShowPanel(0);
    }

    public void ShowPanel(int currentPanel)
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            agent = game.dayAgents[currentAgentIndex];
        }
        else
        {
            agent = game.nightAgents[currentAgentIndex];
        }
        currentPanelIndex = currentPanel;
        for (int i = 0; i < 4; i++)
        {
            activeButtons[i].SetActive(i == currentPanel);
            inactiveButtons[i].SetActive(i != currentPanel);
        }
        buttonsSkillsTexts[0].text = $"Хитрость {agent.skills[Agent.Skills.CUNNING]}";
        buttonsSkillsTexts[1].text = $"Разум {agent.skills[Agent.Skills.MIND]}";
        buttonsSkillsTexts[2].text = $"Дух {agent.skills[Agent.Skills.SPIRIT]}";
        buttonsSkillsTexts[3].text = $"Хитрость {agent.skills[Agent.Skills.CUNNING]}";
        buttonsSkillsTexts[4].text = $"Разум {agent.skills[Agent.Skills.MIND]}";
        buttonsSkillsTexts[5].text = $"Дух {agent.skills[Agent.Skills.SPIRIT]}";
        name.text = $"{agent.name}";
        firstPageLeftPage.SetActive(false);
        firstPageRightPage.SetActive(false);
        firstPageRightButton.SetActive(false);
        mainLeftPage.SetActive(false);
        mainRightPage.SetActive(false);
        mainRightButton.SetActive(false);
        agentsRightPage.SetActive(false);
        inquisitionRightPage.SetActive(false);
        moneyRightPage.SetActive(false);
        if (currentPanel == 0)
        {
            firstPageLeftPage.SetActive(true);
            firstPageRightPage.SetActive(true);
            firstPageRightButton.SetActive(true);
            trainButtonText.text = $"Повысить уровень {agent.NeedGold(agent.RemainExp())} золота";
            agentInfoText.text =
                $"Доступно очков навыка: {agent.skillPoints}\nХитрость: {agent.skills[Agent.Skills.CUNNING]}\nРазум: {agent.skills[Agent.Skills.MIND]}\nДух: {agent.skills[Agent.Skills.SPIRIT]}\nОпыт: {agent.experience}/{Agent.EXPERIENCE[agent.level]}\nУровень: {(game.dayTime == Game.DayTime.DAY ? DAY_LEVELS[agent.level - 1] : NIGHT_LEVELS[agent.level - 1])}";
            for (int i = 0; i < 4; i++)
            {
                perksTexts[i].text = "";
            }
            int index = 0;
            foreach (Agent.Perks perks in agent.perks)
            {
                perksTexts[index].text = PERKS[perks];
            }
        }
        else
        {
            mainLeftPage.SetActive(true);
            mainRightButton.SetActive(true);
            tasksSkillText.text = TASKS_SKILL[currentPanel - 1];
            for (int i = 0; i < 3; i++)
            {
                buttonsTasks[i].onClick.RemoveAllListeners();
                buttonsTasksTicks[i].SetActive(false);
                if (game.dayTime == Game.DayTime.DAY)
                {
                    DayAgent dayAgent = (DayAgent) agent;
                    DayAgent.DayTask dayTask = DAY_TASKS[currentPanel - 1][i];
                    buttonsTasksText[i].text = DAY_TASKS_TEXTS[currentPanel - 1][i];
                    if (dayAgent.IsSkillAvailable(dayTask))
                    {
                        buttonsTasksLocks[i].SetActive(false);
                        if (i == 0)
                        {
                            buttonsTasks[i].onClick.AddListener(delegate { GoTask(0); });
                        }
                        else if (i == 1)
                        {
                            buttonsTasks[i].onClick.AddListener(delegate { GoTask(1); });
                        }
                        else if (i == 2)
                        {
                            buttonsTasks[i].onClick.AddListener(delegate { GoTask(2); });
                        }
                        if (dayAgent.task == dayTask)
                        {
                            buttonsTasksTicks[i].SetActive(true);
                            buttonsTasksImages[i].sprite = activeButton;
                            mainRightPage.SetActive(true);
                        }
                        else
                        {
                            buttonsTasksImages[i].sprite = inactiveButton;
                        }
                    }
                    else
                    {
                        buttonsTasksLocks[i].SetActive(true);
                        buttonsTasksImages[i].sprite = lockedButton;
                    }
                }
                else
                {
                    NightAgent nightAgent = (NightAgent) agent;
                    NightAgent.NightTask nightTask = NIGHT_TASKS[currentPanel - 1][i];
                    buttonsTasksText[i].text = NIGHT_TASKS_TEXTS[currentPanel - 1][i];
                    if (nightAgent.IsSkillAvailable(nightTask))
                    {
                        buttonsTasksLocks[i].SetActive(false);
                        if (i == 0)
                        {
                            buttonsTasks[i].onClick.AddListener(delegate { GoTask(0); });
                        }
                        else if (i == 1)
                        {
                            buttonsTasks[i].onClick.AddListener(delegate { GoTask(1); });
                        }
                        else if (i == 2)
                        {
                            buttonsTasks[i].onClick.AddListener(delegate { GoTask(2); });
                        }
                        if (nightAgent.task == nightTask)
                        {
                            buttonsTasksTicks[i].SetActive(true);
                            buttonsTasksImages[i].sprite = activeButton;
                            mainRightPage.SetActive(true);
                        }
                        else
                        {
                            buttonsTasksImages[i].sprite = inactiveButton;
                        }
                    }
                    else
                    {
                        buttonsTasksLocks[i].SetActive(true);
                        buttonsTasksImages[i].sprite = lockedButton;
                    }
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (game.dayTime == Game.DayTime.DAY)
            {
                socialsTicks[i].SetActive(((DayAgent)agent).tempSocialStatus == SOCIALS[i]);
            }
            else
            {
                socialsTicks[i].SetActive(((NightAgent)agent).tempSocialStatus == SOCIALS[i]);
            }
        }
    }

    public void LeftAgent()
    {
        if (currentAgentIndex == 0)
        {
            currentAgentIndex = 3;
        }
        else
        {
            currentAgentIndex--;
        }
        ShowPanel(currentPanelIndex);
    }

    public void RightAgent()
    {
        if (currentAgentIndex == 3)
        {
            currentAgentIndex = 0;
        }
        else
        {
            currentAgentIndex++;
        }
        ShowPanel(currentPanelIndex);
    }

    public void Close()
    {
        gameMenu.UpdateData();
        gameObject.SetActive(false);
    }

    private void NewAgentCallback()
    {
        agent.NewAgent(game);
        ShowPanel(currentPanelIndex);
    }

    public void NewAgent()
    {
        confirmationMenu.Show("Вы действительно хотите нанять нового агента за 200 монет?", NewAgentCallback);
    }

    private void TrainAgentCallback()
    {
        agent.TrainAgent(game, agent.RemainExp());
        ShowPanel(currentPanelIndex);
    }

    public void TrainAgent()
    {
        confirmationMenu.Show($"Вы действительно хотите повысить уровень за {agent.NeedGold(agent.RemainExp())} золота?", TrainAgentCallback);
    }

    public void ImproveSkill(int skillIndex)
    {
        if (agent.skillPoints > 0)
        {
            agent.skills[SKILLS[skillIndex]]++;
            ShowPanel(currentPanelIndex);
        }
    }

    public void ShowSinners()
    {
        sinnersMenu.Show(0);
        gameObject.SetActive(false);
    }

    public void GoTask(int buttonIndex)
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            DayAgent.DayTask dayTask = DAY_TASKS[currentPanelIndex - 1][buttonIndex];
            DayAgent dayAgent = (DayAgent) agent;
            dayAgent.task = dayTask;
            for (int i = 0; i < 4; i++)
            {
                int needDays = dayAgent.GetDaysResult(dayTask);
                if (needDays > 0)
                {
                    topImages[i].gameObject.SetActive(true);
                    topTexts[i].text = $"{needDays}";
                    addImages[i].gameObject.SetActive(true);
                }
                else
                {
                    topImages[i].gameObject.SetActive(false);
                    topTexts[i].text = "";
                    addImages[i].gameObject.SetActive(false);
                }

                float firstResult = dayAgent.GetFirstResult(game, SOCIALS[i], dayTask);
                float secondResult = dayAgent.GetSecondResult(game, SOCIALS[i], dayTask);
                if (firstResult == 0)
                {
                    leftImages[i].gameObject.SetActive(false);
                    leftTexts[i].text = "";
                }
                else
                {
                    leftTexts[i].text = $"{firstResult}";
                    leftImages[i].gameObject.SetActive(true);
                    switch (dayTask)
                    {
                        case DayAgent.DayTask.ATTRACT_FOLLOWERS:
                            leftImages[i].sprite = strengthSprites[i];
                            break;
                        case DayAgent.DayTask.GIVE_ALMS:
                            leftImages[i].sprite = goldSprites[i];
                            break;
                        case DayAgent.DayTask.SELL_MIRACULOUS_BALMS:
                            leftImages[i].sprite = faithSprites[i];
                            break;
                        case DayAgent.DayTask.SELL_INDULGENCES:
                            leftImages[i].sprite = goldSprites[i];
                            break;
                        case DayAgent.DayTask.DISTRACT_THE_INQUISITION:
                            leftImages[i].sprite = attentionSprite;
                            break;
                        case DayAgent.DayTask.PREACHING:
                            leftImages[i].sprite = faithSprites[i];
                            break;
                        case DayAgent.DayTask.JOINT_CHANTING:
                            leftImages[i].sprite = faithSprites[i];
                            break;
                        case DayAgent.DayTask.ACCEPTANCE_OF_THE_ELECT:
                            leftImages[i].sprite = goldSprites[i];
                            break;
                        case DayAgent.DayTask.CONDUCT_A_SERVICE:
                            leftImages[i].sprite = lockSprite;
                            break;
                    }
                }
                if (secondResult == 0)
                {
                    rightImages[i].gameObject.SetActive(false);
                    rightTexts[i].text = "";
                }
                else
                {
                    rightTexts[i].text = $"{secondResult}";
                    rightImages[i].gameObject.SetActive(true);
                    switch (dayTask)
                    {
                        case DayAgent.DayTask.GIVE_ALMS:
                            rightImages[i].sprite = attentionSprite;
                            break;
                        case DayAgent.DayTask.SELL_MIRACULOUS_BALMS:
                            rightImages[i].sprite = goldSprites[i];
                            break;
                        case DayAgent.DayTask.SELL_INDULGENCES:
                            rightImages[i].sprite = strengthSprites[i];
                            break;
                        case DayAgent.DayTask.ACCEPTANCE_OF_THE_ELECT:
                            rightImages[i].sprite = sinsSprites[i];
                            break;
                    }
                }
            }
        }
        else
        {
            NightAgent.NightTask nightTask = NIGHT_TASKS[currentPanelIndex - 1][buttonIndex];
            NightAgent nightAgent = (NightAgent) agent;
            nightAgent.task = nightTask;
            for (int i = 0; i < 4; i++)
            {
                int needDays = nightAgent.GetDaysResult(nightTask);
                if (needDays > 0)
                {
                    topImages[i].gameObject.SetActive(true);
                    topTexts[i].text = $"{needDays}";
                    addImages[i].gameObject.SetActive(true);
                }
                else
                {
                    topImages[i].gameObject.SetActive(false);
                    topTexts[i].text = "";
                    addImages[i].gameObject.SetActive(false);
                }

                float firstResult = nightAgent.GetFirstResult(game, SOCIALS[i], nightTask);
                float secondResult = nightAgent.GetSecondResult(game, SOCIALS[i], nightTask);
                if (firstResult == 0)
                {
                    leftImages[i].gameObject.SetActive(false);
                    leftTexts[i].text = "";
                }
                else
                {
                    leftTexts[i].text = $"{firstResult}";
                    leftImages[i].gameObject.SetActive(true);
                    switch (nightTask)
                    {
                        case NightAgent.NightTask.MANIPULATION:
                            leftImages[i].sprite = sinsSprites[i];
                            break;
                        case NightAgent.NightTask.SLANDER:
                            leftImages[i].sprite = strengthSprites[i];
                            break;
                        case NightAgent.NightTask.ROBBERY:
                            leftImages[i].sprite = goldSprites[i];
                            break;
                        case NightAgent.NightTask.CORRUPTION:
                            leftImages[i].sprite = wealthSprites[i];
                            break;
                        case NightAgent.NightTask.TRAINING:
                            leftImages[i].sprite = nightAgentSprite;
                            break;
                        case NightAgent.NightTask.TEMPT:
                            leftImages[i].sprite = sinsSprites[i];
                            break;
                        case NightAgent.NightTask.THE_FALL:
                            leftImages[i].sprite = sinsSprites[i];
                            break;
                        case NightAgent.NightTask.PROPAGANDA:
                            leftImages[i].sprite = goldSprites[i];
                            break;
                        case NightAgent.NightTask.TEMPERING_OF_SPIRIT:
                            leftImages[i].sprite = dayAgentSprite;
                            break;
                    }
                }
                if (secondResult == 0)
                {
                    rightImages[i].gameObject.SetActive(false);
                    rightTexts[i].text = "";
                }
                else
                {
                    rightTexts[i].text = $"{secondResult}";
                    rightImages[i].gameObject.SetActive(true);
                    switch (nightTask)
                    {
                        case NightAgent.NightTask.MANIPULATION:
                            rightImages[i].sprite = attentionSprite;
                            break;
                        case NightAgent.NightTask.SLANDER:
                            rightImages[i].sprite = attentionSprite;
                            break;
                        case NightAgent.NightTask.ROBBERY:
                            rightImages[i].sprite = attentionSprite;
                            break;
                        case NightAgent.NightTask.CORRUPTION:
                            rightImages[i].sprite = attentionSprite;
                            break;
                        case NightAgent.NightTask.TEMPT:
                            rightImages[i].sprite = goldSprites[i];
                            break;
                        case NightAgent.NightTask.THE_FALL:
                            rightImages[i].sprite = wealthSprites[i];
                            break;
                    }
                }
            }
        }
        ShowPanel(currentPanelIndex);
    }

    public void ChooseSocial(int buttonIndex)
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            ((DayAgent) agent).tempSocialStatus = SOCIALS[buttonIndex];
        }
        else
        {
            ((NightAgent) agent).tempSocialStatus = SOCIALS[buttonIndex];
        }
        ShowPanel(currentPanelIndex);
    }
    
}
