using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SessionPolice", menuName = "Computer/police/Create session")]
public class policeApp : ScriptableObject
{
    public string nameApp;
    public Sprite logo;
    public string firstname;
    [Range(20, 99)]
    public int age = 20;
    public string lastname;
    public Sprite pp;
    public string grade;
    public string description;
    public List<suspectComputer> suspectsCard = new List<suspectComputer>();
}