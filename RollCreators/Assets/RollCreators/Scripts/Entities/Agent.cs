using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Agent
{

    public static void ClearBusyNames()
    {
        __busyNames.Clear();
    }
    
    private static HashSet<string> __busyNames = new HashSet<string>();
    private static string[] __pullNames =
    {
        "Стас Барецкий",
        "Анна Беспалова",
        "Мария Воробьева",
        "Станислав Тренин",
        "Вячеслав Журавлев",
        "Егор Савенко",
        "Андрей Елисафенко",
        "Андрей Зыкин",
        "Андрей Терехин"
    };

    public static int[] EXPERIENCE = {
        0, 10, 15, 25
    };

    public enum Skills
    {
        CUNNING,
        MIND,
        SPIRIT
    }

    public enum Perks
    {
        PERK_1,
        PERK_2,
        PERK_3,
        PERK_4,
        PERK_5,
        PERK_6,
        PERK_7,
        PERK_8,
        PERK_9
    }

    private float _experience;
    public string name;
    public float lastResult = Int32.MinValue;
    public float experience { 
        get => _experience; 
        set
        {
            float tmp = value;
            while (tmp >= EXPERIENCE[level] && level <= 3)
            {
                level++;
                skillPoints += 2;
                tmp -= EXPERIENCE[level - 1];
            }

            _experience = tmp;
        } 
    }
    public int level;
    public Dictionary<Skills, int> skills = new Dictionary<Skills, int>();
    public int skillPoints = 0;
    public HashSet<Perks> perks = new HashSet<Perks>();

    public Agent()
    {
        SetNewAgent();
    }

    public abstract void DoTask(Game game);

    public void SetNewAgent()
    {
        perks.Clear();
        Perks[] allPerks = (Perks[])Enum.GetValues(typeof(Perks));
        perks.Add(allPerks[Random.Range(0, allPerks.Length)]);
        string newName = __pullNames[Random.Range(0, __pullNames.Length)];
        while (__busyNames.Contains(newName))
        {
            newName = __pullNames[Random.Range(0, __pullNames.Length)];
        }

        __busyNames.Remove(name);
        name = newName;
        __busyNames.Add(name);
        level = 1;
        skills[Skills.CUNNING] = 5 + Random.Range(-1, 1);
        skills[Skills.MIND] = 5 + Random.Range(-1, 1);
        skills[Skills.SPIRIT] = 5 + Random.Range(-1, 1);
    }

    public float NeedGold(int exp)
    {
        return perks.Contains(Perks.PERK_9) ? exp * 35 : exp * 50;
    }

    public void TrainAgent(Game game, int tempInt)
    {
        float needGold = NeedGold(tempInt);
        if (game.gold < needGold) return;
        game.gold -= needGold;
        experience += tempInt;
    }

    public void NewAgent(Game game)
    {
        if (game.gold < 200) return;
        game.gold -= 200;
        SetNewAgent();
    }
    
    protected int AddDays()
    {
        return perks.Contains(Perks.PERK_2) ? 1 : 0;
    }

    protected int AddSkillByPerk(Skills skill)
    {
        switch (skill)
        {
            case Skills.CUNNING:
                return perks.Contains(Perks.PERK_3) ? 1 : 0;
            case Skills.MIND:
                return perks.Contains(Perks.PERK_4) ? 1 : 0;
            case Skills.SPIRIT:
                return perks.Contains(Perks.PERK_5) ? 1 : 0;
        }
        return 0;
    }

    protected float AdditionalSinner()
    {
        return perks.Contains(Perks.PERK_8) ? 5 : 0;
    }

}
