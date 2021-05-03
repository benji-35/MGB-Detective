using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadBetweenScenes : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(loadScene(PlayerPrefs.GetInt("loadScene")));
    }

    IEnumerator loadScene(int index)
    {
        AsyncOperation ope = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!ope.isDone)
            yield return null;
    }
}
