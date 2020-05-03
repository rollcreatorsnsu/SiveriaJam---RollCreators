using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationMenu : MonoBehaviour
{

    public Text text;
    public delegate void Callback();

    private Callback callback;


    public void Show(string text, Callback callback)
    {
        gameObject.SetActive(true);
        this.text.text = text;
        this.callback = callback;
    }

    public void Yes()
    {
        callback();
        gameObject.SetActive(false);
    }

    public void No()
    {
        gameObject.SetActive(false);
    }
}
