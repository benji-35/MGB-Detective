using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stockItem : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    Transform parent;
    [SerializeField]
    string nameStorage;
    [SerializeField]
    List<storageItem> items = new List<storageItem>();
    [Header("UI")]
    [SerializeField]
    Button btnAddDataBase;
    [SerializeField]
    GameObject menu;
    [SerializeField]
    Text leaveMsg;
    [SerializeField]
    Text titleStorage;
    [SerializeField]
    Text titleItem;
    [SerializeField]
    Text descriptionItem;

    private PlayerStat stat;
    private List<GameObject> allObjets = new List<GameObject>();
    private KeyCode interract;
    [SerializeField]
    private bool canInterract;
    private int currItem = -1;
    void Start()
    {
        menu.SetActive(false);
        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        btnAddDataBase.interactable = false;
        GetComponent<MeshRenderer>().enabled = false;
        interract = (KeyCode)PlayerPrefs.GetInt("interract");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interract) && canInterract)
        {
            leaveMsg.text = getLeaveMessage();
            init_menu();
            menu.SetActive(true);
            btnAddDataBase.interactable = true;
            canInterract = false;
            stat.storageOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            stat.GetComponent<Player_Movement>().enabled = false;
            if (stat != null)
            {
                stat.hideInterract();
            }
        } else if (Input.GetKeyDown(interract) && menu.activeSelf)
        {
            close_storage();
            stat.GetComponent<Player_Movement>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canInterract = true;
            string msg = "Open " + nameStorage + " item with \"" + interract.ToString() + "\"";
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
            canInterract = false;
            if (stat != null)
            {
                stat.hideInterract();
            }
        }
    }

    private void init_menu()
    {
        for (int i = 0; i < items.Count; i++)
        {
            GameObject dup = Instantiate(prefab, parent);
            itemStorage itm_stor = dup.GetComponent<itemStorage>();
            allObjets.Add(dup);
            itm_stor.setItem(items[i], this, i);
        }
    }

    private void close_storage()
    {
        foreach (GameObject gobj in allObjets)
            Destroy(gobj);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        allObjets.Clear();
        menu.SetActive(false);
        stat.storageOpen = false;
    }

    public void displayStockedItem(storageItem __itm, int id)
    {
        storageItem itm = items[id];
        currItem = id;
        descriptionItem.text = itm.descritionInStorgae;
        btnAddDataBase.interactable = true;
        titleItem.text = itm.nameInStorage;
    }

    public void addInDataBase()
    {
        if (currItem == -1)
            return;
        item itm = items[currItem].itm;
        itemFound found = GameObject.FindGameObjectWithTag("Player").GetComponent<itemFound>();
        found.addItem(itm);
        items.Remove(items[currItem]);
        Destroy(allObjets[currItem]);
        allObjets.Remove(allObjets[currItem]);
        for (int i = 0; i < allObjets.Count; i++)
        {
            itemStorage __stor = allObjets[i].GetComponent<itemStorage>();
            __stor.setId(i);
        }
        currItem = -1;
        titleItem.text = "No item selected";
        descriptionItem.text = "";
        btnAddDataBase.interactable = false;
    }

    private string getLeaveMessage()
    {
        string res = "To leave " + nameStorage + " push \"" + interract.ToString() + "\"";
        return (res);
    }
}

[System.Serializable]
public class storageItem
{
    public string nameInStorage;
    public string descritionInStorgae;
    public item itm;
}