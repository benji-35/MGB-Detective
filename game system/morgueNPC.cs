using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class morgueNPC : MonoBehaviour
{
    [Header("ID")]
    [SerializeField]
    suspectComputer victimeID;
    [Header("UI")]
    [SerializeField]
    GameObject menu;
    [SerializeField]
    Text firstName;
    [SerializeField]
    Text lastName;
    [SerializeField]
    Text age;
    [SerializeField]
    Text description;
    [SerializeField]
    Image pp;

    private KeyCode interract;
    private bool canInterract = false;

    private void Start()
    {
        menu.SetActive(false);
        interract = (KeyCode)PlayerPrefs.GetInt("interract");
        firstName.text = victimeID.firstname;
        lastName.text = victimeID.lastname;
        age.text = victimeID.age + " ans";
        if (victimeID.age == -1)
            age.text = "Unknown";
        description.text = victimeID.description;
        pp.preserveAspect = true;
        pp.sprite = victimeID.pp;
    }

    private void Update()
    {
        if (canInterract && Input.GetKeyDown(interract))
        {
            menu.SetActive(true);
            canInterract = false;
            PlayerStat stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
            stat.hideInterract();
            if (menu.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void leaveMorgueMenu()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menu.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStat stat = other.GetComponent<PlayerStat>();
            stat.setInterractText("Interract with coroner with \"" + interract.ToString() + "\"");
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
