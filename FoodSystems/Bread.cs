using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Bread : MonoBehaviour
{
    static public bool isContact = false;
    static public string nameofthis;
 
   private void OnTriggerEnter2D(Collider2D other)
    {
        nameofthis = gameObject.name;
       // Debug.Log(gameObject.name + " collided with " + other.name);
        if (other.name == "pan(Clone)")
        {
            SoundEffect.Instance.SqueezeEffect();
            Debug.Log("is Contact true");
            isContact = true;
        }
        else isContact = false;


    }
}
