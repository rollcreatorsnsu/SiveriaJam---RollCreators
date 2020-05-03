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

    public Game game;
    public Text wealth;
    public Text fearOfGod;
    public Text vanity;
    public Text envy;
    public Text anger;
    public Text gloom;
    public Text greed;
    public Text gluttony;
    public Text fornication;
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
        wealth.text = $"Богатство: {(sinner.wealthOpened ? sinner.wealth.ToString() : "???")}";
        fearOfGod.text = $"Богобоязненность: {(sinner.fearOfGodOpened ? sinner.fearOfGod.ToString() : "???")}";
        vanity.text = $"Гордыня: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.VANITY].ToString() : "???")}/100";
        envy.text = $"Зависть: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.ENVY].ToString() : "???")}/100";
        anger.text = $"Гнев: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.ANGER].ToString() : "???")}/100";
        gloom.text = $"Уныние: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.GLOOM].ToString() : "???")}/100";
        greed.text = $"Алчность: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.GREED].ToString() : "???")}/100";
        gluttony.text = $"Чревоугодие: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.GLUTTONY].ToString() : "???")}/100";
        fornication.text = $"Блуд: {(sinner.sinsOpened ? sinner.sins[Sinner.Sins.FORNICATION].ToString() : "???")}/100";
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ChangeSocialStatus(0);
    }
}
