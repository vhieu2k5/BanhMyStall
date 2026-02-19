using System;
class Cooking
{
    static void Main()
    {
        Console.WriteLine("Hello");
    }
}
public abstract class Ingredient
{
    public string NameOfIngredient;
    public float cookingTime;
    public float defaultHealingTime = 5f;
    public void Picking()
    {
        Console.WriteLine($"Picking {NameOfIngredient}...");
    }
}
public class GameManager
{
    public static GameManager Instance { get; private set; }
    public float MoneyEarnedToday { get; private set; }
    
    public void AddMoney(float moneyEarned)
    {
        MoneyEarnedToday += moneyEarned;
    }
}
