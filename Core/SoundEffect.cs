using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public static AudioSource bgmusic;
    public AudioClip clickButton;
    public AudioClip chooseButton;
    public AudioClip WinAudio;
    public AudioClip LoseAudio;
    public AudioClip waitingAudio;
    public AudioClip TrueAudio;
    public AudioClip FalseAudio;
    public AudioClip orderAudio;
    public AudioClip buyAudio;
    public AudioClip gasInit;
    public AudioClip fryingPan;
    public AudioSource soundEffect;
    public AudioSource soundEffect2;
    public AudioClip squeezeAudio;
    public AudioClip closePanGrillAudio;
    public AudioClip tingAudio;
    public static SoundEffect Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        AudioListener.pause = false;   // Bật lại audio
        AudioListener.volume = 1f;  
        bgmusic = GameObject.Find("bg music").GetComponent<AudioSource>();
        bgmusic.Play();
        
    }
    public IEnumerator grillPanEffect()
    {
        yield return new WaitForSeconds(2f);
        soundEffect.clip = closePanGrillAudio;
        soundEffect.Play();
        yield return new WaitForSeconds(4f);
        soundEffect.clip = tingAudio;
        soundEffect.Play();
    }
    public void SqueezeEffect()
    {
        soundEffect.clip = squeezeAudio;
        soundEffect.Play();
    }
    public void ClickEffect()
    {
        soundEffect.clip = clickButton;
        soundEffect.Play();
    }
    public void ChooseEffect()
    {
        soundEffect.clip = chooseButton;
        soundEffect.Play();
    }
    public void WinEffect()
    {
        soundEffect.clip = WinAudio;
        soundEffect.Play();
    }
    public void LoseEffect()
    {
        soundEffect.clip = LoseAudio;
        soundEffect.Play();
    }
    public void AppearEffect()
    {
        soundEffect.clip = orderAudio;
        soundEffect.Play();
    }
    public void WaitEffect()
    {
        soundEffect.clip = waitingAudio;
        soundEffect.Play();
    }
    public void TrueEffect()
    {
        soundEffect.clip = TrueAudio;
        soundEffect.Play();
    }
    public void FalseEffect()
    {
        soundEffect.clip = FalseAudio;
        soundEffect.Play();
    }
    public void BuyEffect()
    {
        soundEffect.clip = buyAudio;
        soundEffect.Play();
    }
    public IEnumerator GasFiringEffect()
    {
        soundEffect2.clip = gasInit;
        soundEffect2.Play();
        yield return new WaitForSeconds(2f);
        soundEffect2.clip = fryingPan;
        soundEffect2.Play();
    }
    public void TurnOffBGMusic()
    {
        bgmusic.Stop();
    }
    public void TurnOnBGMusic()
    {
        bgmusic.Play();
    }
    public void TurnOffSound()
    {
        soundEffect.mute = true;
        soundEffect2.mute = true;
    }
    public void TurnOnSound()
    {
        soundEffect.mute = false;
        soundEffect2.mute = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
