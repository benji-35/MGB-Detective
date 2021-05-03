using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "twitterTendance", menuName = "Computer/twitter/Create tendance")]
public class twitterTendance : ScriptableObject
{
    public string hashtag;
    public string description;
    [Range(0, 99999999)]
    public int nbTweets = 0;
}
