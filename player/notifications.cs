using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class notifications : MonoBehaviour
{
    [Header("Notifs")]
    [SerializeField]
    List<notif> notifs = new List<notif>();
    [Header("Options")]
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    Transform parent;
    [SerializeField]
    [Range(0.5f, 10f)]
    float timeNotif = 1.5f;

    void Start()
    {

    }

    void Update()
    {
        for (int i = 0; i < notifs.Count; i++)
        {
            notif n = notifs[i];
            n.timing -= Time.deltaTime;
            if (n.timing <= 0f)
            {
                Destroy(n.obj);
                notifs.Remove(n);
            }
        }
    }

    public void newNotif(string description, notifState state)
    {
        notif nnotif = new notif();
        nnotif.description = description;
        nnotif.timing = timeNotif;
        nnotif.state = state;
        nnotif.obj = Instantiate(prefab, parent);
        Text text = nnotif.obj.GetComponentInChildren<Text>();
        if (state == notifState.important)
            text.color = Color.red;
        text.text = description;
        notifs.Add(nnotif);
    }
}

[System.Serializable]
public class notif
{
    public float timing;
    public string description;
    public notifState state;
    public GameObject obj;
}

[System.Serializable]
public enum notifState
{
    information,
    important
}