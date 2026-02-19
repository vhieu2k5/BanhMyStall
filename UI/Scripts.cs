using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Playables;

public class Scripts : MonoBehaviour
{
    void Start()
    {
        noti.GetComponent<RectTransform>().localPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        noti.GetComponent<TextMeshProUGUI>().color = new Color32(97, 24, 0, 255);
    }
    public TMP_Text NumberOfCustomer;
    public static int numOfCus = 0;
    public TMP_Text money;
    public TMP_Text Patience;
    public static TMP_Text customerChat;
    public GameObject noti;
    public void LogOutPatience(CustomerManager.Customer cus)
    {
        Patience.text = cus.waitingTime.ToString() + "s";
    }
    public void LogOutNumberCustomer(CustomerManager.Customer cus)
    {
        NumberOfCustomer.text = numOfCus.ToString() + "/3";
    }
    public static void OpenCustomerChat(string chat)
    {
        if (customerChat != null)
        {
            customerChat.text = chat;
            customerChat.DOFade(1, 0.5f).SetDelay(1f);
        }

    }
    public static void CloseCustomerChat()
    {
        if (customerChat != null) customerChat.DOFade(0, 0.5f);
    }
    public void ShowMoney(int moneyearned)
    {
        money.text = moneyearned.ToString() + "K/60k";
    }
    public void ShowNoti(string s, float timer)
    {
       // Debug.Log("show noti");
        noti.GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 0), 0.1f).SetDelay(timer);
        noti.GetComponent<TextMeshProUGUI>().text = s;
    }
    public void HideNoti()
    {
        noti.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0), 0.1f);
    }
    public void ResetNoti()
    {
    noti.GetComponent<RectTransform>().localPosition = Camera.main.ScreenToWorldPoint(new Vector3(143, -26, 0));
    noti.GetComponent<TextMeshProUGUI>().color = new Color32(97, 24, 0, 255);
    noti.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
    noti.GetComponent<TextMeshProUGUI>().fontSize = 70;
   }

}
