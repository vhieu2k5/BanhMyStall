using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class tutorial : MonoBehaviour
{
    public VideoPlayer tutorialVideo;
    public GameObject guildText;
    int tut = 0;
    public static bool isPlaying;
    bool isScreenPlaying;
    public static tutorial Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
       
    }
    void Start()
    {
        #if UNITY_WEBGL
        string videoPath = Path.Combine(Application.streamingAssetsPath, "tut1.mp4");
        tutorialVideo.url = videoPath;
#endif

        tutorialVideo.Prepare();
        tutorialVideo.prepareCompleted += (vp) => {
            vp.Play();
        };
        isPlaying = true;
      //  StartCoroutine(Gamemanager.Instance.TurntheButton(0f));
        tutorialVideo.gameObject.transform.position = new Vector3(0, 0, 0);
        StartCoroutine(StopFor(1.5f));
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            SoundEffect.Instance.ChooseEffect();
            if (tut > 8 && !isScreenPlaying) StartCoroutine(OnVideoFinished(tutorialVideo));
            if (!isScreenPlaying)
                switch (tut)
                {
                    case 1:
                        StartCoroutine(StopFor(2.5f));
                        break;
                    case 2:
                        StartCoroutine(StopFor(2.5f));
                        break;
                    case 3:
                        StartCoroutine(StopFor(5f));
                        break;
                    case 4:
                        StartCoroutine(StopFor(5f));
                        break;
                    case 5:
                        StartCoroutine(StopFor(2.5f));
                        break;
                    case 6:
                        StartCoroutine(StopFor(5f));
                        break;
                    case 7:
                        StartCoroutine(StopFor(5.5f));
                        break;
                    case 8:
                        StartCoroutine(StopFor(5f));
                        break;
                }
        }
    }

    IEnumerator OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished!");
        Time.timeScale = 1;
        guildText.SetActive(false);
       // StartCoroutine(anim.ActivateTransition(Gamemanager.Instance.transition.GetComponent<Animator>(), 1f));
        float t = 0;
        while (t < 2f)
        {
            t += Time.deltaTime;
            tutorialVideo.targetCameraAlpha = Mathf.Lerp(1, 0, t / 2f);
            yield return null;
        }
        // Option 1: Stop video
        vp.Stop();
        yield return new WaitForSeconds(0f);

        // Option 2: Hide or disable video object
        if (tutorialVideo != null)
        {
            tutorialVideo.gameObject.SetActive(false);
        }
        isPlaying = false;

    }

    IEnumerator StopFor(float timer)
    {
        tut++;
        isScreenPlaying = true;
        tutorialVideo.Play();
        guildText.SetActive(false);
        yield return new WaitForSeconds(timer);
        tutorialVideo.Pause();
        isScreenPlaying = false;
        guildText.SetActive(true);
    }
}
