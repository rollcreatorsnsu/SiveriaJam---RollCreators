using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Game game;
    public SettingsMenu settingsMenu;
    public SinnersMenu sinnersMenu;
    public AgentMenu agentMenu;
    public List<Text> untwistedTexts;
    public Slider attentionSlider;
    public Text attentionText;
    public Image changeDayButton;
    public Sprite changeDayButtonDay;
    public Sprite changeDayButtonNight;
    public Image agentPanel;
    public List<Image> agentButtons;
    public List<Image> agentPortraits;
    public List<Image> agentTicks;
    public List<Text> agentNames;
    public Sprite agentPanelDay;
    public Sprite agentPanelNight;
    public Sprite agentButtonDay;
    public Sprite agentButtonNight;
    public Sprite agentPortraitDay;
    public Sprite agentPortraitNight;
    public Sprite agentTickDay;
    public Sprite agentTickNight;
    public Text daysRemainedText;
    public List<Slider> aimSliders;
    public List<Text> aimTexts;
    public GameObject longGoldBuff;
    public Text longGoldBuffLevel;
    public Text longGoldBuffDays;
    public GameObject longAttentionDebuff;
    public Text longAttentionDebuffLevel;
    public Text longAttentionDebuffDays;

    public void ShowSettings()
    {
        settingsMenu.Show();
    }

    public void ShowSinners()
    {
        sinnersMenu.Show(0);
    }

    public void ChangeDayTime()
    {
        game.ChangeDayTime();
    }

    public void ShowAgentMenu(int currentAgent)
    {
        agentMenu.Show(currentAgent);
    }

    public void UpdateData()
    {
        untwistedTexts[0].text = $"{Mathf.FloorToInt(game.sinners[Sinner.SocialStatus.NOBLEMAN].GetUntwisted())}%";
        untwistedTexts[1].text = $"{Mathf.FloorToInt(game.sinners[Sinner.SocialStatus.CITIZEN].GetUntwisted())}%";
        untwistedTexts[2].text = $"{Mathf.FloorToInt(game.sinners[Sinner.SocialStatus.PEASANT].GetUntwisted())}%";
        untwistedTexts[3].text = $"{Mathf.FloorToInt(game.sinners[Sinner.SocialStatus.GARBAGE].GetUntwisted())}%";
        attentionSlider.value = game.attention;
        attentionText.text = $"{game.attention}/100";
        if (game.dayTime == Game.DayTime.DAY)
        {
            changeDayButton.sprite = changeDayButtonDay;
            agentPanel.sprite = agentPanelDay;
            for (int i = 0; i < 4; i++)
            {
                agentButtons[i].sprite = agentButtonDay;
                agentPortraits[i].sprite = agentPortraitDay;
                agentTicks[i].sprite = agentTickDay;
                agentTicks[i].gameObject.SetActive(game.dayAgents[i].task != DayAgent.DayTask.IDLE);
                agentNames[i].text = $"{game.dayAgents[i].name}";
            }
        }
        else
        {
            changeDayButton.sprite = changeDayButtonNight;
            agentPanel.sprite = agentPanelNight;
            for (int i = 0; i < 4; i++)
            {
                agentButtons[i].sprite = agentButtonNight;
                agentPortraits[i].sprite = agentPortraitNight;
                agentTicks[i].sprite = agentTickNight;
                agentTicks[i].gameObject.SetActive(game.nightAgents[i].task != NightAgent.NightTask.IDLE);
                agentNames[i].text = $"{game.nightAgents[i].name}";
            }
        }
        daysRemainedText.text = $"Осталось {game.daysRemained} дней";
        for (int i = 0; i < 2; i++)
        {
            aimSliders[i].value = Mathf.Min(game.gold, game.currentAims[i]());
            aimTexts[i].text = $"{aimSliders[i].value}/{game.currentAims[i]()}";
        }
        if (game.daysLowAttention > 0)
        {
            longAttentionDebuff.SetActive(true);
            longAttentionDebuffLevel.text = $"{game.lowAttentionLevel * 100f}%";
            longAttentionDebuffDays.text = $"{game.daysLowAttention}";
        }
        else
        {
            longAttentionDebuff.SetActive(false);
        }
        if (game.daysHighGold > 0)
        {
            longGoldBuff.SetActive(true);
            longGoldBuffLevel.text = $"{game.highGoldLevel * 100f}%";
            longGoldBuffDays.text = $"{game.daysHighGold}";
        }
        else
        {
            longGoldBuff.SetActive(false);
        }
    }

}
