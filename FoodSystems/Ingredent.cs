using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum IngredentType { Bread, Pate, Sausage, Egg, Tomatoes, Herb, Pickle, Chili, Ketchup, Mayo, Mustard,Butter,Meat }
[CreateAssetMenu(menuName = "Ingredient")]
public class fillings: MonoBehaviour
{
    public string NameOfFood;
    public IngredentType typeOfFood;
    public float HealingTime;
    public float CookingTime;
    public bool NeedCooking;
    public int price;
    public override bool Equals(object obj)
    {
        if (obj is fillings other)
        {
            return this.NameOfFood == other.NameOfFood && this.typeOfFood == other.typeOfFood;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return (NameOfFood, typeOfFood).GetHashCode();
    }

}
public class Banhmy
{
    static public List<fillings> fill = new List<fillings>();
    public void Print()
    {
        Debug.Log("Banh my dang bao gom: ");
        int stt = 0;
        foreach (fillings f in fill)
        {
            Debug.Log(stt++ + ", " + f.typeOfFood);
        }
    }
    static public void AddingFilling(fillings topping)
    {
        fill.Add(topping);
    }
  

}
public abstract class cookingMethod : MonoBehaviour
{
    // public string name;
    public static bool addBread=false;
    public abstract void Activate(GameObject food);

}



