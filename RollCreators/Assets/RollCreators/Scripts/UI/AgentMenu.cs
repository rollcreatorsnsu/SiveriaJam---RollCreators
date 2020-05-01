using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AgentMenu : MonoBehaviour
{
    public Agent currentAgent;
    public GameObject dayMenu;
    public GameObject nightMenu;
    public Game game;
    public Dropdown dropdown;
    public Button dropDownButton;

    public void Show()
    {
        if (game.dayTime == Game.DayTime.DAY)
        {
            dayMenu.SetActive(true);
        }
        else
        {
            nightMenu.SetActive(true);
        }
    }

    private void GetSocialStatusListener()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.tempSocialStatus = (Sinner.SocialStatus) System.Enum.Parse(typeof(Sinner.SocialStatus),
            dropdown.options[dropdown.value].text);
        dropdown.gameObject.SetActive(false);
    }

    private void GetIntListener()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.tempInt = dropdown.value + 1;
        dropdown.gameObject.SetActive(false);
    }

    private void GetNightSocialStatusListener()
    {
        NightAgent agent = (NightAgent)currentAgent;
        agent.tempSocialStatus = (Sinner.SocialStatus) System.Enum.Parse(typeof(Sinner.SocialStatus),
            dropdown.options[dropdown.value].text);
        dropdown.gameObject.SetActive(false);
    }

    private void GetSinnerListener()
    {
        NightAgent agent = (NightAgent)currentAgent;
        foreach (Sinner sinner in game.sinners)
        {
            if (sinner.name == dropdown.options[dropdown.value].ToString())
            {
                agent.tempSinner = sinner;
                dropdown.gameObject.SetActive(false);
            }
        }
    }

    private void ShowSocialStatusDropdown()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string>(Enum.GetNames(typeof(Sinner.SocialStatus))));
        dropDownButton.onClick.RemoveAllListeners();
        if (game.dayTime == Game.DayTime.DAY)
        {
            dropDownButton.onClick.AddListener(GetSocialStatusListener);
        }
        else
        {
            dropDownButton.onClick.AddListener(GetNightSocialStatusListener);
        }
        dropdown.gameObject.SetActive(true);
    }

    private void ShowIntDropdown()
    {
        dropdown.ClearOptions();
        List<string> newOptions = new List<string>();
        for (int i = 1; i <= 10; i++)
        {
            newOptions.Add(i.ToString());
        }
        dropdown.AddOptions(newOptions);
        dropDownButton.onClick.RemoveAllListeners();
        dropDownButton.onClick.AddListener(GetIntListener);
        dropdown.gameObject.SetActive(true);
    }

    private void ShowSinnersDropDown()
    {
        dropdown.ClearOptions();
        List<string> sinners = new List<string>();
        foreach (Sinner sinner in game.sinners)
        {
            sinners.Add(sinner.name);
        }
        dropdown.AddOptions(sinners);
        dropDownButton.onClick.RemoveAllListeners();
        dropDownButton.onClick.AddListener(GetSinnerListener);
        dropdown.gameObject.SetActive(true);
    }

    public void ConductAService()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.task = DayAgent.DayTask.CONDUCT_A_SERVICE;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }

    public void GiveAlms()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.task = DayAgent.DayTask.GIVE_ALMS;
        gameObject.SetActive(false);
        ShowIntDropdown();
    }
    
    public void ConfessSinners()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.task = DayAgent.DayTask.CONFESS_SINNERS;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }
    
    public void InterpretingSacred()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.task = DayAgent.DayTask.INTERPRETING_SACRED_TEXTS;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }

    public void ListenToGossip()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.task = DayAgent.DayTask.LISTEN_TO_GOSSIP;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }
    
    public void PreachInTheCity()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.task = DayAgent.DayTask.PREACH_IN_THE_CITY;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }
    
    public void SellIndulgence()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.task = DayAgent.DayTask.SELL_INDULGENCE;
        gameObject.SetActive(false);
    }
    
    public void ChangeAgent()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.task = DayAgent.DayTask.CHANGE_AGENT;
        gameObject.SetActive(false);
    }

    public void TrainAgent()
    {
        DayAgent agent = (DayAgent)currentAgent;
        agent.task = DayAgent.DayTask.TRAIN_AGENT;
        gameObject.SetActive(false);
    }

    public void OpenFlat()
    {
        NightAgent agent = (NightAgent) currentAgent;
        agent.task = NightAgent.NightTask.OPEN_FLAT;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }
    
    public void MuchlyPraise()
    {
        NightAgent agent = (NightAgent) currentAgent;
        agent.task = NightAgent.NightTask.MUCHLY_PRAISE;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }
    
    public void ProvokeToFight()
    {
        NightAgent agent = (NightAgent) currentAgent;
        agent.task = NightAgent.NightTask.PROVOKE_TO_FIGHT;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }
    
    public void ComplaintOnJustice()
    {
        NightAgent agent = (NightAgent) currentAgent;
        agent.task = NightAgent.NightTask.COMPLAINT_ON_JUSTICE;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }

    public void Dice()
    {
        NightAgent agent = (NightAgent) currentAgent;
        agent.task = NightAgent.NightTask.DICE;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }
    
    public void TakeABreak()
    {
        NightAgent agent = (NightAgent) currentAgent;
        agent.task = NightAgent.NightTask.TAKE_A_BREAK;
        gameObject.SetActive(false);
        ShowSocialStatusDropdown();
    }
    
    public void Develop()
    {
        NightAgent agent = (NightAgent) currentAgent;
        agent.task = NightAgent.NightTask.DEVELOP;
        gameObject.SetActive(false);
        ShowSinnersDropDown();
    }
    
    public void ChangeNightAgent()
    {
        NightAgent agent = (NightAgent) currentAgent;
        agent.task = NightAgent.NightTask.CHANGE_AGENT;
        gameObject.SetActive(false);
    }
    
}
