using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "suspectCard", menuName = "Computer/police/Create suspect card")]
public class suspectComputer : ScriptableObject
{
    public string firstname;
    public string lastname;
    public Sprite pp;
    [Range(-1, 99)]
    public int age;
    public string description;
    public bool isMurder;
}
