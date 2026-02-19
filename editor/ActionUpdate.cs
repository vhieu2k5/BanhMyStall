using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class ActionUpdate : Editor
{
    public static CustomerManager customerManager;
   // [MenuItem("MyTools/Add Customer")]
    [MenuItem("MyTools/Integerated UI")]
    [MenuItem("MyTools/Layered UI")]
  public static void AddCustomer()
     {
        // Gamemanager.Instance.LogCaller();
        customerManager = GameObject.Find("ScriptObject").GetComponent<CustomerManager>();
        customerManager.SpawnCustomer();
    }


}
