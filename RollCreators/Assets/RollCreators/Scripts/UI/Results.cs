using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Game game;
    public List<Text> texts;

    private static Dictionary<DayAgent.DayTask, string> resultsDay = new Dictionary<DayAgent.DayTask, string>();
    private static Dictionary<NightAgent.NightTask, string> resultsNight = new Dictionary<NightAgent.NightTask, string>();

    static Results()
    {
        resultsDay.Add(DayAgent.DayTask.IDLE, "бездействовал");
        resultsDay.Add(DayAgent.DayTask.CONDUCT_A_SERVICE, "проводил службу");
        resultsDay.Add(DayAgent.DayTask.GIVE_ALMS, "раздавал милостыню");
        resultsDay.Add(DayAgent.DayTask.CONFESS_SINNERS, "исповедовал грешников");
        resultsDay.Add(DayAgent.DayTask.INTERPRETING_SACRED_TEXTS, "толковал священные тексты");
        resultsDay.Add(DayAgent.DayTask.LISTEN_TO_GOSSIP, "слушал сплетни");
        resultsDay.Add(DayAgent.DayTask.PREACH_IN_THE_CITY, "проповедовал в городе");
        resultsDay.Add(DayAgent.DayTask.SELL_INDULGENCE, "продавал индульгенцию");
        resultsNight.Add(NightAgent.NightTask.IDLE, "бездействовал");
        resultsNight.Add(NightAgent.NightTask.OPEN_FLAT, "неприкрыто льстил");
        resultsNight.Add(NightAgent.NightTask.MUCHLY_PRAISE, "ехидно похвалялся");
        resultsNight.Add(NightAgent.NightTask.PROVOKE_TO_FIGHT, "провоцировал на драку");
        resultsNight.Add(NightAgent.NightTask.COMPLAINT_ON_JUSTICE, "жаловался на несправедливость");
        resultsNight.Add(NightAgent.NightTask.DICE, "играл в кости");
        resultsNight.Add(NightAgent.NightTask.TAKE_A_BREAK, "закатывал пирушку");
        resultsNight.Add(NightAgent.NightTask.DEVELOP, "развратничал");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        
        for (int i = 0; i < 4; i++)
        {
            if (game.dayTime == Game.DayTime.DAY)
            {
                texts[i].text = $"Агент {i} - {resultsDay[game.dayAgents[i].task]}, - {(game.dayAgents[i].lastResult > 0 ? "успешно" : "провалено")} {(game.dayAgents[i].lastResult != Int32.MinValue && game.dayAgents[i].lastResult != Int32.MaxValue ? Math.Abs(game.dayAgents[i].lastResult).ToString() : "")}";
            }
            else
            {
                texts[i].text = $"Агент {i} - {resultsNight[game.nightAgents[i].task]}, - {(game.nightAgents[i].lastResult > 0 ? "успешно" : "провалено")} {(game.nightAgents[i].lastResult != Int32.MinValue && game.nightAgents[i].lastResult != Int32.MaxValue ? Math.Abs(game.nightAgents[i].lastResult).ToString() : "")}";
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
