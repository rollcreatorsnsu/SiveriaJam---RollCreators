using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour 
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

    public Agent()
    {
        SetNewAgent();
    }

    public abstract void DoTask(Game game);

    public void SetNewAgent()
    {
        // TODO
    }

}
