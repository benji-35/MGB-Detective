using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class itemData : MonoBehaviour
{
    [SerializeField]
    Button btn;
    private item itm;
    private itemFound found;
    private int index;

    public void SHOW_ITEM()
    {
        found.display_item(itm, index);
    }

    public void setItem(item n_itm, itemFound f, int index)
    {
        this.index = index;
        Text txt = btn.GetComponentInChildren<Text>();
        txt.text = n_itm.name;
        itm = n_itm;
        found = f;
    }
}
