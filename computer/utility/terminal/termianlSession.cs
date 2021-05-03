using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TermSession", menuName = "Computer/terminal/Create session")]
public class termianlSession : ScriptableObject
{
    public List<cmdTerminal> commands = new List<cmdTerminal>();
    public directory mainDirectory;
}
