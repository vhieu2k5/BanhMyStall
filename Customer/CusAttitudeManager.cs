using System.Collections.Generic;
using UnityEngine;

public enum CustomerMood { Sad, Neutral, Talk, Angry }

public class CusAttitudeManager : MonoBehaviour
{
    // The SpriteRenderer for each customer in the scene
    public  SpriteRenderer customer1Renderer;
    public SpriteRenderer customer2Renderer;
    public  SpriteRenderer customer3Renderer;
    public  List<Sprite> customerEmotion;


    private static Dictionary<int, Dictionary<CustomerMood, Sprite>> customerSprites;

    void Start()
    {
        customerSprites = new Dictionary<int, Dictionary<CustomerMood, Sprite>>
        {
            {
                1, new Dictionary<CustomerMood, Sprite>
                {
                    { CustomerMood.Sad, Resources.Load<Sprite>("char/char1/Customer1_Sad") },
                    { CustomerMood.Neutral, Resources.Load<Sprite>("char/char1/Customer1_Neutral") },
                    { CustomerMood.Talk, Resources.Load<Sprite>("char/char1/Customer1_Talk") },
                    { CustomerMood.Angry, Resources.Load<Sprite>("char/char1/Customer1_Angry") }
                }
            },
            {
                2, new Dictionary<CustomerMood, Sprite>
                {
                    { CustomerMood.Sad, Resources.Load<Sprite>("char/char2/Customer2_Sad") },
                    { CustomerMood.Neutral, Resources.Load<Sprite>("char/char2/Customer2_Neutral") },
                    { CustomerMood.Talk, Resources.Load<Sprite>("char/char2/Customer2_Talk") },
                    { CustomerMood.Angry, Resources.Load<Sprite>("char/char2/Customer2_Angry") }
                }
            },
            {
                3, new Dictionary<CustomerMood, Sprite>
                {
                    { CustomerMood.Sad, Resources.Load<Sprite>("char/char3/Customer3_Sad") },
                    { CustomerMood.Neutral, Resources.Load<Sprite>("char/char3/Customer3_Neutral") },
                    { CustomerMood.Talk, Resources.Load<Sprite>("char/char3/Customer3_Talk") },
                    { CustomerMood.Angry, Resources.Load<Sprite>("char/char3/Customer3_Angry") }
                }
            }
        };
    }

    public Sprite SetCustomerMood(int customerNumber, CustomerMood mood)
    {
 
        if (customerSprites.TryGetValue(customerNumber, out var moods))
        {
            if (moods.TryGetValue(mood, out Sprite sprite))
            {
                switch (customerNumber)
                {
                    case 1:
                        customer1Renderer.sprite = sprite;
                        return sprite;
                    case 2:
                        customer2Renderer.sprite = sprite;
                        return customer2Renderer.sprite;
                    case 3:
                        customer3Renderer.sprite = sprite;
                        return customer3Renderer.sprite;
                }
                return null;
            }
            else return null;
        }
        else return null;
    }
}
