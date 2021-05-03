using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YWTMessage", menuName = "Computer/yesWetalk/Create message")]
public class whatsappMessage : ScriptableObject
{
    public computerDate dateMessage;
    public whatsappAccount account;
    public string message;
}
