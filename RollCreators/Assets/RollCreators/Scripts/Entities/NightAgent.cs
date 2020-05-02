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
                game.sinners[tempSocialStatus].sins[Sinner.Sins.VANITY] += Random.Range(-30, 10) + 2 * (skills[Skills.ELOQUENCE] + game.sinners[tempSocialStatus].sins[Sinner.Sins.VANITY] / 10);
                game.attention += Random.Range(5, 15);
                game.sinners[tempSocialStatus].Clamp();
                break;
            }
            case (NightTask.MUCHLY_PRAISE):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.ENVY] += Random.Range(-30, 10) + 2 * (skills[Skills.WISDOM] + game.sinners[tempSocialStatus].sins[Sinner.Sins.ENVY] / 10);
                game.attention += Random.Range(5, 15);
                game.sinners[tempSocialStatus].Clamp();
                break;
            }
            case (NightTask.PROVOKE_TO_FIGHT):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.ANGER] += Random.Range(-30, 10) + 2 * (skills[Skills.PRESSURE] + game.sinners[tempSocialStatus].sins[Sinner.Sins.ANGER] / 10);
                game.attention += Random.Range(10, 20);
                game.sinners[tempSocialStatus].Clamp();
                break;
            }
            case (NightTask.COMPLAINT_ON_JUSTICE):
            {
                game.sinners[tempSocialStatus].sins[Sinner.Sins.GLOOM] += Random.Range(-30, 10) + 2 * (skills[Skills.PERSUASIVENESS] + game.sinners[tempSocialStatus].sins[Sinner.Sins.GLOOM] / 10);
                game.attention += Random.Range(5, 15);
                game.sinners[tempSocialStatus].Clamp();
                break;
            }
            case (NightTask.DICE):
            {
                if (game.gold < 25) return;
                game.sinners[tempSocialStatus].sins[Sinner.Sins.GREED] += Random.Range(-30, 10) + 2 * (skills[Skills.CUNNING] + game.sinners[tempSocialStatus].sins[Sinner.Sins.GREED] / 10);
                game.attention += Random.Range(0, 15);
                game.gold -= 25;
                game.sinners[tempSocialStatus].Clamp();
                break;
            }
            case (NightTask.TAKE_A_BREAK):
            {
                if (game.gold < 50) return;
                game.sinners[tempSocialStatus].sins[Sinner.Sins.GLUTTONY] += Random.Range(-30, 10) + 2 * (skills[Skills.INSIGHT] + game.sinners[tempSocialStatus].sins[Sinner.Sins.GLUTTONY] / 10);
                game.attention += Random.Range(0, 10);
                game.gold -= 50;
                game.sinners[tempSocialStatus].Clamp();
                break;
            }
            case (NightTask.DEVELOP):
            {
                if (game.gold < 50) return;
                game.sinners[tempSocialStatus].sins[Sinner.Sins.FORNICATION] += Random.Range(-30, 10) + 2 * (skills[Skills.CHARM] + game.sinners[tempSocialStatus].sins[Sinner.Sins.FORNICATION] / 10);
                game.attention += Random.Range(0, 10);
                game.gold -= 50;
                game.sinners[tempSocialStatus].Clamp();
                break;
            }
            case (NightTask.CHANGE_AGENT):
            {
                if (game.gold < 200) return;
                game.gold -= 200;
                SetNewAgent();
                break;
            }
            case (NightTask.TRAIN_AGENT):
            {
                if (game.gold < 50 * tempInt) return;
                game.gold -= 50 * tempInt;
                experience += tempInt;
                break;
            }
        }
    }
    
}
