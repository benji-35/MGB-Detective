using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class suspectPrefabScript : MonoBehaviour
{
    [SerializeField]
    Image img;
    suspectComputer suspect;
    police_app_ui ui;
    computer compute;
    int id;

    public void init_values(suspectComputer suspect, police_app_ui ui, computer compute, int id)
    {
        this.id = id;
        this.compute = compute;
        this.ui = ui;
        this.suspect = suspect;
        img.sprite = suspect.pp;
        img.preserveAspect = true;
    }

    public void BTN_SUSPECT()
    {
        ui.show_suspect(id, suspect, compute);
    }

    public suspectComputer getSuspectCard()
    {
        return (suspect);
    }
}
