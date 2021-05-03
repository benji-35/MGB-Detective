using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "twitterAccount", menuName = "Computer/twitter/Create account")]
public class twitterAccount : ScriptableObject
{
    public string pseudo;
    public string descriptionAccount;
    public Sprite pp;
    public Sprite banner;
    public List<twitterPost> accountPosts = new List<twitterPost>();
}
