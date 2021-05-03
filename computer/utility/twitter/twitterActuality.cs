using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "twitterActu", menuName = "Computer/twitter/Create actuality")]
public class twitterActuality : ScriptableObject
{
    public List<twitterPost> actuPost = new List<twitterPost>();
    public List<twitterTendance> tendances = new List<twitterTendance>();
}
