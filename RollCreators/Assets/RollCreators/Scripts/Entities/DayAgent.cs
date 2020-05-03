using System;
using UnityEngine;
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
        SELL_INDULGENCE
    }
    public DayTask task = DayTask.IDLE;
    public Sinner.SocialStatus tempSocialStatus;
    public int tempInt;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case DayTask.IDLE:
                lastResult = Int32.MaxValue;
                break;
            case (DayTask.CONDUCT_A_SERVICE):
            {
                int oldValue = game.sinners[tempSocialStatus].fearOfGod;
                game.sinners[tempSocialStatus].fearOfGod += Random.Range(-30, 0) + 5 * skills[Skills.ELOQUENCE];
                game.sinners[tempSocialStatus].Clamp();
                int newValue = game.sinners[tempSocialStatus].fearOfGod;
                lastResult = newValue - oldValue;
                break;
            }
            case (DayTask.GIVE_ALMS):
            {
                if (game.gold < 10 * tempInt)
                {
                    lastResult = Int32.MinValue;
                    return;
                }
                game.gold -= 10 * tempInt;
                int oldValue = game.attention;
                game.attention += Random.Range(0, 10) - 3 * skills[Skills.CUNNING];
                game.attention = Mathf.Clamp(game.attention, 0, 100);
                int newValue = game.attention;
                lastResult = oldValue - newValue;
                break;
            }
            case (DayTask.CONFESS_SINNERS):
            {
                if (Random.Range(0, 100) <= game.sinners[tempSocialStatus].fearOfGod - 20 + 5 * skills[Skills.INSIGHT])
                {
                    game.sinners[tempSocialStatus].sinsOpened = true;
                    lastResult = Int32.MaxValue;
                }
                else
                {
                    lastResult = Int32.MinValue;
                }
                break;
            }
            case (DayTask.INTERPRETING_SACRED_TEXTS):
            {
                if (Random.Range(0, 100) <= Random.Range(10, 50) + 5 * skills[Skills.WISDOM])
                {
                    game.sinners[tempSocialStatus].fearOfGodOpened = true;
                    lastResult = Int32.MaxValue;
                }
                else
                {
                    lastResult = Int32.MinValue;
                }
                break;
            }
            case (DayTask.LISTEN_TO_GOSSIP):
            {
                if (Random.Range(0, 100) <= Random.Range(10, 50) + 5 * skills[Skills.CHARM])
                {
                    game.sinners[tempSocialStatus].wealthOpened = true;
                    lastResult = Int32.MaxValue;
                }
                else
                {
                    lastResult = Int32.MinValue;
                }
                break;
            }
            case (DayTask.PREACH_IN_THE_CITY):
            {
                lastResult = Random.Range(-10, 10) + 5 * skills[Skills.PERSUASIVENESS];
                game.sinners[tempSocialStatus].strength += lastResult;
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
                    sum += sinner.strength * sinner.fearOfGod * sins * skills[Skills.PRESSURE] * sinner.wealth / 70000;
                    sinner.strength -= sinner.strength * sinner.fearOfGod / 100;
                }

                lastResult = (int)sum;
                game.gold += sum;
                break;
            }
        }
    }
}
