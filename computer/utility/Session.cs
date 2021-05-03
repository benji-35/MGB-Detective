using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Session", menuName = "Computer/Create session")]
public class Session : ScriptableObject
{
    public bool logged;
    public loginSession login;
    public twitterSession twitter;
    public policeApp police;
    public whatsappSession whatsapp;
}
