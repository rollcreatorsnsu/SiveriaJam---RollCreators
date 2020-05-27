using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DayAgent : Agent
{
    public enum DayTask
    {
        IDLE,
        LISTEN_TO_GOSSIP,
        GIVE_ALMS,
        SELL_MIRACULOUS_BALMS,
        SELL_INDULGENCES,
        SKILL_5,
        SKILL_6,
        PREACHING,
        SKILL_8,
        SKILL_9
    }
    public DayTask task = DayTask.IDLE;
    public Sinner.SocialStatus tempSocialStatus;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case DayTask.IDLE:
                lastResult = Int32.MaxValue;
                break;
            case DayTask.LISTEN_TO_GOSSIP:
                game.sinners[tempSocialStatus].daysOpened = skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING);
                lastResult = game.sinners[tempSocialStatus].daysOpened;
                break;
            case DayTask.GIVE_ALMS:
                if (game.gold < 100 * (1f + (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING)) / 10f))
                {
                    lastResult = Int32.MinValue;
                    break;
                }
                game.gold -= 100 * (1f + (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING)) / 10f);
                lastResult = game.attention * 5 * (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING)) / 100f;
                game.attention -= lastResult;
                break;
            case DayTask.SELL_MIRACULOUS_BALMS:
                if (game.sinners[tempSocialStatus].faith < 5 * (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING)) / 10f)
                {
                    lastResult = Int32.MinValue;
                    break;
                }
                game.sinners[tempSocialStatus].faith -= 5 * (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING)) / 10f;
                lastResult = game.sinners[tempSocialStatus].wealth * game.sinners[tempSocialStatus].strength * 5 *
                    (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING)) / 10f;
                if (perks.Contains(Perks.PERK_1))
                {
                    lastResult *= 1.1f;
                }
                game.gold += lastResult;
                break;
            case DayTask.SELL_INDULGENCES:
                lastResult = game.sinners[tempSocialStatus].wealth * game.sinners[tempSocialStatus].sins *
                    game.sinners[tempSocialStatus].faith * game.sinners[tempSocialStatus].strength / 10000;
                if (perks.Contains(Perks.PERK_1))
                {
                    lastResult *= 1.1f;
                }
                game.gold += lastResult;
                game.sinners[tempSocialStatus].strength =
                    (1 - game.sinners[tempSocialStatus].faith) * game.sinners[tempSocialStatus].strength;
                game.sinners[tempSocialStatus].Reset();
                break;
            case DayTask.SKILL_5:
                game.sinners[tempSocialStatus].daysBrokenSpecial = (skills[Skills.MIND] + AddSkillByPerk(Skills.MIND)) / 2 + AddDays();
                lastResult = game.sinners[tempSocialStatus].daysBrokenSpecial;
                break;
            case DayTask.SKILL_6:
                game.daysLowAttention = (skills[Skills.MIND] + AddSkillByPerk(Skills.MIND)) / 3 + AddDays();
                game.lowAttentionLevel = 5 * (skills[Skills.MIND] + AddSkillByPerk(Skills.MIND));
                lastResult = game.lowAttentionLevel;
                break;
            case DayTask.PREACHING:
                lastResult = 5 * (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT)) / 10f + AdditionalSinner();
                game.sinners[tempSocialStatus].faith += lastResult;
                game.sinners[tempSocialStatus].wealth += lastResult;
                break;
            case DayTask.SKILL_8:
                game.sinners[tempSocialStatus].daysHighFaith = (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT)) / 3 + AddDays();
                game.sinners[tempSocialStatus].levelHighFaith = 5 * (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT));
                lastResult = game.sinners[tempSocialStatus].levelHighFaith;
                break;
            case DayTask.SKILL_9:
                if (game.sinners[tempSocialStatus].sins < 5 * (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT)) / 10f)
                {
                    lastResult = Int32.MinValue;
                    break;
                }
                lastResult = game.sinners[tempSocialStatus].strength * game.sinners[tempSocialStatus].wealth * 5 * (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT)) / 10f;
                if (perks.Contains(Perks.PERK_1))
                {
                    lastResult *= 1.1f;
                }
                game.gold += lastResult;
                game.sinners[tempSocialStatus].sins -= 5 * (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT)) / 10f;
                break;
        }
        if (perks.Contains(Perks.PERK_7))
        {
            game.attention -= 5;
        }
    }

    public bool IsSkillAvailable(DayTask skill)
    {
        switch (skill)
        {
            case DayTask.GIVE_ALMS:
                return skills[Skills.CUNNING] >= 7;
            case DayTask.SELL_MIRACULOUS_BALMS:
                return skills[Skills.CUNNING] >= 9;
            case DayTask.SKILL_5:
                return skills[Skills.MIND] >= 7;
            case DayTask.SKILL_6:
                return skills[Skills.MIND] >= 9;
            case DayTask.SKILL_8:
                return skills[Skills.SPIRIT] >= 7;
            case DayTask.SKILL_9:
                return skills[Skills.SPIRIT] >= 9;
        }
        return true;
    }
}
