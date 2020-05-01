using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinner : MonoBehaviour
{
    public enum SocialStatus
    {
        NOBLEMAN,
        CITIZEN,
        PEASANT,
        GARBAGE
    };

    public enum Sins
    {
        VANITY,
        ENVY,
        ANGER,
        GLOOM,
        GREED,
        GLUTTONY,
        FORNICATION
    }

    public string name;
    public SocialStatus socialStatus;
    public int fearOfGod;
    public int wealth;
    public Dictionary<Sins, int> sins = new Dictionary<Sins, int>();

}
