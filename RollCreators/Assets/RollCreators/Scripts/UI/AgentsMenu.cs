using System;
using UnityEngine;
using UnityEngine.UI;

public class AgentsMenu : MonoBehaviour
{
    private static string[] DAY_AGENTS_NAMES =
    {
        "Клирик", "Дракон", "Священник"
    };

    private static string[] NIGHT_AGENTS_NAMES =
    {
        "Мошенник", "Скользкий тип", "Тайный агент"
    };

    private static Agent.Skills[] SKILLS_INDEX =
    {
        Agent.Skills.ELOQUENCE, Agent.Skills.CUNNING, Agent.Skills.WISDOM, Agent.Skills.INSIGHT, Agent.Skills.CHARM, Agent.Skills.PERSUASIVENESS, Agent.Skills.PRESSURE
    };

    public Game game;
    public GameMenu gameMenu;
    public Text nameText;
    public Text typeText;
    public Text levelText;
    public Text experienceText;
    public Text skillsPointsText;
    public Text eloquenceText;
    public Text cunningText;
    public Text wisdomText;
    public Text insightText;
    public Text charmText;
    public Text persuasivenessText;
    public Text pressureText;
    public Text trainButtonText;
    private Agent currentAgent;

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void UpdateText(Game.DayTime dayTime)
    {
        nameText.text = $"Имя: {currentAgent.name}";
        typeText.text = $"Тип: {(dayTime == Game.DayTime.DAY ? "священнослужитель" : "негодяй")}";
        levelText.text = $"Уровень: {(dayTime == Game.DayTime.DAY ? DAY_AGENTS_NAMES[currentAgent.level - 1] : NIGHT_AGENTS_NAMES[currentAgent.level - 1])} ({currentAgent.level})";
        experienceText.text = $"Опыт: {currentAgent.experience}/{Agent.EXPERIENCE[currentAgent.level]}";
        skillsPointsText.text = $"Доступно очков навыков {currentAgent.skillPoints}";
        eloquenceText.text = $"Красноречие {currentAgent.skills[Agent.Skills.ELOQUENCE]}";
        cunningText.text = $"Хитрость {currentAgent.skills[Agent.Skills.CUNNING]}";
        wisdomText.text = $"Мудрость {currentAgent.skills[Agent.Skills.WISDOM]}";
        insightText.text = $"Проницательность {currentAgent.skills[Agent.Skills.INSIGHT]}";
        charmText.text = $"Обаяние {currentAgent.skills[Agent.Skills.CHARM]}";
        persuasivenessText.text = $"Убедительность {currentAgent.skills[Agent.Skills.PERSUASIVENESS]}";
        pressureText.text = $"Напор {currentAgent.skills[Agent.Skills.PRESSURE]}";
    }
    
    public void ChangeAgent(int index)
    {
        if (index < 4)
        {
            currentAgent = game.dayAgents[index];
            UpdateText(Game.DayTime.DAY);
        }
        else
        {
            currentAgent = game.nightAgents[index - 4];
            UpdateText(Game.DayTime.NIGHT);
        }
        trainButtonText.text = $"Повысить уровень {Agent.EXPERIENCE[currentAgent.level] * 50} золота";
    }

    public void ImproveSkill(int index)
    {
        if (currentAgent.skillPoints == 0 || currentAgent.skills[SKILLS_INDEX[index]] == 10) return;
        currentAgent.skillPoints--;
        currentAgent.skills[SKILLS_INDEX[index]]++;
        UpdateText(currentAgent is DayAgent ? Game.DayTime.DAY : Game.DayTime.NIGHT);
    }
    
    public void NewAgent()
    {
        if (currentAgent is DayAgent)
        {
            DayAgent agent = (DayAgent) currentAgent;
            agent.task = DayAgent.DayTask.CHANGE_AGENT;
        }
        else
        {
            NightAgent agent = (NightAgent) currentAgent;
            agent.task = NightAgent.NightTask.CHANGE_AGENT;
        }
        gameMenu.UpdateAgentButtons();
    }

    public void TrainAgent()
    {
        if (currentAgent is DayAgent)
        {
            DayAgent agent = (DayAgent) currentAgent;
            agent.task = DayAgent.DayTask.TRAIN_AGENT;
            agent.tempInt = Agent.EXPERIENCE[agent.level];
        }
        else
        {
            NightAgent agent = (NightAgent) currentAgent;
            agent.task = NightAgent.NightTask.TRAIN_AGENT;
            agent.tempInt = Agent.EXPERIENCE[agent.level];
        }
        gameMenu.UpdateAgentButtons();
    }

    private void OnEnable()
    {
        ChangeAgent(0);
    }
}
