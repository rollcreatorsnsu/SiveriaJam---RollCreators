using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        DEVELOP,
        CHANGE_AGENT,
        TRAIN_AGENT
    }
    public NightTask task = NightTask.IDLE;
    public Sinner.SocialStatus tempSocialStatus;
    public int tempInt;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case (NightTask.OPEN_FLAT):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.VANITY] += Random.Range(-30, 10) + 5 * skills[Skills.ELOQUENCE];
                game.attention += Random.Range(5, 15);
                break;
            }
            case (NightTask.MUCHLY_PRAISE):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.ENVY] += Random.Range(-30, 10) + 5 * skills[Skills.WISDOM];
                game.attention += Random.Range(5, 15);
                break;
            }
            case (NightTask.PROVOKE_TO_FIGHT):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.ANGER] += Random.Range(-30, 10) + 5 * skills[Skills.PRESSURE];
                game.attention += Random.Range(10, 20);
                break;
            }
            case (NightTask.COMPLAINT_ON_JUSTICE):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.GLOOM] += Random.Range(-30, 10) + 5 * skills[Skills.PERSUASIVENESS];
                game.attention += Random.Range(5, 15);
                break;
            }
            case (NightTask.DICE):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.GREED] += Random.Range(-30, 10) + 5 * skills[Skills.CUNNING];
                game.attention += Random.Range(0, 15);
                game.gold -= 25;
                break;
            }
            case (NightTask.TAKE_A_BREAK):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.GLUTTONY] += Random.Range(-30, 10) + 5 * skills[Skills.INSIGHT];
                game.attention += Random.Range(0, 10);
                game.gold -= 50;
                break;
            }
            case (NightTask.DEVELOP):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.FORNICATION] += Random.Range(-30, 10) + 5 * skills[Skills.CHARM];
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
            case (NightTask.TRAIN_AGENT):
            {
                game.gold -= 50 * tempInt;
                experience += tempInt;
                break;
            }
        }
    }
    
}
