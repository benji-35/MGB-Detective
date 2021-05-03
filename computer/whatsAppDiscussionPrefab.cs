using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class whatsAppDiscussionPrefab : MonoBehaviour
{
    [SerializeField]
    Text discussionName;
    [SerializeField]
    Image ppDiscussion;

    int id;
    private computer compute;

    public void set_values(whatsappDiscussion discu, computer compute, int id)
    {
        this.compute = compute;
        this.id = id;
        discussionName.text = discu.nameDiscussion;
        ppDiscussion.preserveAspect = true;
        ppDiscussion.sprite = discu.discuImage;
    }

    public void BTN_SHOW_DISCU_PREFAB()
    {
        compute.open_discussion_whatsapp(id);
    }

    public int getId()
    {
        return (id);
    }
}
