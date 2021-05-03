using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scientistScript : MonoBehaviour
{
    private KeyCode interract;
    private bool canInterract = false;

    private void Start()
    {
        interract = (KeyCode)PlayerPrefs.GetInt("interract");
    }

    private void Update()
    {
        if (canInterract && Input.GetKeyDown(interract))
        {
            PlayerStat stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
            stat.openData();
            stat.hideInterract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStat stat = other.GetComponent<PlayerStat>();
            stat.setInterractText("Interract with scientist with \"" + interract.ToString() + "\"");
            stat.showInterract();
            canInterract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canInterract && other.tag == "Player")
        {
            PlayerStat stat = other.GetComponent<PlayerStat>();
            stat.hideInterract();
            canInterract = false;
        }
    }
}
