using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    [SerializeField]
    GameObject[] menus;
    [SerializeField]
    preloadScene[] scenesToLoad;

    [Header("Loading Menu")]
    [SerializeField]
    GameObject loadingScene;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < menus.Length; i++)
            menus[i].SetActive(false);
        menus[0].SetActive(true);
        loadingScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QUIT()
    {
        Application.Quit();
    }

    public void startScene(int index)
    {
        for (int i = 0; i < scenesToLoad.Length; i++)
        {
            if (scenesToLoad[i].index == index)
            {
                Debug.Log("scene found");
                loadingScene.SetActive(true);
                StartCoroutine(scenesToLoad[i].loadScene());
                break;
            }
        }
    }
}

[System.Serializable]
public class preloadScene
{
    public int index;
    [HideInInspector]
    public AsyncOperation operation;

    public IEnumerator loadScene()
    {
        this.operation = SceneManager.LoadSceneAsync(this.index);
        
        while (!operation.isDone)
            yield return null;
    }
}