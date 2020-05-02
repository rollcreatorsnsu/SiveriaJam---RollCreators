using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sinner
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
    public int fearOfGod;
    public float wealth;
    public Dictionary<Sins, int> sins = new Dictionary<Sins, int>();
    public int strength;
    public bool fearOfGodOpened = false;
    public bool wealthOpened = false;
    public bool sinsOpened = false;

    public Sinner()
    {
        sins.Add(Sins.VANITY, 40);
        sins.Add(Sins.ENVY, 30);
        sins.Add(Sins.ANGER, 30);
        sins.Add(Sins.GLOOM, 5);
        sins.Add(Sins.GREED, 40);
        sins.Add(Sins.GLUTTONY, 40);
        sins.Add(Sins.FORNICATION, 30);
        fearOfGod = 10;
        wealth = 2;
        strength = 100;
    }

    public void Hide()
    {
        fearOfGodOpened = false;
        wealthOpened = false;
        sinsOpened = false;
    }

    public void MorningUpdate()
    {
        sins[Sins.VANITY] += Random.Range(-5, 10);
        sins[Sins.ENVY] += Random.Range(-5, 5);
        sins[Sins.ANGER] += Random.Range(-5, 5);
        sins[Sins.GLOOM] += Random.Range(-5, 5);
        sins[Sins.GREED] += Random.Range(-5, 10);
        sins[Sins.GLUTTONY] += Random.Range(-5, 5);
        sins[Sins.FORNICATION] += Random.Range(-5, 5);
        fearOfGod += Random.Range(-5, 5);
        wealth += Random.Range(-0.5f, 0.5f);
        strength += Random.Range(-10, 10);
        Clamp();
    }

    public void Clamp()
    {
        foreach (Sins sin in Enum.GetValues(typeof(Sins)))
        {
            sins[sin] = Mathf.Clamp(sins[sin], 0, 100);
        }

        fearOfGod = Mathf.Clamp(fearOfGod, 0, 100);
    }

}
