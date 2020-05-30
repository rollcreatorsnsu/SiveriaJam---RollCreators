public class DayAgent : Agent
{
    public enum DayTask
    {
        IDLE,
        ATTRACT_FOLLOWERS,
        GIVE_ALMS,
        SELL_MIRACULOUS_BALMS,
        SELL_INDULGENCES,
        CONDUCT_A_SERVICE,
        DISTRACT_THE_INQUISITION,
        PREACHING,
        JOINT_CHANTING,
        ACCEPTANCE_OF_THE_ELECT
    }
    public DayTask task = DayTask.IDLE;
    public Sinner.SocialStatus tempSocialStatus;

    public override void DoTask(Game game)
    {
        switch (task)
        {
            case DayTask.IDLE:
                break;
            case DayTask.ATTRACT_FOLLOWERS:
                game.sinners[tempSocialStatus].strength += GetFirstResult(game, tempSocialStatus, task);
                break;
            case DayTask.GIVE_ALMS:
                if (game.gold < -GetFirstResult(game, tempSocialStatus, task))
                {
                    break;
                }
                game.gold += GetFirstResult(game, tempSocialStatus, task);
                game.attention += GetSecondResult(game, tempSocialStatus, task);
                break;
            case DayTask.SELL_MIRACULOUS_BALMS:
                game.sinners[tempSocialStatus].faith += GetFirstResult(game, tempSocialStatus, task);
                game.gold += GetSecondResult(game, tempSocialStatus, task);
                break;
            case DayTask.SELL_INDULGENCES:
                game.gold += GetFirstResult(game, tempSocialStatus, task);
                game.sinners[tempSocialStatus].strength = GetSecondResult(game, tempSocialStatus, task);
                game.sinners[tempSocialStatus].Reset();
                break;
            case DayTask.CONDUCT_A_SERVICE:
                game.sinners[tempSocialStatus].daysBrokenSpecial = GetDaysResult(task);
                break;
            case DayTask.DISTRACT_THE_INQUISITION:
                game.daysLowAttention = GetDaysResult(task);
                game.lowAttentionLevel = GetFirstResult(game, tempSocialStatus, task);
                break;
            case DayTask.PREACHING:
                game.sinners[tempSocialStatus].faith += GetFirstResult(game, tempSocialStatus, task);
                break;
            case DayTask.JOINT_CHANTING:
                game.sinners[tempSocialStatus].daysHighFaith = GetDaysResult(task);
                game.sinners[tempSocialStatus].levelHighFaith = GetFirstResult(game, tempSocialStatus, task);
                break;
            case DayTask.ACCEPTANCE_OF_THE_ELECT:
                game.gold += GetFirstResult(game, tempSocialStatus, task);
                game.sinners[tempSocialStatus].sins += GetSecondResult(game, tempSocialStatus, task);
                break;
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
            case DayTask.CONDUCT_A_SERVICE:
                return skills[Skills.MIND] >= 7;
            case DayTask.DISTRACT_THE_INQUISITION:
                return skills[Skills.MIND] >= 9;
            case DayTask.JOINT_CHANTING:
                return skills[Skills.SPIRIT] >= 7;
            case DayTask.ACCEPTANCE_OF_THE_ELECT:
                return skills[Skills.SPIRIT] >= 9;
        }
        return true;
    }
    
    public float GetFirstResult(Game game, Sinner.SocialStatus tempSocialStatus, DayTask task)
    {
        switch (task)
        {
            case DayTask.ATTRACT_FOLLOWERS:
                return game.sinners[tempSocialStatus].strength * 0.03f * (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING));
            case DayTask.GIVE_ALMS:
                return -100 * (1f + (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING)) / 10f);
            case DayTask.SELL_MIRACULOUS_BALMS:
                return -game.sinners[tempSocialStatus].faith * 0.05f * (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING));
            case DayTask.SELL_INDULGENCES:
                return game.sinners[tempSocialStatus].wealth * game.sinners[tempSocialStatus].sins *
                    game.sinners[tempSocialStatus].faith * game.sinners[tempSocialStatus].strength / 10000 * GoldMultiplier();
            case DayTask.DISTRACT_THE_INQUISITION:
                return 0.05f * (skills[Skills.MIND] + AddSkillByPerk(Skills.MIND));
            case DayTask.PREACHING:
                return game.sinners[tempSocialStatus].faith * 0.05f *
                                                        (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT));
            case DayTask.JOINT_CHANTING:
                return 0.05f * (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT));
            case DayTask.ACCEPTANCE_OF_THE_ELECT:
                return game.sinners[tempSocialStatus].strength * game.sinners[tempSocialStatus].wealth * 5 * (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT)) / 10f * GoldMultiplier();
            case DayTask.CONDUCT_A_SERVICE:
                return 1;
        }
        return 0;
    }

    public float GetSecondResult(Game game, Sinner.SocialStatus tempSocialStatus, DayTask task)
    {
        switch (task)
        {
            case DayTask.GIVE_ALMS:
                return -game.attention * 5 * (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING)) / 100f * AttentionMultiplier();
            case DayTask.SELL_MIRACULOUS_BALMS:
                return game.sinners[tempSocialStatus].wealth * game.sinners[tempSocialStatus].strength * 5 *
                    (skills[Skills.CUNNING] + AddSkillByPerk(Skills.CUNNING)) / 100f * GoldMultiplier();
            case DayTask.SELL_INDULGENCES:
                return (1 - game.sinners[tempSocialStatus].faith / 100) * game.sinners[tempSocialStatus].strength;
            case DayTask.ACCEPTANCE_OF_THE_ELECT:
                return -game.sinners[tempSocialStatus].sins * 0.05f * (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT));
        }
        return 0;
    }

    public int GetDaysResult(DayTask task)
    {
        switch (task)
        {
            case DayTask.CONDUCT_A_SERVICE:
                return (skills[Skills.MIND] + AddSkillByPerk(Skills.MIND)) / 2 + AddDays();
            case DayTask.DISTRACT_THE_INQUISITION:
                return (skills[Skills.MIND] + AddSkillByPerk(Skills.MIND)) / 3 + AddDays();
            case DayTask.JOINT_CHANTING:
                return (skills[Skills.SPIRIT] + AddSkillByPerk(Skills.SPIRIT)) / 3 + AddDays();
        }
        return 0;
    }
}
