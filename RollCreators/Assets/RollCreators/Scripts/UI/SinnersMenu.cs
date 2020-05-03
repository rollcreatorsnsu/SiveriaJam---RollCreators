using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinnersMenu : MonoBehaviour
{
    private static Sinner.SocialStatus[] STATUSES =
    {
        Sinner.SocialStatus.NOBLEMAN,
        Sinner.SocialStatus.CITIZEN,
        Sinner.SocialStatus.PEASANT,
        Sinner.SocialStatus.GARBAGE
    };

    private static string[] DESCRIPTIONS =
    {
        "Неприлично богатые и непомерно развращённые дворяне - самый лакомый кусок. Впрочем, как правило, этим пресытившемся снобам нет никакого дела до спасения своей бессмертной души.",
        "Ремесленники, торгаши, студенты и ростовщики. Все они погрязли в грехах, но ещё не утратили надежду на избавление от вечных мук ада за пригоршню золота.",
        "Усердные, недалёкие и многочисленные. Эти несчастные готовы отдать вам последние гроши, лишь бы спасти свою душу от адского пламени на том свете.",
        "Всё разношёрстное городское дно, от жалких бродяга с дырой в кармане, калек и попрошаек до совестливых головорезов с тугим кошельком. Никогда наверняка не знаешь, чего от них ждать."
    };

    public Game game;
    public Text strength;
    public Text wealth;
    public Text fearOfGod;
    public Text vanity;
    public Text envy;
    public Text anger;
    public Text gloom;
    public Text greed;
    public Text gluttony;
    public Text fornication;
    public Text description;
    public List<Button> buttons;
    public Sprite activeButton;
    public Sprite inactiveButton;
    
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void ChangeSocialStatus(int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == index)
            {
                buttons[i].image.sprite = activeButton;
                buttons[i].GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1, 1);
            }
            else
            {
                buttons[i].image.sprite = inactiveButton;
                buttons[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
        Sinner sinner = game.sinners[STATUSES[index]];
        strength.text = $"Численность: {sinner.strength}";
        wealth.text = $"Богатство: {(sinner.wealthOpened ? sinner.wealth.ToString() : "???")}";
        fearOfGod.text = $"Богобоязненность: {(sinner.fearOfGodOpened ? sinner.fearOfGod.ToString() : "???")}";
        vanity.text = $"Гордыня: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.VANITY].ToString() : "???")}%";
        envy.text = $"Зависть: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.ENVY].ToString() : "???")}%";
        anger.text = $"Гнев: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.ANGER].ToString() : "???")}%";
        gloom.text = $"Уныние: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.GLOOM].ToString() : "???")}%";
        greed.text = $"Алчность: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.GREED].ToString() : "???")}%";
        gluttony.text = $"Чревоугодие: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.GLUTTONY].ToString() : "???")}%";
        fornication.text = $"Блуд: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.FORNICATION].ToString() : "???")}%";
        description.text = DESCRIPTIONS[index];
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ChangeSocialStatus(0);
    }
}
