using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class inputSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    List<keyChange> options = new List<keyChange>();
    [SerializeField]
    GameObject pannelWaitTouch;
    [Header("Keys")]
    [SerializeField]
    KeyCode forward_key = KeyCode.Z;
    [SerializeField]
    KeyCode backward_key = KeyCode.S;
    [SerializeField]
    KeyCode left_key = KeyCode.Q;
    [SerializeField]
    KeyCode right_key = KeyCode.D;
    [SerializeField]
    KeyCode interraction_key = KeyCode.E;
    [SerializeField]
    KeyCode tablette_key = KeyCode.I;
    [SerializeField]
    KeyCode space_key = KeyCode.Space;
    [SerializeField]
    KeyCode echap_key = KeyCode.Escape;

    private bool changeKey;
    private string keyNameChange;

    void Start()
    {
        pannelWaitTouch.SetActive(false);
        init_save_values();
        update_name_button();
        checkSame();
    }

    void Update()
    {
        if (changeKey)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    PlayerPrefs.SetInt(keyNameChange, (int)kcode);
                    pannelWaitTouch.SetActive(false);
                    changeKey = false;
                    update_name_button();
                    checkSame();
                    break;
                }
            }
        }
    }

    void init_save_values()
    {
        if (PlayerPrefs.HasKey("forward"))
        {
            forward_key = (KeyCode)PlayerPrefs.GetInt("forward");
        } else
        {
            PlayerPrefs.SetInt("forward", (int)forward_key);
        }
        if (PlayerPrefs.HasKey("backward"))
        {
            backward_key = (KeyCode)PlayerPrefs.GetInt("backward");
        }
        else
        {
            PlayerPrefs.SetInt("backward", (int)backward_key);
        }
        if (PlayerPrefs.HasKey("left"))
        {
            left_key = (KeyCode)PlayerPrefs.GetInt("left");
        } else
        {
            PlayerPrefs.SetInt("left", (int)left_key);
        }
        if (PlayerPrefs.HasKey("right"))
        {
            right_key = (KeyCode)PlayerPrefs.GetInt("right");
        } else
        {
            PlayerPrefs.SetInt("right", (int)right_key);
        }
        if (PlayerPrefs.HasKey("interract"))
        {
            interraction_key = (KeyCode)PlayerPrefs.GetInt("interract");
        } else
        {
            PlayerPrefs.SetInt("interract", (int)interraction_key);
        }
        if (PlayerPrefs.HasKey("tablette"))
        {
            tablette_key = (KeyCode)PlayerPrefs.GetInt("tablette");
        } else
        {
            PlayerPrefs.SetInt("tablette", (int)tablette_key);
        }
        if (PlayerPrefs.HasKey("space"))
        {
            tablette_key = (KeyCode)PlayerPrefs.GetInt("space");
        }
        else
        {
            PlayerPrefs.SetInt("space", (int)space_key);
        }
        if (PlayerPrefs.HasKey("echap"))
        {
            tablette_key = (KeyCode)PlayerPrefs.GetInt("echap");
        }
        else
        {
            PlayerPrefs.SetInt("echap", (int)echap_key);
        }
    }

    private void update_name_button()
    {
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].btn != null) {
                GameObject btn_obj = options[i].btn.gameObject;
                Text[] text = btn_obj.GetComponentsInChildren<Text>();
                KeyCode key = (KeyCode)PlayerPrefs.GetInt(options[i].key);
                options[i].idKey = (int)key;
                if (text.Length >= 1)
                    text[0].text = key.ToString();
            }
        }
    }

    public void checkSame()
    {
        for (int i = 0; i < options.Count; i++)
            options[i].doublon = false;
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].btn != null)
            {
                for (int x = 0; x < options.Count; x++)
                {
                    if (options[x].btn != null)
                    {
                        if (x != i)
                        {
                            if (options[i].idKey == options[x].idKey)
                            {
                                options[i].doublon = true;
                                options[x].doublon = true;
                            }
                        }
                    }
                }
            }
        }
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].btn != null) {
                if (options[i].doublon)
                {
                    options[i].txt.color = Color.red;
                } else
                {
                    options[i].txt.color = Color.white;
                }
            }
        }
    }

    public void change_button(int id)
    {
        keyNameChange = options[id].key;
        changeKey = true;
        pannelWaitTouch.SetActive(true);
    }
}

[Serializable]
public class keyChange
{
    [HideInInspector]
    public int id;
    public Text txt;
    public Button btn;
    public string key;
    [HideInInspector]
    public int idKey;
    [HideInInspector]
    public bool doublon;
}