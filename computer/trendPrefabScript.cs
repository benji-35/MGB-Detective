using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trendPrefabScript : MonoBehaviour
{
    [SerializeField]
    Text hashtag;
    [SerializeField]
    Text description;
    [SerializeField]
    Text nbTweets;

    public void setValues(twitterTendance trend)
    {
        hashtag.text = trend.hashtag;
        description.text = trend.description;
        nbTweets.text = trend.nbTweets + " repost";
        if (trend.nbTweets == 0)
            nbTweets.text = "no repost";
    }
}
