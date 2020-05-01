using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Agent : MonoBehaviour, IPointerClickHandler
{

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
        // TODO
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        agentMenu.currentAgent = this;
        agentMenu.Show();
    }
}
