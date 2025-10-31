using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
  public static CusAttitudeManager cusAttitudeManager;
  public static GameObject effectAngry;
  public class Customer
  {
    public int id;
    public Order CurrentOrder = new Order();
    public int price;
    public float waitingTime;
    public bool served = false;
    public GameObject body;
    public Sprite sad;
    public Sprite neutral;
    public Sprite talk;
    public Sprite angry;
    public string orderScript;
    public string rightScript;
    public string wrongScript;

    public void GetAttitude()
    {
      cusAttitudeManager = GameObject.Find("ScriptObject").GetComponent<CusAttitudeManager>();
      Debug.Log("num of Cus: " + Scripts.numOfCus);
      sad = cusAttitudeManager.SetCustomerMood(Scripts.numOfCus, CustomerMood.Sad);
      talk = cusAttitudeManager.SetCustomerMood(Scripts.numOfCus, CustomerMood.Talk);
      angry = cusAttitudeManager.SetCustomerMood(Scripts.numOfCus, CustomerMood.Angry);
      neutral = cusAttitudeManager.SetCustomerMood(Scripts.numOfCus, CustomerMood.Neutral);
      effectAngry = GameObject.Find("effectAngry");
    }

    public Customer(int stt, Order order, int price, float waitingTime)
    {
      this.id = stt;  
      this.CurrentOrder = order;
      this.price = price;
      this.waitingTime = waitingTime;

      String s = "";
        foreach (fillings f in this.CurrentOrder.RequestedOrder)
        {
          s += f.NameOfFood + ", ";
        }

      if (id == 1)
      {
        orderScript = "CHO TÔI LẤY ĐƠN MỘT CHIẾC " + s.TrimEnd(',', ' ');
        rightScript = "MÌNH NHẬN ĐƠN NHÉ!";
        wrongScript = "ĐƠN CỦA TÔI LÀ " + s.TrimEnd(',', ' ') +" MÀ!";
      }
      else if (id == 2)
      {
        orderScript = "CHO BÁC MỘT  " + s.TrimEnd(',', ' ');
        rightScript = "CẢM ƠN CHÁU NHÉ!";
        wrongScript = "BÁC GỌI " + s.TrimEnd(',', ' ') + " MÀ CHÁU!";
      }
      else if (id == 3)
      {
        orderScript = "LÀM CHO MÌNH MỘT " + s.TrimEnd(',', ' ');
        rightScript = "MÌNH XIN NHA!";
        wrongScript = "MÌNH GỌI " + s.TrimEnd(',', ' ') + " MÀ!";
      }


    }
    public void GetAppearance(Sprite appearance)
    {
      customers.Last().body.GetComponent<SpriteRenderer>().sprite = appearance;
    }

    public IEnumerator PatienceCoroutine()
    {
      GameObject.Find("patience").GetComponent<TextMeshProUGUI>().color = new Color32(146,49,18,255);
      effectAngry.GetComponent<SpriteRenderer>().enabled = false;
      while (waitingTime > 0 && !served)
      {
        yield return new WaitForSeconds(2f);

        if (customers.Count > 0 && customers.Last().waitingTime <= 50 && customers.Last().waitingTime >= 20)
        {
          customers.Last().GetAppearance(customers.Last().neutral);

        }
        else if (customers.Count > 0 && customers.Last().waitingTime <= 20 && customers.Last().waitingTime >= 0)
        {
          anim.Shaking(GameObject.Find("box độ hài lòng"));
          anim.Shaking(GameObject.Find("CustomerChatBox"));
          GameObject.Find("patience").GetComponent<TextMeshProUGUI>().color = Color.red;
          SoundEffect.Instance.WaitEffect();
          customers.Last().GetAppearance(customers.Last().angry);
          effectAngry.GetComponent<SpriteRenderer>().enabled = true;
        }
        waitingTime -= 2;
      }
      if (!served)
      {
        Debug.Log("Customer " + id + " has run out of patience and is leaving.");
        body.GetComponent<CustomerAction>().Disappear();
        customers.Remove(this);
      }

    }
  }

    public static GameObject customerPrefab;
    static public List<Customer> customers = new List<Customer>();
    private static int stt = 0;
    public static GameObject instatiatePlace;
  public void SpawnCustomer()
  {
    Scripts.numOfCus++;
    Order tempOrder = OrderManager.SpawnOrder();
    if (stt > 3) stt = 0;
    Customer customer1 = new Customer(++stt, tempOrder, tempOrder.cost, 60f);
    customerPrefab = GameObject.Find("cus");
    instatiatePlace = GameObject.Find("cus");
    customer1.body = Instantiate(customerPrefab, instatiatePlace.transform.position, Quaternion.identity);
    customer1.body.gameObject.transform.localScale = new Vector3(1, 1, 1);
    customers.Add(customer1);
    ShowOrder(customer1);
    customer1.body.AddComponent<CustomerAction>();
    customer1.GetAttitude();
    customer1.GetAppearance(customer1.talk);
    StartCoroutine(customer1.PatienceCoroutine());
    }
    public static void ShowOrder(Customer customer)
    {
        Debug.Log("Order of customer " + customer.id);
      foreach (fillings f in customer.CurrentOrder.RequestedOrder)
      {
        Debug.Log("We need " + f.NameOfFood);
      }
    }

  IEnumerator CheckOut(Customer customer)
  {
    if (customer == null)
      {
        Debug.LogError("Customer is null when trying to check out!");
        yield break;
      }
    if (Method.GrillPan.isPanGrilling) yield break;
    if (OrderManager.Instance.CheckOrder(customer.CurrentOrder, Banhmy.fill))
    {
      SoundEffect.Instance.TrueEffect();
      customers.Last().GetAppearance(customers.Last().talk);
      Debug.Log("Customer " + customer.id + " is served successfully!");
      Scripts.OpenCustomerChat(customer.rightScript);
      //Mình nhận đơn nhé!
      //Cảm ơn cháu nhé!
      //Mình xin nha!
      customer.served = true;
      customer.price = 10 + customer.CurrentOrder.cost;
      customer.body.GetComponent<CustomerAction>().Buy();
      Debug.Log("Order MONEY: " + customer.price);
      Gamemanager.AddMoney(customer.price);
      customers.Remove(customer);
    }
    else
    {
      SoundEffect.Instance.FalseEffect();
      customers.Last().GetAppearance(customers.Last().angry);
      PlayerAction.Throw();
      yield return new WaitForSeconds(2f);
      Scripts.OpenCustomerChat(customer.wrongScript);
      //Đơn của tôi là bánh mỳ .. mà!
      //Của bác là bánh mỳ ... mà cháu!
      //Mình gọi bm ... mà!
    }
     yield return null;
    }
    public void Submit()
    {
      Debug.Log("Submitted!");
      Gamemanager.Instance.DeactivateButtonsTemporarily(1f);
      if (customers.Count == 0)
    {
      Debug.Log("No customers to submit orders for.");
      return;
    }
    else
    {

      StartCoroutine(CheckOut(customers.Last()));
      //customers.Remove(customers.First());
    }

    }
    void Update()
    {
      if (customers.Count > 0 && customers[0].waitingTime <= 0 && !customers[0].served)
      {
        Debug.Log("Customer " + customers[0].id + " has run out of patience and is leaving.");
        // customers[0].body.GetComponent<CustomerAction>().Buy();
        // customers.RemoveAt(0);
      }
    }
    public IEnumerator Patience(Customer customer)
    {
      if (customer == null)
      {
        Debug.LogError("Customer is null before starting patience coroutine!");
        yield break;
      }
      while (customer.waitingTime > 0)
      {
        yield return new WaitForSeconds(1f);
        customer.waitingTime--;
      }

    }

  
}
