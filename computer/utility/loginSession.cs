using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "loginSession", menuName = "Computer/Create login session")]
public class loginSession : ScriptableObject
{
    public string pseudo;
    public Sprite pp;
    public string password;
}