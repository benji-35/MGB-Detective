using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cinematicLoadLevel : MonoBehaviour
{
    [SerializeField]
    int idMenu;
    AsyncOperation op;

    public void Start()
    {
        StartCoroutine(load());
    }

    public void BTN_CINEMATIC_LOADSCENE()
    {
        op.allowSceneActivation = true;
    }

    IEnumerator load()
    {
        op = SceneManager.LoadSceneAsync(idMenu);
        op.allowSceneActivation = false;
        while (!op.isDone)
            yield return null;
    }
}
