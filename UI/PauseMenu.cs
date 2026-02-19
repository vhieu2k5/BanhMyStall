using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    private GameObject pausebox;
    private GameObject continuebutton;
    private GameObject pausebg;
    void Start()
    {
        pausebox = GameObject.Find("pause box");
        continuebutton = GameObject.Find("continue button");
        pausebg = GameObject.Find("pausebg");
    //    pausebg.transform.DOMoveY(0 + Camera.main.transform.position.y, 0.5f);
        pausebg.GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
        continuebutton.transform.position = new Vector3(continuebutton.transform.position.x, -60 + Camera.main.transform.position.y, 1);
        pausebox.transform.position = new Vector3(0, 60 + Camera.main.transform.position.y, 1);
    }
    public void Activate()
    {
        Gamemanager.isMainScreen = true;
        SoundEffect.Instance.LoseEffect();
        pausebox.transform.DOMoveY(0.2f + Camera.main.transform.position.y, 0.5f).SetDelay(0.3f);
        continuebutton.transform.DOMoveY(-3.5f + Camera.main.transform.position.y, 0.5f).SetDelay(0.3f);
        pausebg.GetComponent<SpriteRenderer>().DOFade(0.8f, 0.5f);
        StartCoroutine(waitfor(1f));
        Gamemanager.isPause = true;
    }
    public void Continue()
    {
        Gamemanager.isMainScreen = false;
        Time.timeScale = 1;
        continuebutton.transform.DOMove(new Vector3(continuebutton.transform.position.x, -60 + Camera.main.transform.position.y, 1), 0.5f);
        pausebox.transform.position = new Vector3(0, 60 + Camera.main.transform.position.y, 1);
        pausebg.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        Gamemanager.isPause = false;
    }
    public static IEnumerator waitfor(float timer)
    {
        yield return new WaitForSeconds(timer);
        Time.timeScale = 0;
    }
}
