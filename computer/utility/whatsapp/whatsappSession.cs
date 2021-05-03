using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YWTSession", menuName = "Computer/yesWetalk/Create session")]
public class whatsappSession : ScriptableObject
{
    public string nameApp;
    public Sprite logo;
    public whatsappAccount ownAccount;
    public List<whatsappDiscussion> discussions = new List<whatsappDiscussion>();
}
