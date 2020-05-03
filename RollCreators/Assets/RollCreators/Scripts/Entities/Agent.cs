﻿using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Agent
{

    public static int[] EXPERIENCE = {
        0, 10, 15, 25
    };

    public enum Skills
    {
        ELOQUENCE,
        CUNNING,
        WISDOM,
        INSIGHT,
        CHARM,
        PERSUASIVENESS,
        PRESSURE
    }

    private int _experience;
    public string name;
    public int experience { 
        get => _experience; 
        set
        {
            int tmp = value;
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

    private AgentMenu agentMenu;

    public Agent()
    {
        agentMenu = GameObject.Find("Game").GetComponent<AgentMenu>();
        SetNewAgent();
    }

    public abstract void DoTask(Game game);

    public void SetNewAgent()
    {
        level = 1;
        skills[Skills.ELOQUENCE] = 4 + Random.Range(-2, 2);
        skills[Skills.CUNNING] = 4 + Random.Range(-2, 2);
        skills[Skills.WISDOM] = 4 + Random.Range(-2, 2);
        skills[Skills.INSIGHT] = 4 + Random.Range(-2, 2);
        skills[Skills.CHARM] = 4 + Random.Range(-2, 2);
        skills[Skills.PERSUASIVENESS] = 4 + Random.Range(-2, 2);
        skills[Skills.PRESSURE] = 4 + Random.Range(-2, 2);
    }

    public void TrainAgent(Game game, int tempInt)
    {
        if (game.gold < 50 * tempInt) return;
        game.gold -= 50 * tempInt;
        experience += tempInt;
    }

    public void NewAgent(Game game)
    {
        if (game.gold < 200) return;
        game.gold -= 200;
        SetNewAgent();
    }

}
