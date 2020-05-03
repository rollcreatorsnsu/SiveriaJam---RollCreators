using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NightAgent : Agent
{
    public enum NightTask
    {
        IDLE,
        OPEN_FLAT,
        MUCHLY_PRAISE,
        PROVOKE_TO_FIGHT,
        COMPLAINT_ON_JUSTICE,
        DICE,
        TAKE_A_BREAK,
        DEVELOP
    }
    public NightTask task = NightTask.IDLE;
    public Sinner.SocialStatus tempSocialStatus;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case NightTask.IDLE:
                lastResult = Int32.MaxValue;
                break;
            case (NightTask.OPEN_FLAT):
            {
                int oldValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.VANITY];
                game.sinners[tempSocialStatus].sins[Sinner.Sins.VANITY] += Random.Range(-30, 10) + 2 * (skills[Skills.ELOQUENCE] + game.sinners[tempSocialStatus].sins[Sinner.Sins.VANITY] / 10);
                game.attention += Mathf.RoundToInt(Random.Range(5, 15) / 10f);
                game.sinners[tempSocialStatus].Clamp();
                int newValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.VANITY];
                lastResult = newValue - oldValue;
                break;
            }
            case (NightTask.MUCHLY_PRAISE):
            {
                int oldValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.ENVY];
                game.sinners[tempSocialStatus].sins[Sinner.Sins.ENVY] += Random.Range(-30, 10) + 2 * (skills[Skills.WISDOM] + game.sinners[tempSocialStatus].sins[Sinner.Sins.ENVY] / 10);
                game.attention += Mathf.RoundToInt(Random.Range(5, 15) / 10f);
                game.sinners[tempSocialStatus].Clamp();
                int newValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.ENVY];
                lastResult = newValue - oldValue;
                break;
            }
            case (NightTask.PROVOKE_TO_FIGHT):
            {
                int oldValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.ANGER];
                game.sinners[tempSocialStatus].sins[Sinner.Sins.ANGER] += Random.Range(-30, 10) + 2 * (skills[Skills.PRESSURE] + game.sinners[tempSocialStatus].sins[Sinner.Sins.ANGER] / 10);
                game.attention += Mathf.RoundToInt(Random.Range(10, 20) / 10f);
                game.sinners[tempSocialStatus].Clamp();
                int newValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.ANGER];
                lastResult = newValue - oldValue;
                break;
            }
            case (NightTask.COMPLAINT_ON_JUSTICE):
            {
                int oldValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.GLOOM];
                game.sinners[tempSocialStatus].sins[Sinner.Sins.GLOOM] += Random.Range(-30, 10) + 2 * (skills[Skills.PERSUASIVENESS] + game.sinners[tempSocialStatus].sins[Sinner.Sins.GLOOM] / 10);
                game.attention += Mathf.RoundToInt(Random.Range(5, 15) / 10f);
                game.sinners[tempSocialStatus].Clamp();
                int newValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.GLOOM];
                lastResult = newValue - oldValue;
                break;
            }
            case (NightTask.DICE):
            {
                if (game.gold < 25)
                {
                    lastResult = Int32.MinValue;
                    return;
                }

                int oldValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.GREED];
                game.sinners[tempSocialStatus].sins[Sinner.Sins.GREED] += Random.Range(-30, 10) + 2 * (skills[Skills.CUNNING] + game.sinners[tempSocialStatus].sins[Sinner.Sins.GREED] / 10);
                game.attention += Mathf.RoundToInt(Random.Range(0, 15) / 10f);
                game.gold -= 25;
                game.sinners[tempSocialStatus].Clamp();
                int newValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.GREED];
                lastResult = newValue - oldValue;
                break;
            }
            case (NightTask.TAKE_A_BREAK):
            {
                if (game.gold < 50)
                {
                    lastResult = Int32.MinValue;
                    return;
                }

                int oldValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.GLUTTONY];
                game.sinners[tempSocialStatus].sins[Sinner.Sins.GLUTTONY] += Random.Range(-30, 10) + 2 * (skills[Skills.INSIGHT] + game.sinners[tempSocialStatus].sins[Sinner.Sins.GLUTTONY] / 10);
                game.attention += Mathf.RoundToInt(Random.Range(0, 10) / 10f);
                game.gold -= 50;
                game.sinners[tempSocialStatus].Clamp();
                int newValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.GLUTTONY];
                lastResult = newValue - oldValue;
                break;
            }
            case (NightTask.DEVELOP):
            {
                if (game.gold < 50)
                {
                    lastResult = Int32.MinValue;
                    return;
                }

                int oldValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.FORNICATION];
                game.sinners[tempSocialStatus].sins[Sinner.Sins.FORNICATION] += Random.Range(-30, 10) + 2 * (skills[Skills.CHARM] + game.sinners[tempSocialStatus].sins[Sinner.Sins.FORNICATION] / 10);
                game.attention += Mathf.RoundToInt(Random.Range(0, 10) / 10f);
                game.gold -= 50;
                game.sinners[tempSocialStatus].Clamp();
                int newValue = game.sinners[tempSocialStatus].sins[Sinner.Sins.FORNICATION];
                lastResult = newValue - oldValue;
                break;
            }
        }
    }
    
}
