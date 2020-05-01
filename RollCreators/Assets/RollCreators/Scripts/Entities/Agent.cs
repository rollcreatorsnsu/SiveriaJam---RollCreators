using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public enum AgentType
    {
        CLERGYMAN,
        VILLAIN
    }

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
    public AgentType type;
    public int experience;
    public int level;
    public Dictionary<Skills, int> skills = new Dictionary<Skills, int>();

}
