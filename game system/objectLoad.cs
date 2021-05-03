using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectLoad : MonoBehaviour
{
    [SerializeField]
    cinematicLoadLevel cineLoad;

    private void OnEnable()
    {
        cineLoad.BTN_CINEMATIC_LOADSCENE();
    }
}
