using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class StartScene : MonoBehaviour
{
  private GameObject startBox;
    private GameObject button;
    private GameObject bg;
    void Start()
    {
        startBox = GameObject.Find("start box");
        button = GameObject.Find("play button");
        bg = GameObject.Find("startbg");
        startBox.transform.DOMoveY(20f + Camera.main.transform.position.y, 0.5f).SetDelay(0.3f);
        button.transform.DOMoveY(-20f + Camera.main.transform.position.y, 0.5f).SetDelay(0.3f);
        bg.transform.DOMoveY(10.61f, 0.5f);
        bg.GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
    }
    public void Activate()
    {
        GameObject.Find("ReplayButton").GetComponent<UnityEngine.UI.Button>().interactable = false;
     //   Debug.Log("Y: " + Camera.main.transform.position.y);
        startBox.transform.DOMoveY(10.81f, 0.5f).SetDelay(0.5f);
        button.transform.DOMoveY(-3.5f + 10.61f, 0.5f).SetDelay(0.3f);
        bg.GetComponent<SpriteRenderer>().DOFade(0.6f, 0.5f);
        bg.transform.DOMoveY(10.61f, 0.5f);
    }
    public void DeActivate()
    {
        GameObject.Find("ReplayButton").GetComponent<UnityEngine.UI.Button>().interactable = true;
        float posY = Camera.main.transform.position.y;
        startBox.transform.DOMoveY(20f + posY, 0.5f).SetDelay(0.3f);
        button.transform.DOMoveY(-20f + posY, 0.5f).SetDelay(0.3f);
        bg.transform.DOMoveY(30 + posY, 0.5f);
   }
}
