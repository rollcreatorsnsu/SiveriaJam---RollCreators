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

    public int fearOfGod;
    public float wealth;
    public Dictionary<Sins, int> sins = new Dictionary<Sins, int>();
    public int strength;
    public bool fearOfGodOpened = false;
    public bool wealthOpened = false;
    public bool sinsOpened = false;
    private SocialStatus status;

    public Sinner(SocialStatus status)
    {
        this.status = status;
        switch (status)
        {
            case SocialStatus.NOBLEMAN:
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
                break;
            case SocialStatus.CITIZEN:
                sins.Add(Sins.VANITY, 20);
                sins.Add(Sins.ENVY, 40);
                sins.Add(Sins.ANGER, 20);
                sins.Add(Sins.GLOOM, 15);
                sins.Add(Sins.GREED, 30);
                sins.Add(Sins.GLUTTONY, 30);
                sins.Add(Sins.FORNICATION, 40);
                fearOfGod = 20;
                wealth = 0.25f;
                strength = 400;
                break;
            case SocialStatus.PEASANT:
                sins.Add(Sins.VANITY, 10);
                sins.Add(Sins.ENVY, 30);
                sins.Add(Sins.ANGER, 20);
                sins.Add(Sins.GLOOM, 30);
                sins.Add(Sins.GREED, 40);
                sins.Add(Sins.GLUTTONY, 20);
                sins.Add(Sins.FORNICATION, 30);
                fearOfGod = 35;
                wealth = 1;
                strength = 1200;
                break;
            case SocialStatus.GARBAGE:
                sins.Add(Sins.VANITY, 5);
                sins.Add(Sins.ENVY, 30);
                sins.Add(Sins.ANGER, 30);
                sins.Add(Sins.GLOOM, 40);
                sins.Add(Sins.GREED, 20);
                sins.Add(Sins.GLUTTONY, 10);
                sins.Add(Sins.FORNICATION, 30);
                fearOfGod = 35;
                wealth = 0.5f;
                strength = 2000;
                break;
        }
    }

    public void Hide()
    {
        fearOfGodOpened = false;
        wealthOpened = false;
        sinsOpened = false;
    }

    public void MorningUpdate()
    {
        switch (status)
        {
            case SocialStatus.NOBLEMAN:
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
                break;
            case SocialStatus.CITIZEN:
                sins[Sins.VANITY] += Random.Range(-5, 10);
                sins[Sins.ENVY] += Random.Range(-5, 10);
                sins[Sins.ANGER] += Random.Range(-5, 5);
                sins[Sins.GLOOM] += Random.Range(-5, 5);
                sins[Sins.GREED] += Random.Range(-5, 5);
                sins[Sins.GLUTTONY] += Random.Range(-5, 10);
                sins[Sins.FORNICATION] += Random.Range(-5, 5);
                fearOfGod += Random.Range(-5, 5);
                wealth += Random.Range(-0.25f, 0.25f);
                strength += Random.Range(-30, 30);
                break;
            case SocialStatus.PEASANT:
                sins[Sins.VANITY] += Random.Range(-5, 5);
                sins[Sins.ENVY] += Random.Range(-5, 10);
                sins[Sins.ANGER] += Random.Range(-5, 5);
                sins[Sins.GLOOM] += Random.Range(-5, 10);
                sins[Sins.GREED] += Random.Range(-5, 10);
                sins[Sins.GLUTTONY] += Random.Range(-5, 5);
                sins[Sins.FORNICATION] += Random.Range(-5, 5);
                fearOfGod += Random.Range(-5, 10);
                wealth += Random.Range(-0.25f, 0.25f);
                strength += Random.Range(-50, 50);
                break;
            case SocialStatus.GARBAGE:
                sins[Sins.VANITY] += Random.Range(-5, 5);
                sins[Sins.ENVY] += Random.Range(-5, 10);
                sins[Sins.ANGER] += Random.Range(-5, 5);
                sins[Sins.GLOOM] += Random.Range(-5, 10);
                sins[Sins.GREED] += Random.Range(-5, 5);
                sins[Sins.GLUTTONY] += Random.Range(-5, 5);
                sins[Sins.FORNICATION] += Random.Range(-5, 10);
                fearOfGod += Random.Range(-5, 10);
                wealth += Random.Range(-0.25f, 0.25f);
                strength += Random.Range(-100, 100);
                break;
        }
        Clamp();
    }

    public void Clamp()
    {
        foreach (Sins sin in Enum.GetValues(typeof(Sins)))
        {
            sins[sin] = Mathf.Clamp(sins[sin], 0, 100);
        }

        fearOfGod = Mathf.Clamp(fearOfGod, 0, 100);
        wealth = Mathf.Clamp(wealth, 0, Int32.MaxValue);
        strength = Mathf.Clamp(strength, 0, Int32.MaxValue);
    }

    public void Reset()
    {
        switch (status)
        {
            case SocialStatus.NOBLEMAN:
                sins.Add(Sins.VANITY, 40);
                sins.Add(Sins.ENVY, 30);
                sins.Add(Sins.ANGER, 30);
                sins.Add(Sins.GLOOM, 5);
                sins.Add(Sins.GREED, 40);
                sins.Add(Sins.GLUTTONY, 40);
                sins.Add(Sins.FORNICATION, 30);
                fearOfGod = 10;
                break;
            case SocialStatus.CITIZEN:
                sins.Add(Sins.VANITY, 20);
                sins.Add(Sins.ENVY, 40);
                sins.Add(Sins.ANGER, 20);
                sins.Add(Sins.GLOOM, 15);
                sins.Add(Sins.GREED, 30);
                sins.Add(Sins.GLUTTONY, 30);
                sins.Add(Sins.FORNICATION, 40);
                fearOfGod = 20;
                break;
            case SocialStatus.PEASANT:
                sins.Add(Sins.VANITY, 10);
                sins.Add(Sins.ENVY, 30);
                sins.Add(Sins.ANGER, 20);
                sins.Add(Sins.GLOOM, 30);
                sins.Add(Sins.GREED, 40);
                sins.Add(Sins.GLUTTONY, 20);
                sins.Add(Sins.FORNICATION, 30);
                fearOfGod = 35;
                break;
            case SocialStatus.GARBAGE:
                sins.Add(Sins.VANITY, 5);
                sins.Add(Sins.ENVY, 30);
                sins.Add(Sins.ANGER, 30);
                sins.Add(Sins.GLOOM, 40);
                sins.Add(Sins.GREED, 20);
                sins.Add(Sins.GLUTTONY, 10);
                sins.Add(Sins.FORNICATION, 30);
                fearOfGod = 35;
                break;
        }
    }

}
