using UnityEngine;

public class PreventDoubleClickRaycast : MonoBehaviour
{
 public static bool isClicked = false;
    public float cooldownTime ;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                    isClicked = true;
                    Debug.Log("Sprite clicked!");
                    DoSomething();
                    Invoke(nameof(ResetClick), cooldownTime);

            }
        }
    }

    void DoSomething()
    {
        // your action here
    }

    void ResetClick()
    {
        isClicked = false;
    }
}
