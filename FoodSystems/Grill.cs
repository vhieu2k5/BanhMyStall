using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Grill : cookingMethod
    {
    [SerializeField] private GameObject grilledSausage;
    [SerializeField] private GameObject grillegg;
    [SerializeField] private GameObject cookingpan;
    [SerializeField] private GameObject cookingGasFire;
    public GameObject oil;
    public static bool grilling;
        private GameObject clone;
        void Start()
        {
        grilling = false;
        }
    void Update()
    {
        if (Method.taken) cookingGasFire.GetComponent<Animator>().SetInteger("fire", 0);
    }
    public override void Activate(GameObject food)
    {
        StartCoroutine(anim.ActivateGasFire(cookingGasFire.GetComponent<Animator>()));
        StartCoroutine(SoundEffect.Instance.GasFiringEffect());
        if (food != null && !grilling)
        {
            if (food.gameObject.name == "Nom")
            {
                clone = Instantiate(grilledSausage, food.transform.position, food.transform.rotation);
                clone.SetActive(true);
                clone.transform.localScale = new Vector3(0, 0, 0);//Animation
                clone.transform.DOMove(cookingpan.transform.position, 0.5f);
                clone.transform.DOScale(new Vector3(1f, 1f, 3f), 0.5f);
                clone.GetComponent<SpriteRenderer>().DOColor(new Color(200, 75, 75), 1f);
                StartCoroutine(CookingTime(food.gameObject.name, clone.GetComponent<fillings>().CookingTime));

            }
            if (food.gameObject.name == "egg")
            {
                clone = Instantiate(grillegg, food.transform.position, food.transform.rotation);
                clone.SetActive(true);
                StartCoroutine(anim.ActivateDropEgg(clone.GetComponent<Animator>()));
                clone.transform.localScale = new Vector3(0, 0, 0);//Animation
                clone.transform.DOMove(cookingpan.transform.position, 0.5f);
                clone.transform.DOMoveY(cookingpan.transform.position.y + 0.5f, 1f).SetDelay(0.5f);
                clone.transform.DOScale(new Vector3(2f, 2f, 3f), 0.5f);
                clone.GetComponent<SpriteRenderer>().DOColor(new Color32(255, 255, 255, 225), 5f);
                StartCoroutine(CookingTime(food.gameObject.name, clone.GetComponent<fillings>().CookingTime));
            }

        }
    }
    public IEnumerator CookingTime(string nameofFood, float CookingTime)
    {
        oil.GetComponent<SpriteRenderer>().DOFade(1, 2f);
        oil.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 2f);
        grilling = true;
        Method.taken = false;
        clone.GetComponent<CircleCollider2D>().enabled = false;
        clone.GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
        clone.GetComponent<SpriteRenderer>().DOFade(1, CookingTime + 3f).SetDelay(0.2f);
        UnityEngine.UI.Image timerfill1 = GameObject.Find("timer1 fill").GetComponent<UnityEngine.UI.Image>();
        UnityEngine.UI.Image timerfill2 = GameObject.Find("timer2 fill").GetComponent<UnityEngine.UI.Image>();
        GameObject timer1 = GameObject.Find("timer1");
        GameObject timer2 = GameObject.Find("timer2");
        timerfill2.fillAmount = 0;
        timerfill1.fillAmount = 0;
       timer2.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        timer1.transform.DOScale(new Vector3(0, 1, 1), 0.2f);
        CookingTime++;
         if (nameofFood == "Nom" && !Banhmy.fill.Contains(grilledSausage.GetComponent<fillings>()))
        {
            clone.GetComponent<SpriteRenderer>().DOColor(new Color32(150, 0, 3, 255), 10f).SetEase(Ease.InOutFlash);
            Debug.Log("Grilled sausage is ready!");
        }
        if ((nameofFood == "egg") && !Banhmy.fill.Contains(grillegg.GetComponent<fillings>()))
        {
            Debug.Log("Grilled egg is ready!");
            clone.GetComponent<SpriteRenderer>().DOColor(new Color32(255, 210, 177, 255), 3f).SetDelay(7f).SetEase(Ease.InOutFlash);
            clone.transform.DOScale(new Vector3(2f, 2f, 3f), 1f);
            clone.transform.DOMoveZ(-2f, 0.2f);
        }
        while (CookingTime > 0)
        {
            timerfill2.fillAmount += 0.17f;
            yield return new WaitForSeconds(1f);
            CookingTime--;
        }
        //SOund Effect
        SoundEffect.Instance.soundEffect.clip = SoundEffect.Instance.tingAudio;
        SoundEffect.Instance.soundEffect.Play();
        //
        
        clone.tag = "ingredients";
        grilling = false;
        float timerBurning = 10f;
        timer2.transform.DOScale(new Vector3(0, 1, 1), 0.2f);
        timer1.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
        clone.GetComponent<CircleCollider2D>().enabled = true;
        while (timerBurning > 0)
        {
            timerfill1.fillAmount += 0.1f;
            yield return new WaitForSeconds(1f);
            timerBurning--;
            if (Method.taken)
            {
                timerBurning = 0;
                timer2.transform.DOScale(new Vector3(0, 1, 1), 0.2f);
                timer1.transform.DOScale(new Vector3(0, 1, 1), 0.2f);
            }
        }
        oil.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        oil.transform.DOScale(new Vector3(0, 0, 0.8f), 2f);
       if (clone != null) clone.tag = "CookingFood";

        if (!Method.taken)
        {
            SoundEffect.Instance.FalseEffect();
            PlayerAction.ThrowCookingFood();
            Debug.Log("Food is burned!");
            if (clone != null) clone.tag = "ServedFood";
            Method.taken = true;
        }
        }
}
