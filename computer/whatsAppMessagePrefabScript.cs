using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class whatsAppMessagePrefabScript : MonoBehaviour
{
    [Header("Other Part")]
    [SerializeField]
    Text message_other;
    [SerializeField]
    Text sender_other;
    [SerializeField]
    Image pp_other;
    [SerializeField]
    Text date_other;
    [Header("Own Part")]
    [SerializeField]
    Text message_me;
    [SerializeField]
    Text sender_me;
    [SerializeField]
    Image pp_me;
    [SerializeField]
    Text date_me;
    [Header("UI Panels")]
    [SerializeField]
    GameObject me;
    [SerializeField]
    GameObject other;

    private whatsappAccount whoPost;
    private computer compute;
    public void set_values(whatsappMessage msg, whatsappSession wsession, computer compute)
    {
        this.compute = compute;
        whoPost = msg.account;
        if (msg.account == wsession.ownAccount)
        {
            message_me.text = msg.message;
            pp_me.preserveAspect = true;
            pp_me.sprite = msg.account.pp;
            computerDate date = msg.dateMessage;
            date_me.text = date.day + "/" + date.month + "/" + date.year + " " + date.hours + ":" + date.minutes;
            me.SetActive(true);
            other.SetActive(false);
            sender_me.text = "Me";
        } else
        {
            sender_other.text = msg.account.pseudo;
            message_other.text = msg.message;
            pp_other.preserveAspect = true;
            pp_other.sprite = msg.account.pp;
            computerDate date = msg.dateMessage;
            date_other.text = date.day + "/" + date.month + "/" + date.year + " " + date.hours + ":" + date.minutes;
            me.SetActive(false);
            other.SetActive(true);
        }
    }

    public void BTN_SHOW_ACCOUNT_MSG()
    {
        compute.open_account_whatsapp(whoPost);
    }
}
