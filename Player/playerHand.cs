using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject ketchupBottle;
    public GameObject mayoBottle;
    private static GameObject selectedObject;
    private Rigidbody2D rid;
    public Texture2D handCursor;
    public float dragSpeed = 2f;
    private Vector3 offset;
    private bool draggingObject = false;
    private bool draggingScreen = false;
    public GameObject cookingScreen;
    public GameObject sellingScreen;
    public GameObject packageScreen;
    private GameObject dish;

    private static Vector3 dragOrigin;
    private Method.SpreadButter spreadButter;
    [SerializeField] PickandChoose pickandChoose;
    private Method.SpreadSouce spreadSouce;
    private Method.GrillPan grillPan;
    public static bool doubleClick = false;
    public float doubleClickTime = 0.3f; // Max time between clicks
    private float lastClickTime = -1f;
    void Start()
    {
        // pickandChoose = gameObject.AddComponent<PickandChoose>();
        spreadButter = gameObject.AddComponent<Method.SpreadButter>();
        spreadSouce = gameObject.AddComponent<Method.SpreadSouce>();
        grillPan = gameObject.AddComponent<Method.GrillPan>();
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
        dish = GameObject.Find("cutting board");
    }
    void DraggingScreen()
    {
        //  Gamemanager.Instance.LogCaller();
        if (Input.GetMouseButtonDown(0) && !draggingObject)
        {
            dragOrigin = Input.mousePosition;
            draggingScreen = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            draggingScreen = false;
        }
    }
    void DraggingObject()
    {
        // Gamemanager.Instance.LogCaller();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 10f;
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit != null)
            {
                if (hit.gameObject.name == "spicy souce" || hit.gameObject.name == "mayonaise") dragOrigin = hit.gameObject.transform.position;
                if (hit.gameObject.CompareTag("PickandChoose"))
                {
                    draggingObject = true;
                    rid = hit.GetComponent<Rigidbody2D>();
                    // rid.bodyType = RigidbodyType2D.Dynamic
                    //  Debug.Log(hit.name);
                    selectedObject = hit.gameObject;
                    offset = selectedObject.transform.position - mouseWorldPos;
                    hit.GetComponent<SpriteRenderer>().sortingOrder = 40;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject != null && (selectedObject.gameObject.name == "spicy souce" || selectedObject.gameObject.name == "mayonaise"))
            {
                selectedObject.gameObject.transform.DOMove(new Vector3(dragOrigin.x, dragOrigin.y, 10), 0.5f);
                selectedObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            selectedObject = null;
            draggingObject = false;
        }

        if (Input.GetMouseButton(0) && selectedObject != null)
        {
            selectedObject.transform.position = mouseWorldPos + offset;
            selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, 20);
        }
    }
    void OnMouseEnter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name != "trashbin" && hit.collider.gameObject.name != "crunchy" && hit.collider.gameObject.CompareTag("ingredients") && !Banhmy.fill.Contains(hit.collider.gameObject.GetComponent<fillings>()))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    pickandChoose.Activate(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.name == "trashbin" && Banhmy.fill.Count > 0)
                {
                    PlayerAction.Throw();
                }
                else if (hit.collider.gameObject.name == "crunchy" && !CustomerManager.customers[CustomerManager.customers.Count - 1].CurrentOrder.grilled && !Method.GrillPan.isPanGrilling)
                {
                    grillPan.Activate(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.name == "butter" && !Method.SpreadButter.spreadbutter && cookingMethod.addBread)
                {
                    spreadButter.Activate(hit.collider.gameObject);
                }
                //Button hit
                else if (hit.collider.gameObject.name == "replay button" || hit.collider.gameObject.name == "replay button pause menu")
                {
                    StartCoroutine(anim.ActivateTransition(Gamemanager.Instance.transition.GetComponent<Animator>(), 0.5f));
                    StartCoroutine(Gamemanager.Instance.ReplayLevel());
                }
                else if (hit.collider.gameObject.name == "play button")
                {
                    if (!Gamemanager.isPause)
                    {
                        Gamemanager.Instance.RealStart();
                    }

                }
                else if (hit.collider.gameObject.name == "Day 1 button" && !tutorial.isPlaying)
                {
                    StartCoroutine(anim.ActivateTransition(Gamemanager.Instance.transition.GetComponent<Animator>(), 1f));
                    StartCoroutine(Gamemanager.Instance.StartNewDay());
                }
                else if (hit.collider.gameObject.name == "next button" || hit.collider.gameObject.name == "menu button")
                {
                    StartCoroutine(anim.ActivateTransition(Gamemanager.Instance.transition.GetComponent<Animator>(), 1f));
                    StartCoroutine(Gamemanager.Instance.ReplayLevel());
                }
                // else if (hit.collider.gameObject.name == "pause button")
                // {
                //     Gamemanager.Instance.Pause();
                // }
                else if (hit.collider.gameObject.name == "close button" || hit.collider.gameObject.name == "continue button")
                {
                    Gamemanager.Instance.ContinuePlay();
                }
                else if (hit.collider.gameObject.name == "settingbg")
                {
                    Gamemanager.Instance.Exit();
                }
                else if (hit.collider.gameObject.name == "sound button")
                {
                    Gamemanager.Instance.TurnSound();
                }
                else if (hit.collider.gameObject.name == "music button")
                {
                    Gamemanager.Instance.TurnBGMusic();
                }
                else if (hit.collider.gameObject.name == "setting button" && !tutorial.isPlaying)
                {
                    if (!Gamemanager.isPause)
                        Gamemanager.Instance.SettingPlay();
                }
                    
            }
            else
            {
                Debug.Log("Double Click Detected but nothing was hit");
            }
            doubleClick = false;
      //  }

        if (Bread.isContact && (!Method.SpreadSouce.spreadedspicy || !Method.SpreadSouce.spreadedmayo))
        {
            if (Bread.nameofthis == "spicy souce" && !Method.SpreadSouce.spreadedspicy) StartCoroutine(SpreadWithDelay(0.7f, ketchupBottle));
                else if (Bread.nameofthis == "mayonaise" && !Method.SpreadSouce.spreadedmayo) StartCoroutine(SpreadWithDelay(0.7f, mayoBottle)); // Prevent multiple activations
        } 
        
        }
         
    }
    void Update()
    {
        DraggingObject();
        OnMouseEnter();
        //  DraggingScreen();
        //Check double click
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickTime)
            {
                doubleClick = true;
            }
            lastClickTime = Time.time;
        }

        if (Bread.isContact && (!Method.SpreadSouce.spreadedspicy || !Method.SpreadSouce.spreadedmayo))
        {
            if (Bread.nameofthis == "spicy souce" && !Method.SpreadSouce.spreadedspicy) StartCoroutine(SpreadWithDelay(1f, ketchupBottle));
            else if (Bread.nameofthis == "mayonaise" && !Method.SpreadSouce.spreadedmayo) StartCoroutine(SpreadWithDelay(1f, mayoBottle)); // Prevent multiple activations
        } 

    }
    IEnumerator SpreadWithDelay(float delay, GameObject souce)
    {
        
        yield return new WaitForSeconds(delay);
        spreadSouce.Activate(souce);
        if (souce.gameObject.name == "SpreadKetchup") Method.SpreadSouce.spreadedspicy = true;
        else if (souce.gameObject.name == "SpreadMayo") Method.SpreadSouce.spreadedmayo = true;
    }

}
