using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "command", menuName = "Computer/terminal/Create command")]
public class cmdTerminal : ScriptableObject
{
    public string cmd;
    public string description;
    public cmdType type;
}

public enum cmdType
{
    cd,
    cat,
    ls
}