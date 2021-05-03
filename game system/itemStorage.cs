using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemStorage : MonoBehaviour
{
    [SerializeField]
    Button btn;
    private storageItem itm;
    private stockItem found;
    private int index;

    public void SHOW_ITEM_STORAGE()
    {
        found.displayStockedItem(itm, index);
    }

    public void setItem(storageItem n_itm, stockItem f, int index)
    {
        this.index = index;
        Text txt = btn.GetComponentInChildren<Text>();
        txt.text = n_itm.nameInStorage;
        itm = n_itm;
        found = f;
    }

    public void setId(int id)
    {
        this.index = id;
    }
}
