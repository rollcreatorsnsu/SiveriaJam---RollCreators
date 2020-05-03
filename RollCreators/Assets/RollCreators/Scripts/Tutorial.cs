using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private static string[] texts =
    {
        "Цель - добейтесь, чтобы выиграть",
        "Таймер - оставшееся количество дней, за которое нужно выполнить цель",
        "Золото - мерило вашего успеха",
        "Внимание инквизиции - если достигнет 100% вы проиграете",
        "Грешники - здесь содержится вся известная вам информация о ваших прихожанах",
        "Агенты - здесь вы можете увидеть всю информацию о ваших агентах, а также менять и прокачивать их",
        "Вкладки выдачи заданий - выберете одно из доступных заданий для вашего агента",
        "Кнопка следующего цикла - каждый игровой день состоит из 2 фаз, каждая из которых отличается агентами и доступными им действиями. Учтите, что все сведения собранные о грешниках становятся неактуальны в начале следующего дня"
    };
    private int NUMBER_OF_STEPS = 8;
    
    public List<GameObject> gameObjects;
    public Text text;

    private int currentStep = 0;

    public void _Start()
    {
        gameObject.SetActive(true);
        currentStep = 0;
        NextStep();
    }
    
    public void NextStep()
    {
        if (currentStep > 0)
        {
            gameObjects[currentStep - 1].SetActive(false);
        }
        if (currentStep == NUMBER_OF_STEPS)
        {
            gameObject.SetActive(false);
        }
        else
        {
            text.text = texts[currentStep];
            gameObjects[currentStep].SetActive(true);
        }
        currentStep++;
    }
}
