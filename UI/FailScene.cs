using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FailScene : MonoBehaviour
{
    private GameObject failbox;
    private GameObject button;
    private GameObject bg;
    void Start()
    {
        failbox = GameObject.Find("fail box");
        button = GameObject.Find("replay button");
        bg = GameObject.Find("bg");
      //  bg.transform.DOMoveY(0 + Camera.main.transform.position.y, 0.5f);
        bg.GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
        button.transform.position = new Vector3(0, -60 + Camera.main.transform.position.y, 1);
        failbox.transform.position = new Vector3(0, 60 + Camera.main.transform.position.y, 1);
    }
    public void Activate()
    {
        SoundEffect.Instance.LoseEffect();
        failbox.transform.DOMoveY(0.2f + Camera.main.transform.position.y, 0.5f).SetDelay(0.3f);
        button.transform.DOMoveY(-3.5f + Camera.main.transform.position.y, 0.5f).SetDelay(0.3f);
        bg.GetComponent<SpriteRenderer>().DOFade(0.8f, 0.5f);
    }
}
