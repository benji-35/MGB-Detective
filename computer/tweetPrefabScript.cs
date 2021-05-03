using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tweetPrefabScript : MonoBehaviour
{
    [SerializeField]
    Image pp;
    [SerializeField]
    Text message;
    [SerializeField]
    Text date;
    [SerializeField]
    Text pseudo;
    [SerializeField]
    Transform repost;

    private computer compute;
    private twitter_ui ui;
    private twitterAccount accoutnSend;

    public void set_values(twitterPost post, computer compute, GameObject prefabPost, bool showrepost, twitter_ui ui)
    {
        this.ui = ui;
        this.compute = compute;
        accoutnSend = post.accountPost;
        pp.preserveAspect = true;
        pp.sprite = post.accountPost.pp;
        message.text = post.message;
        date.text = post.date.day + "/" + post.date.month + "/" + post.date.year;
        pseudo.text = post.accountPost.pseudo;
        if (post.repost != null && showrepost)
        {
            GameObject obj = compute.generateApp(prefabPost, repost);
            tweetPrefabScript script = obj.GetComponent<tweetPrefabScript>();
            script.set_values(post.repost, compute, prefabPost, false, ui);
        }
    }

    public void BTN_SHOW_ACCOUNT()
    {
        if (compute == null)
        {
            Debug.LogError("[tweetPrefabScript]COMPUTE not set");
            return;
        }
        if (accoutnSend == null)
        {
            Debug.LogError("[tweetPrefabScript]account not set");
            return;
        }
        ui.showAccount(accoutnSend, compute);
    }
}
