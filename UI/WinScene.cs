using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WinScene : MonoBehaviour
{
    private GameObject winbox;
    private GameObject button;
    private GameObject bg;
    private GameObject aura;
    void Start()
    {
        winbox = GameObject.Find("win box");
        button = GameObject.Find("next button");
        bg = GameObject.Find("winbg");
        aura = GameObject.Find("aura");
      //  bg.transform.DOMoveY(0 + Camera.main.transform.position.y, 0.5f);
        bg.GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
        button.transform.position = new Vector3(0, -60 + Camera.main.transform.position.y, 1);
        winbox.transform.position = new Vector3(0, 60 + Camera.main.transform.position.y, 1);
        aura.transform.localScale = new Vector3(0, 0, 0);
    }
    public void Activate()
    {
        Gamemanager.Instance.HideUI();
        SoundEffect.Instance.WinEffect();
        SoundEffect.bgmusic.Pause();
        Debug.Log("Y camera: " + Camera.main.transform.position.y);
        winbox.transform.DOMoveY(0.22f + Camera.main.transform.position.y, 0.5f).SetDelay(0.3f);
        button.transform.DOMoveY(-3.2f + Camera.main.transform.position.y, 0.5f).SetDelay(0.3f);
        bg.GetComponent<SpriteRenderer>().DOFade(0.8f, 0.5f);
        aura.transform.DOMoveY(winbox.transform.position.y + 0.18f, 0.5f).SetDelay(0.3f);
        aura.transform.DOScale(new Vector3(0.05f, 0.12f, 1), 1f).SetDelay(1f);
        Gamemanager.isPause = true;
    }
    public void DeActivate()
    {
        Scripts.numOfCus = 0;
        Gamemanager.MoneyEarnedToday = 50;
        float posY = Camera.main.transform.position.y;
        winbox.transform.DOMoveY(20f + posY, 0.5f).SetDelay(0.3f);
        button.transform.DOMoveY(-20f + posY, 0.5f).SetDelay(0.3f);
        bg.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        aura.transform.localScale = new Vector3(0, 0, 0);
        Gamemanager.isPause = false;
   }
}
