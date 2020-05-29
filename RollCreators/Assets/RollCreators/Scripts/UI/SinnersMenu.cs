using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinnersMenu : MonoBehaviour
{
    private static string[] DESCRIPTIONS =
    {
        "Неприлично богатые и непомерно развращённые дворяне - самый лакомый кусок. Впрочем, как правило, этим пресытившемся снобам нет никакого дела до спасения своей бессмертной души.",
        "Ремесленники, торгаши, студенты и ростовщики. Все они погрязли в грехах, но ещё не утратили надежду на избавление от вечных мук ада за пригоршню золота.",
        "Усердные, недалёкие и многочисленные. Эти несчастные готовы отдать вам последние гроши, лишь бы спасти свою душу от адского пламени на том свете.",
        "Всё разношёрстное городское дно, от жалких бродяга с дырой в кармане, калек и попрошаек до совестливых головорезов с тугим кошельком. Никогда наверняка не знаешь, чего от них ждать."
    };

    private static Sinner.SocialStatus[] STATUSES =
    {
        Sinner.SocialStatus.NOBLEMAN,
        Sinner.SocialStatus.CITIZEN,
        Sinner.SocialStatus.PEASANT,
        Sinner.SocialStatus.GARBAGE
    };

    public Game game;
    public GameMenu gameMenu;
    public AgentMenu agentMenu;
    public List<GameObject> activeButtons;
    public List<GameObject> inactiveButtons;
    public Text description;
    public Text wealth;
    public Text faith;
    public Text sins;
    public Text strength;
    public List<Text> specials;
    public GameObject specialsLock;
    public Text daysSpecialsLock;

    public void Show(int currentSinners)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            activeButtons[i].SetActive(i == currentSinners);
            inactiveButtons[i].SetActive(i != currentSinners);
        }
        description.text = DESCRIPTIONS[currentSinners];
        Sinner currentSinner = game.sinners[STATUSES[currentSinners]];
        wealth.text = $"{currentSinner.wealth}";
        faith.text = $"{currentSinner.faith}";
        sins.text = $"{currentSinner.sins}";
        strength.text = $"{currentSinner.strength}";
        if (currentSinner.daysBrokenSpecial > 0)
        {
            specialsLock.SetActive(true);
            daysSpecialsLock.text = $"{currentSinner.daysBrokenSpecial}";
        }
        else
        {
            specialsLock.SetActive(false);
        }
        for (int i = 0; i < specials.Count; i++)
        {
            specials[i].text = "";
        }
        switch (STATUSES[currentSinners])
        {
            case Sinner.SocialStatus.NOBLEMAN:
                specials[0].text = "Вера накаплвается на треть медленнее, а грех на четверть быстрее";
                break;
            case Sinner.SocialStatus.CITIZEN:
                specials[0].text =
                    "Если богатство больше чем у Дворян, грехи накпливаются вдвое быстрее, если ниже, чем у крестьян, то втрое медленнее";
                break;
            case Sinner.SocialStatus.PEASANT:
                specials[0].text = "Если Грехи какого-либо сословия (кроме крестьян) больше 70, вера накапливается в полтора раза быстрее, если Грехи какого-либо сосоловия (кроме крестьян) ниже 20, вера накапливается вполовину медленнее";
                break;
            case Sinner.SocialStatus.GARBAGE:
                specials[0].text =
                    "В начале каждого дня Вера и Гехи изменяюся на +5 или -7 (для каждого выбирается отдельно)";
                break;
        }
    }

    public void Close()
    {
        gameMenu.UpdateData();
        gameObject.SetActive(false);
    }

    public void ShowAgentMenu()
    {
        gameObject.SetActive(false);
        agentMenu.Show(0);
    }
}
