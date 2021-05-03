using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    GameObject interractMenu;
    [SerializeField]
    Text interractText;
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject dataMenu;
    [SerializeField]
    int idLoadBetweenScene;
    [SerializeField]
    int looseScene;
    [SerializeField]
    int winScene;
    [SerializeField]
    Transform posEndLevel;
    [Header("TP MENU")]
    [SerializeField]
    GameObject tpMenu;
    [SerializeField]
    tp_class[] tps;
    [SerializeField]
    GameObject loadingPanel;

    KeyCode escape;
    [HideInInspector]
    public bool storageOpen;
    private Player_Movement mov;
    private AsyncOperation opLoose;
    private AsyncOperation opWin;
    [HideInInspector]
    public bool statusMorgue;
    void Start()
    {
        loadingPanel.SetActive(false);
        mov = GetComponent<Player_Movement>();
        tpMenu.SetActive(false);
        escape = (KeyCode)PlayerPrefs.GetInt("echap");
        interractMenu.SetActive(false);
        BackInGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(escape) && !dataMenu.activeSelf && !storageOpen && !statusMorgue)
        {
            if (pauseMenu.activeSelf)
            {
                BackInGame();
            } else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mov.enabled = false;
            }
        }
    }

    public void setInterractText(string message)
    {
        interractText.text = message;
    }

    public void showInterract()
    {
        interractMenu.SetActive(true);
    }

    public void hideInterract()
    {
        interractMenu.SetActive(false);
    }

    public void openData()
    {
        if (pauseMenu.activeSelf)
            return;
        dataMenu.SetActive(!dataMenu.activeSelf);
        if (dataMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mov.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mov.enabled = true;
        }
    }

    public void BackInGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mov.enabled = true;
    }

    public void BackToMenu()
    {
        PlayerPrefs.SetInt("loadScene", 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(idLoadBetweenScene);
    }

    public void openTpMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        tpMenu.SetActive(true);
        mov.enabled = false;
    }

    public void TP_PLAYER(int index)
    {
        if (index < 0 || index >= tps.Length)
            return;
        Debug.Log("TP to " + tps[index].name);
        gameObject.transform.position = tps[index].pos.position;
        gameObject.transform.rotation = tps[index].pos.rotation;
        tpMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mov.enabled = true;
    }

    public void loadLooseScene()
    {
        StartCoroutine(looseSceneLoad());
    }

    public void loadWinScene()
    {
        StartCoroutine(winSceneLoad());
    }

    private IEnumerator looseSceneLoad()
    {
        loadingPanel.SetActive(true);
        opLoose = SceneManager.LoadSceneAsync(looseScene);
        while (!opLoose.isDone)
            yield return null;
    }

    private IEnumerator winSceneLoad()
    {
        loadingPanel.SetActive(true);
        opWin = SceneManager.LoadSceneAsync(winScene);
        while (!opWin.isDone)
            yield return null;
    }

    public void teleportEndLevel()
    {
        gameObject.transform.position = posEndLevel.position;
        gameObject.transform.rotation = posEndLevel.rotation;
    }
}
