using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightAgent : Agent
{
    public enum NightTask
    {
        OPEN_FLAT,
        MUCHLY_PRAISE,
        PROVOKE_TO_FIGHT,
        COMPLAINT_ON_JUSTICE,
        DICE,
        TAKE_A_BREAK,
        DEVELOP,
        CHANGE_AGENT
    }
    public NightTask task;
    public Sinner.SocialStatus tempSocialStatus;
    public Sinner tempSinner;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case (NightTask.OPEN_FLAT):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        sinner.sins[Sinner.Sins.VANITY] += Random.Range(-30, 10) + 5 * skills[Skills.ELOQUENCE];
                    }
                }
                game.attention += Random.Range(5, 15);
                break;
            }
            case (NightTask.MUCHLY_PRAISE):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        sinner.sins[Sinner.Sins.ENVY] += Random.Range(-30, 10) + 5 * skills[Skills.WISDOM];
                    }
                }
                game.attention += Random.Range(5, 15);
                break;
            }
            case (NightTask.PROVOKE_TO_FIGHT):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        sinner.sins[Sinner.Sins.ANGER] += Random.Range(-30, 10) + 5 * skills[Skills.PRESSURE];
                    }
                }
                game.attention += Random.Range(10, 20);
                break;
            }
            case (NightTask.COMPLAINT_ON_JUSTICE):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        sinner.sins[Sinner.Sins.GLOOM] += Random.Range(-30, 10) + 5 * skills[Skills.PERSUASIVENESS];
                    }
                }
                game.attention += Random.Range(5, 15);
                break;
            }
            case (NightTask.DICE):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        sinner.sins[Sinner.Sins.GREED] += Random.Range(-30, 10) + 5 * skills[Skills.CUNNING];
                    }
                }
                game.attention += Random.Range(0, 15);
                game.gold -= 25;
                break;
            }
            case (NightTask.TAKE_A_BREAK):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        sinner.sins[Sinner.Sins.GLUTTONY] += Random.Range(-30, 10) + 5 * skills[Skills.INSIGHT];
                    }
                }
                game.attention += Random.Range(0, 10);
                game.gold -= 50;
                break;
            }
            case (NightTask.DEVELOP):
            {
                tempSinner.sins[Sinner.Sins.FORNICATION] += Random.Range(-30, 10) + 5 * skills[Skills.CHARM];
                game.attention += Random.Range(0, 10);
                game.gold -= 50;
                break;
            }
            case (NightTask.CHANGE_AGENT):
            {
                game.gold -= 200;
                SetNewAgent();
                break;
            }
        }
    }
    
}
