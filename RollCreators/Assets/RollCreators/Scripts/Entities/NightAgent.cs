using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NightAgent : Agent
{
    public enum NightTask
    {
        IDLE,
        SKILL_10,
        SLANDER,
        ROBBERY,
        SKILL_13,
        SKILL_14,
        SKILL_15,
        SKILL_16,
        SKILL_17,
        SKILL_18
    }
    public NightTask task = NightTask.IDLE;
    public Sinner.SocialStatus tempSocialStatus;
    public int daysHighSkill = 0;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case NightTask.IDLE:
                lastResult = Int32.MaxValue;
                break;
            case NightTask.SKILL_10:
                lastResult = 5 * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) / 10f + AdditionalSinner();
                game.sinners[tempSocialStatus].sins += lastResult;
                game.attention += 2 * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) / 10f;
                break;
            case NightTask.SLANDER:
                lastResult = 2 * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) / 10f;
                game.sinners[tempSocialStatus].strength -= lastResult;
                game.attention += 4 * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) / 10f;
                break;
            case NightTask.ROBBERY:
                lastResult = game.sinners[tempSocialStatus].wealth * game.sinners[tempSocialStatus].strength * 5 * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) / 10f;
                if (perks.Contains(Perks.PERK_1))
                {
                    lastResult *= 1.1f;
                }
                game.gold += lastResult;
                game.attention += 5 * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) / 10f;
                break;
            case NightTask.SKILL_13:
                lastResult = 4 * (skills[Skills.MIND] + AddSkill(Skills.MIND)) / 10f + AdditionalSinner();
                game.sinners[tempSocialStatus].wealth += lastResult;
                game.attention += 3 * (skills[Skills.CUNNING] + AddSkill(Skills.MIND)) / 10f;
                break;
            case NightTask.SKILL_14:
                lastResult = Mathf.Floor((skills[Skills.MIND] + AddSkill(Skills.MIND)) / 2f);
                experience += lastResult;
                break;
            case NightTask.SKILL_15:
                if (game.gold < 50 * (skills[Skills.MIND] + AddSkill(Skills.MIND)))
                {
                    lastResult = Int32.MinValue;
                    break;
                }
                lastResult = 4 * (skills[Skills.MIND] + AddSkill(Skills.MIND)) / 10f + AdditionalSinner();
                game.sinners[tempSocialStatus].sins += lastResult;
                game.gold -= 50 * (skills[Skills.MIND] + AddSkill(Skills.MIND));
                break;
            case NightTask.SKILL_16:
                daysHighSkill = (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT)) / 3 + AddDays();
                lastResult = daysHighSkill;
                break;
            case NightTask.SKILL_17:
                if (game.sinners[tempSocialStatus].wealth < 5 * (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT)) / 10f)
                {
                    lastResult = Int32.MinValue;
                    break;
                }
                lastResult = 7 * (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT)) / 10f;
                game.sinners[tempSocialStatus].wealth -= lastResult;
                game.sinners[tempSocialStatus].wealth -= 5 * (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT)) / 10f;
                break;
            case NightTask.SKILL_18:
                game.sinners[tempSocialStatus].daysHighWealth = (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT)) / 3 + AddDays();
                game.sinners[tempSocialStatus].levelHighWealth = 2 * (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT));
                lastResult = game.sinners[tempSocialStatus].levelHighWealth;
                break;
        }
        if (perks.Contains(Perks.PERK_7))
        {
            game.attention -= 5;
        }
    }
    
    public bool IsSkillAvailable(NightTask skill)
    {
        switch (skill)
        {
            case NightTask.SLANDER:
                return skills[Skills.CUNNING] >= 7;
            case NightTask.ROBBERY:
                return skills[Skills.CUNNING] >= 9;
            case NightTask.SKILL_14:
                return skills[Skills.MIND] >= 7;
            case NightTask.SKILL_15:
                return skills[Skills.MIND] >= 9;
            case NightTask.SKILL_17:
                return skills[Skills.SPIRIT] >= 7;
            case NightTask.SKILL_18:
                return skills[Skills.SPIRIT] >= 9;
        }
        return true;
    }

    private int AddSkill(Skills skill)
    {
        return (daysHighSkill > 0 ? 1 : 0) + AddSkillByPerk(skill);
    }
    
}
