using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using JetBrains.Annotations;
using System.Linq;
using TMPro;
public class CustomerAction : MonoBehaviour
{
    public GameObject customerChatBox;
    GameObject destinationForCustomer;
    void Start()
    {
        customerChatBox = GameObject.Find("CustomerChatBox");
        customerChatBox.transform.localScale = new Vector3(0, 0, 0);
        destinationForCustomer = GameObject.Find("destinationforCustomer");
        Appear();
    }
    public void Appear()
    {
        if (destinationForCustomer != null)
        this.transform.DOMove(new Vector3(-4f, destinationForCustomer.gameObject.transform.position.y - 1f, destinationForCustomer.gameObject.transform.position.z), 0.5f);
        else Debug.Log("Destination for customer not found");
        StartCoroutine(ScriptOut(0f));
        String order = "";
        for (int i = 1; i < CustomerManager.customers[CustomerManager.customers.Count() - 1].CurrentOrder.RequestedOrder.Count; i++)
        {
            order += CustomerManager.customers[CustomerManager.customers.Count() - 1].CurrentOrder.RequestedOrder[i].NameOfFood + ", ";
        }
        //Cho tôi lấy đơn 1 chiếc bánh mỳ trứng, xúc xích,..
        //Cho bác một bánh mỳ mayonaise, tương ớt nhé!
        //Làm cho mình một bánh mỳ trứng, xúc xích nha!
        Scripts.OpenCustomerChat(CustomerManager.customers[CustomerManager.customers.Count() - 1].orderScript);
    }
    IEnumerator ScriptOut(float waitTime)
    {
        SoundEffect.Instance.AppearEffect();
        customerChatBox = GameObject.Find("CustomerChatBox");
        yield return new WaitForSeconds(waitTime);
        customerChatBox.transform.DOScale(new Vector3(0.04f, 0.04f, 1f), 0.5f);
    }
    IEnumerator ScriptIn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        customerChatBox.transform.DOScale(new Vector3(0, 0, 0), 0.2f);
    }
    IEnumerator moneyGiving(float timer)
    {
        anim.Shaking(GameObject.Find("box tiền"));
        GameObject.Find("moneytext").GetComponent<TextMeshProUGUI>().color = Color.green;
        yield return new WaitForSeconds(timer);
        GameObject clone = Instantiate(GameObject.Find("money"), GameObject.Find("Serve Button").transform.position, Quaternion.identity);
        clone.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        clone.transform.DOMove(GameObject.Find("cashier").transform.position, 1f);
        clone.transform.DOScale(new Vector3(0, 0, 0), 0.3f).SetDelay(2f);
        Destroy(clone, 3f);
        GameObject.Find("moneytext").GetComponent<TextMeshProUGUI>().color = new Color32(146,49,18,255);
    }
    IEnumerator Wrapping()
    {
        GameObject wrapper = GameObject.Find("wrapper");
        GameObject clone = Instantiate(wrapper, wrapper.transform.position, Quaternion.identity);
        StartCoroutine(anim.ActivateTransition(clone.GetComponent<Animator>(),5f));
        clone.tag = "ServedFood";
        yield return new WaitForSeconds(10f);
        Destroy(clone,6f);
    }
    IEnumerator FoodToCustomers()
    {
        GameObject[] madeFood = GameObject.FindGameObjectsWithTag("ServedFood");
        foreach (GameObject food in madeFood)
        {

            if (food.name != "pan(Clone)")
            {
                food.transform.rotation = Quaternion.Euler(0, 0, -90);
                food.transform.DOMove(GameObject.Find("wrapper(Clone)").transform.position, 0.5f);
            }
            else
            {
                food.transform.rotation = Quaternion.Euler(0, 0, 70);
                food.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
                food.transform.DOMove(GameObject.Find("wrapper(Clone)").transform.position + new Vector3(1f, 0, 0), 0.5f);
            }
        }
        yield return new WaitForSeconds(1f);
        foreach (GameObject food in madeFood)
        {
            food.transform.DOMove(gameObject.transform.position + new Vector3(-1f,-0.5f,0f), 0.5f);
            food.transform.DOScale(new Vector3(0.6f, 0.6f, 0), 1f);
            food.transform.DORotate(new Vector3(0, 0, 90), 0.7f, RotateMode.LocalAxisAdd);
            food.transform.DOScale(new Vector3(0f, 0f, 0), 0.5f).SetDelay(1.7f);
            Destroy(food, 3f);
        }
        
    }
    public void Buy()
    {
        StartCoroutine(moneyGiving(0f));
        SoundEffect.Instance.BuyEffect();
        StartCoroutine(Wrapping());
        StartCoroutine(FoodToCustomers());
        Method.Reset();
        StartCoroutine(Wait(4f));
        Banhmy.fill.Clear();
        Method.SpreadSouce.Deactivate(GameObject.Find("SpreadKetchup(Clone)"));
    }
    public void Disappear()
    {
        gameObject.transform.DOMoveX(10f, 0.5f);
        //  CustomerManager.customers.RemoveAt(GetComponent<CustomerManager.Customer>().id - 1);
        Destroy(gameObject, 0.5f);
        StartCoroutine(ScriptIn(0.1f));
        Scripts.CloseCustomerChat(); 
        if (GameObject.Find("ScriptObject") != null) GameObject.Find("ScriptObject").GetComponent<CustomerManager>().SpawnCustomer();
        else Debug.Log("NULLLLLL");
    }
    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Disappear();
    }
   
}
