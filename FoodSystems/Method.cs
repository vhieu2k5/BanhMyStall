using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using JetBrains.Annotations;
using System;
using UnityEngine.UI;

public class Method : MonoBehaviour
{
    static public GameObject dish;
    static GameObject GrillMachine;
    public static bool taken = true;
    public static GameObject butter;
    public static GameObject upperPart;

    void Start()
    {
        taken = true;
        dish = GameObject.Find("cutting board");
        GrillMachine = GameObject.Find("crunchy");
        butter = GameObject.Find("SpreadButter");
        upperPart = GameObject.Find("upper");
    }
    public static void Reset()
    {
        Gamemanager.Instance.ShowUI();
        SpreadSouce.spreadedmayo = false;
        SpreadSouce.spreadedspicy = false;
        SpreadButter.spreadbutter = false;
        cookingMethod.addBread = false;
        Banhmy.fill.Clear();
        if (CustomerManager.customers.Count > 0)
            CustomerManager.customers[CustomerManager.customers.Count - 1].CurrentOrder.grilled = false;
    }
    public class SpreadSouce : cookingMethod
    {
        public static bool spreadedspicy = false;
        public static bool spreadedmayo = false;
        public override void Activate(GameObject spreadKetchup)
        {
            if (spreadKetchup != null && ((!spreadedspicy && !spreadedmayo) || (!spreadedspicy && spreadKetchup.gameObject.name == "SpreadKetchup") || (!spreadedmayo && spreadKetchup.gameObject.name == "SpreadMayo")))
            {
                GameObject clone = Instantiate(spreadKetchup, spreadKetchup.transform.position, spreadKetchup.transform.rotation);
                clone.SetActive(true);
                clone.GetComponent<SpriteRenderer>().sortingOrder = PickandChoose.layer++;
                clone.transform.DOScale(new Vector3(1f, 1f, 3f), 1f);
                clone.tag = "ServedFood";
                if (spreadKetchup.gameObject.name == "SpreadKetchup") spreadedspicy = true;
                else if (spreadKetchup.gameObject.name == "SpreadMayo") spreadedmayo = true;
                if (!Banhmy.fill.Contains(spreadKetchup.GetComponent<fillings>()))
                {
                    Banhmy.fill.Add(spreadKetchup.GetComponent<fillings>());
                    Debug.Log("Added " + spreadKetchup.name);
                }
            }

        }
        public static void Deactivate(GameObject spreadKetchup)
        {
            if (spreadKetchup != null)
            {
                spreadKetchup.SetActive(false);
                 if (spreadKetchup.gameObject.name == "SpreadKetchup") spreadedspicy = false;
            else if (spreadKetchup.gameObject.name == "SpreadMayo") spreadedmayo = false;
            }
            Bread.isContact = false;
            //addBread = false;
        }
    }
    public class GrillPan : cookingMethod
    {
       public static bool isPanGrilling = false;
        public override void Activate(GameObject food)
        {
            
            if (!isPanGrilling && Banhmy.fill.Count>0)
            {
                StartCoroutine(SoundEffect.Instance.grillPanEffect());
                StartCoroutine(anim.ActivateGriller(GrillMachine.GetComponent<Animator>()));
                StartCoroutine(grillingPan(5f));
            }
                
        }
        IEnumerator grillingPan(float timer)
        {
            isPanGrilling = true;
            //Animation
                GameObject upper = Instantiate(upperPart, dish.transform.position + new Vector3(1f, 0, 0), Quaternion.identity);
                upper.transform.DOMoveX(dish.transform.position.x,0.2f);
                upper.tag = "ServedFood";
                upper.GetComponent<SpriteRenderer>().sortingOrder = ++PickandChoose.layer;
            yield return new WaitForSeconds(1f);
            GameObject[] banhmy = GameObject.FindGameObjectsWithTag("ServedFood");
            foreach (GameObject food in banhmy)
            {
                if (food.name == "pan(Clone)")
                {
                food.GetComponent<Animator>().SetInteger("cut",0);
                food.transform.DOMove(GrillMachine.transform.position, 0.8f);
                food.transform.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color32(142, 27, 0, 0), 0.3f).SetDelay(1f);
                food.transform.DOScale(food.transform.localScale - new Vector3(0.2f, 0.2f, 0.2f),0.8f);
                food.transform.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color32(255, 198, 152, 255), 0.2f).SetDelay(5.7f);
                }
                else food.GetComponent<SpriteRenderer>().DOFade(0,0.3f);
            }
            
            yield return new WaitForSeconds(timer);
            Debug.Log("Done a pan!!!");
            isPanGrilling = false;
            if (CustomerManager.customers.Count > 0) CustomerManager.customers[CustomerManager.customers.Count - 1].CurrentOrder.grilled = true;
        }
    }
    public class SpreadButter : cookingMethod
    {
        public static bool spreadbutter = false;
        public override void Activate(GameObject butterfake)
        {
            GameObject clone = Instantiate(butter, butter.transform.position, butter.transform.rotation);
            StartCoroutine(anim.ActivateSpread(clone.GetComponent<Animator>()));
            clone.transform.DOMove(dish.transform.position, 0.5f);
            clone.transform.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color32(255, 255, 0, 255), 0.5f).SetDelay(0.5f);
            clone.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetDelay(0.5f);
            clone.transform.DOScale(new Vector3(1.8f, 0.5f, 1f), 0.5f).SetDelay(1f);
            spreadbutter = true;
            clone.tag = "ServedFood";
        }
    }
}





