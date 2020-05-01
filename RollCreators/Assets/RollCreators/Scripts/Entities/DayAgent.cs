using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class DayAgent : Agent
{
    public enum DayTask
    {
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
    public DayTask task;
    public Sinner.SocialStatus tempSocialStatus;
    public int tempInt;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case (DayTask.CONDUCT_A_SERVICE):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        sinner.fearOfGod += Random.Range(-30, 0) + 5 * skills[Skills.ELOQUENCE];
                    }
                }
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
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        if (Random.Range(0, 100) <= sinner.fearOfGod - 20 + 5 * skills[Skills.INSIGHT])
                        {
                            sinner.sinsOpened = true;
                        }
                    }
                }
                break;
            }
            case (DayTask.INTERPRETING_SACRED_TEXTS):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        if (Random.Range(0, 100) <= Random.Range(10, 50) + 5 * skills[Skills.WISDOM])
                        {
                            sinner.sinsOpened = true;
                        }
                    }
                }
                break;
            }
            case (DayTask.LISTEN_TO_GOSSIP):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        if (Random.Range(0, 100) <= Random.Range(10, 50) + 5 * skills[Skills.CHARM])
                        {
                            sinner.sinsOpened = true;
                        }
                    }
                }
                break;
            }
            case (DayTask.PREACH_IN_THE_CITY):
            {
                foreach (Sinner sinner in game.sinners)
                {
                    if (sinner.socialStatus == tempSocialStatus)
                    {
                        sinner.strength++; // TODO
                    }
                }
                break;
            }
            case (DayTask.SELL_INDULGENCE):
            {
                int sum = 0;
                foreach (Sinner sinner in game.sinners)
                {
                    int sins = 0;
                    foreach (Sinner.Sins sin in Enum.GetValues(typeof(Sinner.Sins)))
                    {
                        sins += sinner.sins[sin];
                    }
                    sum += sinner.strength * sinner.fearOfGod * sins * skills[Skills.PRESSURE] * sinner.wealth / 700000;
                    sinner.strength -= sinner.strength * sinner.fearOfGod / 100;
                }
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
