using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "twitterPost", menuName = "Computer/twitter/Create post")]
public class twitterPost : ScriptableObject
{
    public twitterAccount accountPost;
    public computerDate date;
    public string message;
    public twitterPost repost;
}
