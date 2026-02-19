using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAction : MonoBehaviour
{
    static public GameObject trashbin;
    void Start()
    {
        trashbin = GameObject.Find("trashbin");
    }
    static public void Throw()
    {
        GameObject[] madeFood = GameObject.FindGameObjectsWithTag("ServedFood");

        foreach (GameObject food in madeFood)
        {
            food.transform.DOMove(trashbin.transform.position, 0.5f);
            food.transform.DOScale(new Vector3(0, 0, 0), 1f);
            Destroy(food, 2f);
        }
       // Banhmy.fill.Clear();
        if (GameObject.Find("SpreadKetchup(Clone)") != null) Method.SpreadSouce.Deactivate(GameObject.Find("SpreadKetchup(Clone)"));
        Method.Reset();
    }
    static public void ThrowCookingFood()
    {
        GameObject[] cookingFood = GameObject.FindGameObjectsWithTag("CookingFood");

        foreach (GameObject food in cookingFood)
        {
            food.transform.DOMove(trashbin.transform.position, 0.5f);
            food.transform.DOScale(new Vector3(0, 0, 0), 1f);
            Gamemanager.MinusMoney(food.GetComponent<fillings>().price);
            Destroy(food, 2f);
        }
    }
}
