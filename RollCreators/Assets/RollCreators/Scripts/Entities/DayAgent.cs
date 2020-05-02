using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class DayAgent : Agent
{
    public enum DayTask
    {
        IDLE,
        CONDUCT_A_SERVICE,
        GIVE_ALMS,
        CONFESS_SINNERS,
        INTERPRETING_SACRED_TEXTS,
        LISTEN_TO_GOSSIP,
        PREACH_IN_THE_CITY,
        SELL_INDULGENCE,
        CHANGE_AGENT,
        TRAIN_AGENT
    }
    public DayTask task = DayTask.IDLE;
    public Sinner.SocialStatus tempSocialStatus;
    public int tempInt;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case (DayTask.CONDUCT_A_SERVICE):
            {
                game.sinners[tempSocialStatus].fearOfGod += Random.Range(-30, 0) + 5 * skills[Skills.ELOQUENCE];
                break;
            }
            case (DayTask.GIVE_ALMS):
            {
                game.gold -= 10 * tempInt;
                game.attention += Random.Range(0, 10) - 3 * skills[Skills.CUNNING];
                break;
            }
            case (DayTask.CONFESS_SINNERS):
            {
                if (Random.Range(0, 100) <= game.sinners[tempSocialStatus].fearOfGod - 20 + 5 * skills[Skills.INSIGHT])
                {
                    game.sinners[tempSocialStatus].sinsOpened = true;
                }
                break;
            }
            case (DayTask.INTERPRETING_SACRED_TEXTS):
            {
                if (Random.Range(0, 100) <= Random.Range(10, 50) + 5 * skills[Skills.WISDOM])
                {
                    game.sinners[tempSocialStatus].fearOfGodOpened = true;
                }
                break;
            }
            case (DayTask.LISTEN_TO_GOSSIP):
            {
                if (Random.Range(0, 100) <= Random.Range(10, 50) + 5 * skills[Skills.CHARM])
                {
                    game.sinners[tempSocialStatus].wealthOpened = true;
                }
                break;
            }
            case (DayTask.PREACH_IN_THE_CITY):
            {
                game.sinners[tempSocialStatus].strength += Random.Range(-10, 10);
                break;
            }
            case (DayTask.SELL_INDULGENCE):
            {
                float sum = 0;
                foreach (Sinner sinner in game.sinners.Values)
                {
                    int sins = 0;
                    foreach (int sin in sinner.sins.Values)
                    {
                        sins += sin;
                    }
                    sum += sinner.strength * sinner.fearOfGod * sins * skills[Skills.PRESSURE] * sinner.wealth / 700000;
                    sinner.strength -= sinner.strength * sinner.fearOfGod / 100;
                }

                game.gold += sum;
                break;
            }
            case (DayTask.CHANGE_AGENT):
            {
                game.gold -= 200;
                SetNewAgent();
                break;
            }
            case (DayTask.TRAIN_AGENT):
            {
                game.gold -= 50 * tempInt;
                experience += tempInt;
                break;
            }
        }
    }
}
