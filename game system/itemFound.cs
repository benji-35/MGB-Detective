using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemFound : MonoBehaviour
{
    [SerializeField]
    List<item> itemfound = new List<item>();
    [SerializeField]
    GameObject prefabSelectItem = null;
    [SerializeField]
    GameObject parentSelectItem = null;
    [SerializeField]
    List<computer> policeSessions = new List<computer>();
    [Header("UI")]
    [SerializeField]
    Text name_ui;
    [SerializeField]
    Text description_ui;
    [SerializeField]
    Button analyze_btn;
    [SerializeField]
    Button updateSuspectBtn;

    private int currDisplay = -1;
    void Start()
    {
        
    }

    void Update()
    {
        foreach (item it in itemfound)
        {
            if (it.inAnalysing && it.analyseTime > 0f)
                it.analyseTime -= Time.deltaTime;
        }
        if (currDisplay != -1)
        {
            description_ui.text = getDescription_itm(currDisplay);
            if (itemfound[currDisplay].canAnalyse && !itemfound[currDisplay].inAnalysing)
            {
                analyze_btn.interactable = true;
                updateSuspectBtn.interactable = false;
            }
            else
            {
                item it = itemfound[currDisplay];
                if (it.newSuspects.Count > 0 && !it.updateSuspectDone && it.analyseTime <= 0)
                {
                    updateSuspectBtn.interactable = true;
                } else
                {
                    updateSuspectBtn.interactable = false;
                }
                analyze_btn.interactable = false;
            }
        }
    }

    public void openItemInfos(int id)
    {

    }

    public void addItem(item it)
    {
        itemfound.Add(it);
        it.maxTime = it.analyseTime;
        GameObject selectCurrItem = Instantiate(prefabSelectItem, parentSelectItem.transform);
        itemData dataItem = selectCurrItem.GetComponent<itemData>();
        dataItem.setItem(it, this, itemfound.Count - 1);
        Button btn = selectCurrItem.GetComponent<Button>();
        btn.onClick.AddListener(() => openItemInfos(itemfound.Count - 1));
        if (!it.canAnalyse)
            add_suspect_in_listing(it);
    }

    private void add_suspect_in_listing(item it)
    {
        for (int i = 0; i < policeSessions.Count; i++)
        {
            foreach (suspectComputer susp in it.newSuspects)
                policeSessions[i].add_suspect_in_list(susp);
        }
    }

    public void display_item(item itm, int id)
    {
        currDisplay = id;
        if (itm.canAnalyse && !itemfound[id].inAnalysing)
        {
            analyze_btn.interactable = true;
            Text txt = analyze_btn.gameObject.GetComponentInChildren<Text>();
            txt.text = "Analyse (" + itemfound[id].analyseTime + "s)";
        } else
        {
            Text txt = analyze_btn.gameObject.GetComponentInChildren<Text>();
            txt.text = "Analyse";
            analyze_btn.interactable = false;
        }
        name_ui.text = itm.name;
        string description_itm = getDescription_itm(id);
        description_ui.text = description_itm;
    }

    private string getDescription_itm(int id)
    {
        item itm = itemfound[id];
        string res = "Description: \n" + itm.description + "\n\n";
        if (itemfound[id].canAnalyse && itemfound[id].analyseTime <= 0f)
            res += "Analyse: \n" + itm.summaryAnalyse + "\n";
        return (res);
    }

    public void ANALYZE()
    {
        analyze_btn.interactable = false;
        itemfound[currDisplay].inAnalysing = true;
        Text txt = analyze_btn.gameObject.GetComponentInChildren<Text>();
        txt.text = "Analyse";
    }

    public void updateSuspect()
    {
        item it = itemfound[currDisplay];
        it.updateSuspectDone = true;
        for (int i = 0; i < policeSessions.Count; i++)
        {
            foreach (suspectComputer susp in it.newSuspects)
            {
                policeSessions[i].add_suspect_in_list(susp);
                
            }
        }
    }
}

[System.Serializable]
public class item
{
    public string name;
    public string description;
    public bool canAnalyse;
    [HideInInspector]
    public bool inAnalysing;
    [HideInInspector]
    public float maxTime;
    public string summaryAnalyse;
    public float analyseTime;
    public List<suspectComputer> newSuspects = new List<suspectComputer>();
    public bool updateSuspectDone = false;
}