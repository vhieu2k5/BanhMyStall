using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SettingMenu : MonoBehaviour
{
    
    private GameObject Settingbox;
    private GameObject pausebg;
    void Start()
    {
        Settingbox = GameObject.Find("setting box");
        pausebg = GameObject.Find("settingbg");
        pausebg.transform.DOMoveY(0 + Camera.main.transform.position.y, 0.5f);
        pausebg.GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
        Settingbox.transform.position = new Vector3(0, 60 + Camera.main.transform.position.y, 1);
        pausebg.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Activate()
    {
        StartCoroutine(Gamemanager.Instance.TurntheButton(1f));
        Gamemanager.isMainScreen = true;
        SoundEffect.Instance.LoseEffect();
        Settingbox.transform.DOMoveY(0.2f + Camera.main.transform.position.y, 0.5f).SetDelay(0.3f);
        pausebg.GetComponent<SpriteRenderer>().DOFade(0.8f, 0.5f);
        pausebg.GetComponent<SpriteRenderer>().sortingOrder = 30;
        StartCoroutine(waitfor(1f));
        Gamemanager.isPause = true;
        GameObject.Find("play button").GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Continue()
    {
        StartCoroutine(Gamemanager.Instance.TurntheButton(1f));
        Time.timeScale = 1;
        Settingbox.transform.position = new Vector3(0, 60 + Camera.main.transform.position.y, 1);
        pausebg.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        pausebg.GetComponent<SpriteRenderer>().sortingOrder = -1;
        Gamemanager.isPause = false;
        GameObject.Find("play button").GetComponent<BoxCollider2D>().enabled = true;
    }
    public static IEnumerator waitfor(float timer)
    {
        yield return new WaitForSeconds(timer);
        Time.timeScale = 0;
    }
}
