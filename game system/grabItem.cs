using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabItem : MonoBehaviour
{
    [SerializeField]
    private item itm;
    [SerializeField]
    string messageNotif;
    [SerializeField]
    notifState stateOfNotif;
    private bool canGrap = false;
    private KeyCode interract;
    itemFound found;
    // Start is called before the first frame update
    void Start()
    {
        interract = (KeyCode)PlayerPrefs.GetInt("interract");
    }

    // Update is called once per frame
    void Update()
    {
        if (canGrap && Input.GetKeyDown(interract))
        {
            found.addItem(itm);
            notifications notifs = found.gameObject.GetComponent<notifications>();
            notifs.newNotif(messageNotif, stateOfNotif);
            PlayerStat stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
            stat.hideInterract();
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canGrap = true;
            string msg = "Grab item with \"" + interract.ToString() + "\"";
            PlayerStat stat = other.GetComponent<PlayerStat>();
            found = other.GetComponent<itemFound>();
            if (stat != null)
            {
                stat.setInterractText(msg);
                stat.showInterract();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canGrap = false;
            PlayerStat stat = other.GetComponent<PlayerStat>();
            if (stat != null)
            {
                stat.hideInterract();
            }
        }
    }
}
