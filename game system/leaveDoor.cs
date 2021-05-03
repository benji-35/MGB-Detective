using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaveDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>().openTpMenu();
        }
    }
}

[System.Serializable]
public class tp_class
{
    public string name;
    public Transform pos;
    [HideInInspector]
    public int id;
}