using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YWTDiscussion", menuName = "Computer/yesWetalk/Create discussion")]
public class whatsappDiscussion : ScriptableObject
{
    public string nameDiscussion;
    public Sprite discuImage;
    public List<whatsappMessage> messages = new List<whatsappMessage>();
}
