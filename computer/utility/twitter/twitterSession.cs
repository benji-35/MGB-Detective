using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "twitterSession", menuName = "Computer/twitter/Create session")]
public class twitterSession : ScriptableObject
{
    public string nameApp;
    public Sprite logoTwitter;
    public twitterAccount accoutnSession;
    public twitterActuality actuality;
}
