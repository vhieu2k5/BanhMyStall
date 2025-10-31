using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    static public List<fillings> availableIngredients = new List<fillings>();
    static  public Banhmy currentBread = new Banhmy();
    private bool isMakingPan = true;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void MakingFiller()
    {
        availableIngredients.Add(GameObject.Find("meat").GetComponent<fillings>());
        availableIngredients.Add(GameObject.Find("pate").GetComponent<fillings>());
        availableIngredients.Add(GameObject.Find("pan").GetComponent<fillings>());
        availableIngredients.Add(GameObject.Find("Pickle").GetComponent<fillings>());
        availableIngredients.Add(GameObject.Find("Tomatoes").GetComponent<fillings>());
        availableIngredients.Add(GameObject.Find("SpreadKetchup").GetComponent<fillings>());
        availableIngredients.Add(GameObject.Find("grillegg").GetComponent<fillings>());
        availableIngredients.Add(GameObject.Find("SpreadMayo").GetComponent<fillings>());
        availableIngredients.Add(GameObject.Find("grilledSausage").GetComponent<fillings>());
    }
    public static Order SpawnOrder()
    {
        //Gamemanager.Instance.LogCaller();
        Order order = new Order();
        order.RequestedOrder.Add(GameObject.Find("pan").GetComponent<fillings>()); //Bánh mì luôn có trong order
        int count = UnityEngine.Random.Range(1, 7);
      //  Debug.Log("Order will have " + count + " ingredients.");
      //  Debug.Log("Available ingredients: "+availableIngredients.Count);
        for (int i = 0; i < count; i++)
        {
            fillings ing = availableIngredients[UnityEngine.Random.Range(0, availableIngredients.Count)];
            if (!order.RequestedOrder.Contains(ing))
            {
                order.RequestedOrder.Add(ing);
                order.cost += ing.price+ 2;
               // Debug.Log("Added " + ing.NameOfFood + " to order.");
            }
            else i--; //Nếu đã có trong order thì random lại
        }

        order.TimeLimit = 30f;
        return order;
    }
    public bool CheckOrder(Order order, List<fillings> ingredients)
    {
        // Gamemanager.Instance.LogCaller();
        if (order.RequestedOrder.Count != ingredients.Count)
        {
            Scripts.OpenCustomerChat("TÔI KHÔNG CÓ GỌI CÁI NÀY!?!");
            Debug.Log("Order count does not match ingredient count. " + order.RequestedOrder.Count + " vs " + ingredients.Count);
            foreach (fillings item in order.RequestedOrder)
            {
                Debug.Log("item in order: " + item.NameOfFood);
            }
            return false;
        }
        // foreach (fillings item in ingredients)
        // {
        //     Debug.Log("item in ingredients: " + item.NameOfFood);
        // }
        // foreach (fillings item in order.RequestedOrder)
        // {
        //     Debug.Log("item in order: " + item.NameOfFood);
        // }
        foreach (fillings item in order.RequestedOrder)
        {
            if (!ingredients.Contains(item))
            {
                Scripts.OpenCustomerChat("ỦA " + item.NameOfFood + " ĐÂU??");
                Debug.Log("Missing ingredient: " + item.NameOfFood);
                return false;
            }

        }
        if (!CustomerManager.customers[CustomerManager.customers.Count - 1].CurrentOrder.grilled)
        {
            Scripts.OpenCustomerChat("SAO BÁNH MỲ LẠI NGUỘI VẬY?");
            return false;
        }
        return true;
    }
    public void AddFillings(fillings ingredient)
    {
      //  Gamemanager.Instance.LogCaller();
        if (isMakingPan)
        {
            if (!Banhmy.fill.Contains(ingredient))
            {
                Banhmy.AddingFilling(ingredient);
               // Debug.Log("Added " + ingredient.NameOfFood + " to current pan.");
            }
            } 
    }
       
    
}
