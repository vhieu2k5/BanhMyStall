using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Order
{
    public List<fillings> RequestedOrder = new List<fillings>();
    public float TimeLimit;
    public bool grilled = false;
    public int cost = 0;
}


