using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "computerDate", menuName = "Computer/Create computer date")]
public class computerDate : ScriptableObject
{
    [Range(1, 31)]
    public int day = 1;
    [Range(1, 12)]
    public int month = 1;
    [Range(-2020, 3512)]
    public int year = 2021;
    [Range(0, 59)]
    public int minutes = 0;
    [Range(0, 23)]
    public int hours = 0;
    [Range(0, 59)]
    public int seconds = 0;
}
