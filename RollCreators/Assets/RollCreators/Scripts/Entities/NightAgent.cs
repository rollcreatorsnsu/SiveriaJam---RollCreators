using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NightAgent : Agent
{
    public enum NightTask
    {
        IDLE,
        MANIPULATION,
        SLANDER,
        ROBBERY,
        CORRUPTION,
        TRAINING,
        TEMPT,
        TEMPERING_OF_SPIRIT,
        THE_FALL,
        PROPAGANDA
    }
    public NightTask task = NightTask.IDLE;
    public Sinner.SocialStatus tempSocialStatus;
    public int daysHighSkill = 0;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case NightTask.IDLE:
                break;
            case NightTask.MANIPULATION:
                game.sinners[tempSocialStatus].sins += GetFirstResult(game, tempSocialStatus, task);
                game.attention += GetSecondResult(game, tempSocialStatus, task);
                break;
            case NightTask.SLANDER:
                game.sinners[tempSocialStatus].strength += GetFirstResult(game, tempSocialStatus, task);
                game.attention += GetSecondResult(game, tempSocialStatus, task);
                break;
            case NightTask.ROBBERY:
                game.gold += GetFirstResult(game, tempSocialStatus, task);
                game.attention += GetSecondResult(game, tempSocialStatus, task);
                break;
            case NightTask.CORRUPTION:
                game.sinners[tempSocialStatus].wealth += GetFirstResult(game, tempSocialStatus, task);
                game.attention += GetSecondResult(game, tempSocialStatus, task);
                break;
            case NightTask.TRAINING:
                experience += (int)GetFirstResult(game, tempSocialStatus, task);
                break;
            case NightTask.TEMPT:
                if (game.gold < -GetSecondResult(game, tempSocialStatus, task))
                {
                    break;
                }
                game.sinners[tempSocialStatus].sins += GetFirstResult(game, tempSocialStatus, task);
                game.gold += GetSecondResult(game, tempSocialStatus, task);
                break;
            case NightTask.TEMPERING_OF_SPIRIT:
                daysHighSkill = GetDaysResult(task);
                break;
            case NightTask.THE_FALL:
                game.sinners[tempSocialStatus].sins += GetFirstResult(game, tempSocialStatus, task);
                game.sinners[tempSocialStatus].wealth += GetSecondResult(game, tempSocialStatus, task);
                break;
            case NightTask.PROPAGANDA:
                game.daysHighGold = GetDaysResult(task);
                game.highGoldLevel = GetFirstResult(game, tempSocialStatus, task);
                break;
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
            case NightTask.TRAINING:
                return skills[Skills.MIND] >= 7;
            case NightTask.TEMPT:
                return skills[Skills.MIND] >= 9;
            case NightTask.THE_FALL:
                return skills[Skills.SPIRIT] >= 7;
            case NightTask.PROPAGANDA:
                return skills[Skills.SPIRIT] >= 9;
        }
        return true;
    }

    private int AddSkill(Skills skill)
    {
        return (daysHighSkill > 0 ? 1 : 0) + AddSkillByPerk(skill);
    }

    public float GetFirstResult(Game game, Sinner.SocialStatus tempSocialStatus, NightTask task)
    {
        switch (task)
        {
            case NightTask.MANIPULATION:
                return game.sinners[tempSocialStatus].sins * (0.05f * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) + AdditionalSinner());
            case NightTask.SLANDER:
                return -game.sinners[tempSocialStatus].strength * (0.02f * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) + AdditionalSinner());
            case NightTask.ROBBERY:
                return game.sinners[tempSocialStatus].wealth * game.sinners[tempSocialStatus].strength * 5 * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) / 10f * GoldMultiplier();
            case NightTask.CORRUPTION:
                return game.sinners[tempSocialStatus].wealth * (0.04f * (skills[Skills.MIND] + AddSkill(Skills.MIND)) + AdditionalSinner());
            case NightTask.TRAINING:
                return Mathf.FloorToInt((skills[Skills.MIND] + AddSkill(Skills.MIND)) / 2f);
            case NightTask.TEMPT:
                return game.sinners[tempSocialStatus].sins * (0.04f * (skills[Skills.MIND] + AddSkill(Skills.MIND)) + AdditionalSinner());
            case NightTask.THE_FALL:
                return game.sinners[tempSocialStatus].sins * (0.07f * (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT)) + AdditionalSinner());
            case NightTask.PROPAGANDA:
                return 0.02f * (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT));
            case NightTask.TEMPERING_OF_SPIRIT:
                return 1;
        }
        return 0;
    }

    public float GetSecondResult(Game game, Sinner.SocialStatus tempSocialStatus, NightTask task)
    {
        switch (task)
        {
            case NightTask.MANIPULATION:
                return game.attention * 0.02f * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) * AttentionMultiplier();
            case NightTask.SLANDER:
                return game.attention * 0.04f * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) * AttentionMultiplier();
            case NightTask.ROBBERY:
                return game.attention * 0.05f * (skills[Skills.CUNNING] + AddSkill(Skills.CUNNING)) * AttentionMultiplier();
            case NightTask.CORRUPTION:
                return game.attention * 0.03f * (skills[Skills.CUNNING] + AddSkill(Skills.MIND)) * AttentionMultiplier();
            case NightTask.TEMPT:
                return 50 * (skills[Skills.MIND] + AddSkill(Skills.MIND));
            case NightTask.THE_FALL:
                return -game.sinners[tempSocialStatus].wealth * 0.05f * (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT));
        }
        return 0;
    }

    public int GetDaysResult(NightTask task)
    {
        switch (task)
        {
            case NightTask.TEMPERING_OF_SPIRIT:
                return (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT)) / 3 + AddDays();
            case NightTask.PROPAGANDA:
                return (skills[Skills.SPIRIT] + AddSkill(Skills.SPIRIT)) / 3;
        }
        return 0;
    }
    
}
