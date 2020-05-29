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

    private float _faith;
    public float faith
    {
        get => _faith;
        set
        {
            float delta = value - _faith;
            if (daysBrokenSpecial == 0)
            {
                if (status == SocialStatus.NOBLEMAN)
                {
                    delta /= 3;
                }

                if (status == SocialStatus.CITIZEN)
                {
                    foreach (SocialStatus status in Enum.GetValues(typeof(SocialStatus)))
                    {
                        if (status != SocialStatus.CITIZEN && game.sinners[status].sins > 70)
                        {
                            delta *= 1.5f;
                            break;
                        }
                    }

                    foreach (SocialStatus status in Enum.GetValues(typeof(SocialStatus)))
                    {
                        if (status != SocialStatus.CITIZEN && game.sinners[status].sins < 20)
                        {
                            delta *= 0.5f;
                            break;
                        }
                    }
                }
            }

            if (daysHighFaith > 0)
            {
                delta += delta * levelHighFaith;
            }
            _faith += delta;
            Clamp();
        }
    }
    private float _wealth;

    public float wealth
    {
        get => _wealth;
        set
        {
            float delta = value - _wealth;
            if (daysHighWealth > 0)
            {
                delta += delta * levelHighWealth;
            }
            _wealth += delta;
            Clamp();
        }
    }
    
    private float _sins;

    public float sins
    {
        get => _sins;
        set
        {
            float delta = value - _sins;
            if (daysBrokenSpecial == 0)
            {
                if (status == SocialStatus.NOBLEMAN)
                {
                    delta *= 4;
                }

                if (status == SocialStatus.CITIZEN)
                {
                    if (wealth > game.sinners[SocialStatus.NOBLEMAN].wealth)
                    {
                        delta *= 2;
                    }

                    if (wealth < game.sinners[SocialStatus.CITIZEN].wealth)
                    {
                        delta /= 3;
                    }
                }
            }
            _sins += delta;
            Clamp();
        }
    }
    public float strength;
    public int daysBrokenSpecial = 0;
    public int daysHighFaith = 0;
    public float levelHighFaith = 0;
    public int daysHighWealth = 0;
    public float levelHighWealth = 0;
    private SocialStatus status;
    private float maxWealth;
    private int maxStrength;
    private Game game;

    public Sinner(SocialStatus status)
    {
        game = GameObject.Find("Game").GetComponent<Game>();
        this.status = status;
        switch (status)
        {
            case SocialStatus.NOBLEMAN:
                _sins = 40;
                _faith = 10;
                _wealth = 2;
                strength = 100;
                maxWealth = 5;
                maxStrength = 300;
                break;
            case SocialStatus.CITIZEN:
                _sins = 25;
                _faith = 20;
                _wealth = 1.25f;
                strength = 400;
                maxWealth = 2.25f;
                maxStrength = 1000;
                break;
            case SocialStatus.PEASANT:
                _sins = 10;
                _faith = 35;
                _wealth = 1;
                strength = 1200;
                maxWealth = 2;
                maxStrength = 2500;
                break;
            case SocialStatus.GARBAGE:
                _sins = 20;
                _faith = 35;
                _wealth = 0.5f;
                strength = 2000;
                maxWealth = 1;
                maxStrength = 5000;
                break;
        }
    }

    public void MorningUpdate()
    {
        if (status == SocialStatus.GARBAGE)
        {
            if (Random.value < 0.5)
            {
                faith += 5;
            }
            else
            {
                faith -= 7;
            }

            if (Random.value < 0.5)
            {
                sins += 5;
            }
            else
            {
                sins -= 7;
            }
        }

        if (daysBrokenSpecial > 0)
        {
            daysBrokenSpecial--;
        }

        if (daysHighFaith > 0)
        {
            daysHighFaith--;
        }

        if (daysHighWealth > 0)
        {
            daysHighWealth--;
        }

        Clamp();
    }

    public void Clamp()
    {
        _sins = Mathf.Clamp(_sins, 0, 100);
        _faith = Mathf.Clamp(_faith, 0, 100);
        _wealth = Mathf.Clamp(_wealth, 0, maxWealth);
        strength = Mathf.Clamp(strength, 0, maxStrength);
    }

    public void Reset()
    {
        switch (status)
        {
            case SocialStatus.NOBLEMAN:
                _sins = 40;
                _faith = 10;
                break;
            case SocialStatus.CITIZEN:
                _sins = 25;
                _faith = 20;
                break;
            case SocialStatus.PEASANT:
                _sins = 10;
                _faith = 35;
                break;
            case SocialStatus.GARBAGE:
                _sins = 20;
                _faith = 35;
                break;
        }
    }

    public float GetUntwisted()
    {
        return (sins * wealth * strength) / (maxStrength * maxWealth * 100);
    }

}
