using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prefabAppScript : MonoBehaviour
{
    [SerializeField]
    private Image logo;
    [SerializeField]
    private Text nameApp;
    private appType app;
    private computer comput;

    public void setValues(Sprite sprt, appType type, computer comp, string name)
    {
        logo.sprite = sprt;
        app = type;
        comput = comp;
        nameApp.text = name;
    }

    public void LOAD_APP()
    {
        comput.open_app(app);
    }
}

public enum appType
{
    twitter,
    whatsapp,
    policeapp
}