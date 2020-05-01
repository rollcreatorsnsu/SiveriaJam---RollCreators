using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Agent : MonoBehaviour, IPointerClickHandler
{

    private static int[] EXPERIENCE = {
        0, 10, 25, 50
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

    public string name;
    public int experience;
    public int level;
    public Dictionary<Skills, int> skills = new Dictionary<Skills, int>();

    private AgentMenu agentMenu;

    void Start()
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

    public void OnPointerClick(PointerEventData eventData)
    {
        agentMenu.currentAgent = this;
        agentMenu.Show();
    }

    public void CheckNextLevel()
    {
        if (experience >= EXPERIENCE[level])
        {
            agentMenu.currentAgent = this;
            agentMenu.ShowUpgradeMenu();
            level++;
        }
    }
}
