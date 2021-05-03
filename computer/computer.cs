using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class computer : MonoBehaviour
{
    [Header("Computer System")]
    [SerializeField]
    Session session;
    [Header("Options")]
    [SerializeField]
    List<Behaviour> componentsToDisactive = new List<Behaviour>();
    [Header("UI")]
    [SerializeField]
    GameObject computerMenu;
    [SerializeField]
    login_ui uiLogin;
    [SerializeField]
    window_ui uiWindow;
    [SerializeField]
    police_app_ui uiPolice;
    [SerializeField]
    twitter_ui uiTwitter;
    [SerializeField]
    whats_app_ui uiWhatsApp;
    private KeyCode interract;
    private bool triggered;
    private bool inComputer;
    [HideInInspector]
    public int curr_suspect;
    // Start is called before the first frame update
    void Start()
    {
        BTN_LEAVE_COMPUTER();
        interract = (KeyCode)PlayerPrefs.GetInt("interract");
        uiLogin.initImages(session);
        uiWindow.setupApps(session, this);
        if (session.police != null)
            uiPolice.init_police_app(session, this);
        if (session.twitter != null)
            uiTwitter.init_twitter(session, this);
        if (session.whatsapp != null)
            uiWhatsApp.set_values(session, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inComputer && triggered && Input.GetKeyDown(interract))
        {
            openComputer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            KeyCode interract = (KeyCode)PlayerPrefs.GetInt("interract");
            string msg = "Interract with computer with \"" + interract.ToString() + "\"";
            PlayerStat stat = other.GetComponent<PlayerStat>();
            if (stat != null)
            {
                stat.setInterractText(msg);
                stat.showInterract();
                triggered = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStat stat = other.GetComponent<PlayerStat>();
            if (stat != null)
            {
                stat.hideInterract();
                triggered = false;
            }
        }
    }

    private void openComputer()
    {
        inComputer = true;
        triggered = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        computerMenu.SetActive(true);
        for (int i = 0; i < componentsToDisactive.Count; i++)
            componentsToDisactive[i].enabled = false;
        if (session.logged)
        {
            uiLogin.loginMenu.SetActive(false);
            uiWindow.windowMenu.SetActive(true);
        } else
        {
            uiLogin.loginMenu.SetActive(true);
            uiWindow.windowMenu.SetActive(false);
        }
    }

    public void BTN_LEAVE_COMPUTER()
    {
        computerMenu.SetActive(false);
        inComputer = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        for (int i = 0; i < componentsToDisactive.Count; i++)
            componentsToDisactive[i].enabled = true;
    }

    public void BTN_LOGGIN_WINDOW()
    {
       if (uiLogin.goodPassword(session))
        {
            uiLogin.loginMenu.SetActive(false);
            uiWindow.windowMenu.SetActive(true);
        }
    }

    public void BTN_DISCONNECT_WINDOW()
    {
        if (session.logged)
        {
            BTN_LEAVE_COMPUTER();
        } else
        {
            uiLogin.loginMenu.SetActive(true);
            uiWindow.windowMenu.SetActive(false);
        }
    }

    public void BTN_DENOUCE_SUSPECT()
    {
        GameObject suspect = uiPolice.getSuspects()[curr_suspect];
        suspectPrefabScript suspect_script = suspect.GetComponent<suspectPrefabScript>();
        suspectComputer suspectCard = suspect_script.getSuspectCard();
        PlayerStat stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        Debug.Log("denounce : " + suspectCard.firstname + " " + suspectCard.lastname);
        if (suspectCard.isMurder)
        {
            stat.loadWinScene();
        } else
        {
            stat.loadLooseScene();
        }
    }

    public void open_app(appType type)
    {
        if (type == appType.policeapp)
        {
            uiWindow.policeMenu.SetActive(true);
            uiWindow.twitterMenu.SetActive(false);
            uiWindow.whatsappMenu.SetActive(false);
        } else if (type == appType.twitter)
        {
            uiWindow.policeMenu.SetActive(false);
            uiWindow.twitterMenu.SetActive(true);
            uiWindow.whatsappMenu.SetActive(false);
        } else if (type == appType.whatsapp)
        {
            uiWindow.policeMenu.SetActive(false);
            uiWindow.twitterMenu.SetActive(false);
            uiWindow.whatsappMenu.SetActive(true);
        }
    }

    public void open_discussion_whatsapp(int id)
    {
        uiWhatsApp.change_discuss(id, this, session);
    }

    public void open_account_whatsapp(whatsappAccount account)
    {
        uiWhatsApp.showAccount(account);
    }

    public GameObject generateApp(GameObject obj, Transform par)
    {
        GameObject gobj = Instantiate(obj, par);
        return (gobj);
    }

    public void destroyGo(GameObject go)
    {
        Destroy(go);
    }

    public void add_suspect_in_list(suspectComputer suspect)
    {
        uiPolice.add_suspect_police_ui(suspect, this);
    }

    public void showOwnAccount()
    {
        uiTwitter.showAccount(session.twitter.accoutnSession, this);
    }
}

[System.Serializable]
public class login_ui
{
    public GameObject loginMenu;
    public Image pp;
    public InputField pwd;
    public Text pseudoText;

    public bool goodPassword(Session login)
    {
        if (login.login.password == pwd.text)
            return (true);
        return (false);
    }

    public void initImages(Session login)
    {
        pp.sprite = login.login.pp;
        pp.preserveAspect = true;
        pseudoText.text = login.login.pseudo;
    }
}

[System.Serializable]
public class window_ui
{
    public GameObject windowMenu;
    public Transform parentApps;
    public GameObject prefabApp;
    [HideInInspector]
    public List<GameObject> apps = new List<GameObject>();
    public GameObject policeMenu;
    public GameObject twitterMenu;
    public GameObject whatsappMenu;

    public void setupApps(Session session, computer comp)
    {
        policeMenu.SetActive(false);
        twitterMenu.SetActive(false);
        whatsappMenu.SetActive(false);
        if (session.twitter != null)
        {
            GameObject obj = comp.generateApp(prefabApp, parentApps);
            obj.GetComponent<prefabAppScript>().setValues(session.twitter.logoTwitter, appType.twitter, comp, session.twitter.nameApp);
            apps.Add(obj);
        }
        if (session.police != null)
        {
            GameObject obj = comp.generateApp(prefabApp, parentApps);
            obj.GetComponent<prefabAppScript>().setValues(session.police.logo, appType.policeapp, comp, session.police.nameApp);
            apps.Add(obj);
        }
        if (session.whatsapp != null)
        {
            GameObject obj = comp.generateApp(prefabApp, parentApps);
            obj.GetComponent<prefabAppScript>().setValues(session.whatsapp.logo, appType.whatsapp, comp, session.whatsapp.nameApp);
            apps.Add(obj);
        }
    }
}

[System.Serializable]
public class twitter_ui
{
    public GameObject prefabTrend;
    public Transform parentTrend;
    public GameObject prefabtweet;
    public Transform parentTweetActu;
    public Transform parentTweetProfile;
    public GameObject Actu;
    public GameObject Profile;
    List<GameObject> trends = new List<GameObject>();
    List<GameObject> tweetsActu = new List<GameObject>();
    List<GameObject> tweetProfile = new List<GameObject>();
    public Text pseudoAccount;
    public Image bannerAccount;
    public Image ppAccount;
    public Text descriptionAccount;

    public void init_twitter(Session session, computer comput)
    {
        twitterSession twitter = session.twitter;
        bannerAccount.sprite = twitter.accoutnSession.banner;
        ppAccount.sprite = twitter.accoutnSession.pp;
        pseudoAccount.text = twitter.accoutnSession.pseudo;
        descriptionAccount.text = twitter.accoutnSession.descriptionAccount;
        for (int i = 0; i < twitter.actuality.tendances.Count; i++)
        {
            GameObject gobj = comput.generateApp(prefabTrend, parentTrend);
            trendPrefabScript script = gobj.GetComponent<trendPrefabScript>();
            script.setValues(twitter.actuality.tendances[i]);
            trends.Add(gobj);
        }
        for (int i = 0; i < twitter.actuality.actuPost.Count; i++)
        {
            GameObject gobj = comput.generateApp(prefabtweet, parentTweetActu);
            tweetPrefabScript script = gobj.GetComponent<tweetPrefabScript>();
            script.set_values(twitter.actuality.actuPost[i], comput, prefabtweet, true, this);
            tweetsActu.Add(gobj);
        }
    }

    public void showAccount(twitterAccount account, computer compute)
    {
        Actu.SetActive(false);
        Profile.SetActive(true);
        pseudoAccount.text = account.pseudo;
        bannerAccount.sprite = account.banner;
        ppAccount.sprite = account.pp;
        descriptionAccount.text = account.descriptionAccount;
        foreach (GameObject gobj in tweetProfile)
            compute.destroyGo(gobj);
        tweetsActu.Clear();
        for (int i = 0; i < account.accountPosts.Count; i++)
        {
            GameObject obj = compute.generateApp(prefabtweet, parentTweetProfile);
            tweetPrefabScript script = obj.GetComponent<tweetPrefabScript>();
            script.set_values(account.accountPosts[i], compute, prefabtweet, true, this);
            tweetProfile.Add(obj);
        }
    }
}

[System.Serializable]
public class whats_app_ui
{
    [Header("Account UI")]
    public GameObject accountPanel;
    public Text nameAccount;
    public Text phoneNumberAccount;
    public Image ppAccount;
    public Text mailAccount;

    [Header("Discussions")]
    public GameObject prefabDisussions;
    public Transform parentDiscussions;
    [Header("Message")]
    public GameObject prefabMessage;
    public Transform parentMessage;
    List<GameObject> discuss_s = new List<GameObject>();
    List<GameObject> discus = new List<GameObject>();

    public void set_values(Session session, computer compute)
    {
        accountPanel.SetActive(false);
        for (int i = 0; i < session.whatsapp.discussions.Count; i++)
        {
            GameObject gobj = compute.generateApp(prefabDisussions, parentDiscussions);
            whatsAppDiscussionPrefab script = gobj.GetComponent<whatsAppDiscussionPrefab>();
            script.set_values(session.whatsapp.discussions[i], compute, i);
            discuss_s.Add(gobj);
        }
    }

    public void change_discuss(int id, computer compute, Session session)
    {
        for (int i = 0; i < discus.Count; i++)
            compute.destroyGo(discus[i]);
        discus.Clear();
        whatsAppDiscussionPrefab dis = discuss_s[id].GetComponent<whatsAppDiscussionPrefab>();
        whatsappDiscussion discu = session.whatsapp.discussions[dis.getId()];
        for (int i = 0; i < discu.messages.Count; i++)
        {
            GameObject gobj = compute.generateApp(prefabMessage, parentMessage);
            whatsAppMessagePrefabScript script = gobj.GetComponent<whatsAppMessagePrefabScript>();
            script.set_values(discu.messages[i], session.whatsapp, compute);
            discus.Add(gobj);
        }
    }

    public void showAccount(whatsappAccount account)
    {
        phoneNumberAccount.text = account.phoneNumber;
        mailAccount.text = account.mail;
        nameAccount.text = account.pseudo;
        ppAccount.preserveAspect = true;
        ppAccount.sprite = account.pp;
        accountPanel.SetActive(true);
    }
}

[System.Serializable]
public class police_app_ui
{
    public GameObject prefabSuspect;
    public Transform parentSuspect;
    public Text firstname_police;
    public Text lastname_police;
    public Text age_police;
    public Text grade_police;
    public Text description_police;
    public Text firstname_suspect;
    public Text lastname_suspect;
    public Text age_suspect;
    public Text description_suspect;
    public Image img_suspect;
    public Image pp_police;
    public Button denounce;
    List<GameObject> suspects = new List<GameObject>();

    public void init_police_app(Session session, computer comput)
    {
        firstname_police.text = session.police.firstname;
        lastname_police.text = session.police.lastname;
        age_police.text = session.police.age + " ans";
        grade_police.text = session.police.grade;
        pp_police.sprite = session.police.pp;
        pp_police.preserveAspect = true;
        description_police.text = session.police.description;
        for (int i = 0; i < session.police.suspectsCard.Count; i++)
        {
            GameObject suspect = comput.generateApp(prefabSuspect, parentSuspect);
            suspectPrefabScript script = suspect.GetComponent<suspectPrefabScript>();
            script.init_values(session.police.suspectsCard[i], this, comput, suspects.Count);
            suspects.Add(suspect);
        }
    }

    public void show_suspect(int id, suspectComputer suspect, computer compute)
    {
        compute.curr_suspect = id;
        firstname_suspect.text = suspect.firstname;
        lastname_suspect.text = suspect.lastname;
        age_suspect.text = suspect.age + " ans";
        if (suspect.age < 0)
            age_suspect.text = "? ans";
        description_suspect.text = suspect.description;
        denounce.interactable = true;
        img_suspect.sprite = suspect.pp;
        img_suspect.preserveAspect = true;
    }

    public void add_suspect_police_ui(suspectComputer suspect, computer compute)
    {
        GameObject gobj = compute.generateApp(prefabSuspect, parentSuspect);
        suspectPrefabScript script = gobj.GetComponent<suspectPrefabScript>();
        script.init_values(suspect, this, compute, suspects.Count);
        suspects.Add(gobj);
    }

    public List<GameObject> getSuspects()
    {
        return (suspects);
    }
}
