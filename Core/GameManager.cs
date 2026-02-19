    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using System;

public class Gamemanager : MonoBehaviour
{
    public static PauseMenu pauseMenu;
    private static FailScene failScene;
    private static WinScene winScene;
    private static StartScene startScene;
    private static SettingMenu settingMenu;
    public CustomerManager customerManager;
    public Scripts scripts;
    public GameObject transition;
    [SerializeField] GameObject[] soundCheck;
    [SerializeField] GameObject[] bgMusicCheck;
    private bool soundTick = true;
    private bool bgMusicTick = true;
    public static Gamemanager Instance { get; private set; }
    public static int MoneyEarnedToday = 50;
    GameObject UIassets;
    public static bool isMainScreen = true;
    public static bool isPause = false;
    public Button servedButton;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public static void AddMoney(int earning)
    {
        MoneyEarnedToday += earning;
        //  MoneyEarnedToday += 30;
    }
    public static void MinusMoney(int cost)
    {
        MoneyEarnedToday -= cost;
    }
    public IEnumerator ReplayLevel()
    {
        Debug.Log("Replayed!");
        isMainScreen = true;
        isPause = false;
        Time.timeScale = 1;
        scripts.HideNoti();
        Camera.main.transform.DOMoveY(-0.75f, 1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    public IEnumerator StartNewDay()
    {
        SoundEffect.Instance.ClickEffect();
        Camera.main.transform.DOMoveY(10.61f, 1f).SetDelay(0.8f);
        GameObject.Find("UI SCript").GetComponent<StartScene>().Activate();
        scripts.ResetNoti();
        scripts.ShowNoti("MONEY: 60k" + '\n' + "CUSTOMERS: 03",2f);
        yield return new WaitForSeconds(1.5f);
        isMainScreen = false;
    }
     public static void IntegeratedUI()
     {
       GameObject[] UI = GameObject.FindGameObjectsWithTag("integeratedUI");
        foreach (GameObject ui in UI)
        {
            ui.transform.DOScaleX(0, 1f);
        }
    }
    public IEnumerator TurntheButton(float timer)
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("button");
        foreach (GameObject b in buttons)
        {
            if (b.GetComponent<BoxCollider2D>() != null)
                b.GetComponent<BoxCollider2D>().enabled = false;
            else if (b.GetComponent<CapsuleCollider2D>() != null)
                b.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        yield return new WaitForSeconds(timer);
        foreach (GameObject b in buttons)
        {
          if (b.GetComponent<BoxCollider2D>() != null)
                b.GetComponent<BoxCollider2D>().enabled = true;
            else if (b.GetComponent<CapsuleCollider2D>() != null)
                b.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
    public void RealStart()
    {
        ShowUI();
        isMainScreen = false;
        isPause = false;
        Scripts.numOfCus = 0;
        Time.timeScale = 1;
        customerManager.SpawnCustomer();
        scripts.HideNoti();
        SoundEffect.Instance.ClickEffect();
        startScene = GameObject.Find("UI SCript").GetComponent<StartScene>();
        if (startScene != null) startScene.DeActivate();
        Method.Reset();
    }
    public void DeactivateButtonsTemporarily(float delay)
    {
        StartCoroutine(DisableButtonsForSeconds(delay));
    }
    public void HideUI()
    {
        UIassets.GetComponent<Canvas>().enabled = false;
    }
    public void ShowUI()
    {
        UIassets.GetComponent<Canvas>().enabled = true;
       // scripts.HideNoti();
    }
    public IEnumerator ReturnToHomeScreen()
    {
       // StartCoroutine(anim.ActivateTransition(transition.GetComponent<Animator>(), 1f));
        scripts.HideNoti();
        Time.timeScale = 1;
        isPause = false;
        isMainScreen = true;
        GameObject.Find("settingbg").GetComponent<BoxCollider2D>().enabled = false;
        Camera.main.transform.DOMoveY(-0.75f, 1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    public void Pause()
    { if (!isPause)
        {
            pauseMenu.Activate();
            isPause = true;
    }
        
    }
    public void ContinuePlay()
    {
        pauseMenu.Continue();
    }
    public void SettingPlay()
    {
        StartCoroutine(TurntheButton(1f));
        settingMenu.Activate();
   }
    public void Exit()
    {
        StartCoroutine(TurntheButton(1f));
        settingMenu.Continue();
   }
    public void TurnSound()
    {
        if (soundTick)
        {
            SoundEffect.Instance.TurnOffSound();
            foreach (GameObject tick in soundCheck) {
                tick.SetActive(false);
            }
            soundTick = false;
        }
        else
        {
            SoundEffect.Instance.TurnOnSound();
            foreach (GameObject tick in soundCheck) {
                tick.SetActive(true);
            }
            soundTick = true;
        }

    }
    public void TurnBGMusic()
    {
        if (bgMusicTick)
        {
            SoundEffect.Instance.TurnOffBGMusic();
            foreach (GameObject tick in bgMusicCheck)
            {
                tick.SetActive(false);
            }
            bgMusicTick = false;
        }
        else
        {
            SoundEffect.Instance.TurnOnBGMusic();
            foreach (GameObject tick in bgMusicCheck)
            {
                tick.SetActive(true);
            }
            bgMusicTick = true;
        }
        
    }

    private IEnumerator DisableButtonsForSeconds(float delay)
    {
        // Find all buttons in the scene
        Button[] allButtons = FindObjectsOfType<Button>();

        // Disable them
        foreach (Button btn in allButtons)
        {
            btn.interactable = false;
        }

        // Wait for the given delay
        yield return new WaitForSeconds(delay);

        // Re-enable them
        foreach (Button btn in allButtons)
        {
            btn.interactable = true;
        }
    }
    void Start()
    {
        isMainScreen = true;
        UIassets = GameObject.Find("UI");
        if (!isMainScreen) Camera.main.transform.DOMoveY(-0.75f, 1f);
        MoneyEarnedToday = 50;
        OrderManager.MakingFiller();
        //customerManager.SpawnCustomer();
        //Banhmy.fill.Clear();
        Debug.Log("New day started!");
        failScene = GameObject.Find("UI SCript").GetComponent<FailScene>();
        winScene = GameObject.Find("UI SCript").GetComponent<WinScene>();
        pauseMenu = GameObject.Find("UI SCript").GetComponent<PauseMenu>();
        settingMenu = GameObject.Find("UI SCript").GetComponent<SettingMenu>();
        Scripts.customerChat = GameObject.Find("CustomerChat").GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (CustomerManager.customers.Count > 0)
        {
            scripts.LogOutPatience(CustomerManager.customers[CustomerManager.customers.Count - 1]);
            scripts.LogOutNumberCustomer(CustomerManager.customers[CustomerManager.customers.Count - 1]);
            if (CustomerManager.customers[CustomerManager.customers.Count - 1].served) customerManager.SpawnCustomer();
        }
        scripts.ShowMoney(MoneyEarnedToday);
        if (MoneyEarnedToday <= 0 || (Scripts.numOfCus > 3 && MoneyEarnedToday < 60))
        {
            HideUI();
            failScene.Activate();
            Scripts.numOfCus = 0;
            MoneyEarnedToday = 50;
            scripts.ShowNoti("MONEY: " + MoneyEarnedToday.ToString() + "K",1f);
           // StartCoroutine(PauseMenu.waitfor(3f));
            scripts.noti.GetComponent<RectTransform>().localPosition = new Vector3(0, -30f, 0);
            scripts.noti.GetComponent<TextMeshProUGUI>().color = new Color32(112, 80, 69, 255);
            scripts.noti.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            scripts.noti.GetComponent<TextMeshProUGUI>().fontSize = 90;
        }
        if (Scripts.numOfCus > 3)
        {
            HideUI();
            Scripts.numOfCus = 0;
            winScene.Activate();
            scripts.noti.GetComponent<RectTransform>().localPosition = new Vector3(350f, -30f, 0);
            scripts.noti.GetComponent<TextMeshProUGUI>().color = new Color32(136, 54, 0, 255);
            scripts.noti.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
            scripts.noti.GetComponent<TextMeshProUGUI>().fontSize = 120;
            scripts.ShowNoti(MoneyEarnedToday.ToString() + "K",1f);
        }
       
    }
    void FixedUpdate()
    {
        if (isMainScreen) HideUI();
        else ShowUI();
        if (Banhmy.fill.Count() < 1) servedButton.interactable = false;
        else servedButton.interactable = true;
    }

}
