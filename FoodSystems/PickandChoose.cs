using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using System.IO;
using Unity.VisualScripting;

public class PickandChoose : cookingMethod
{
    static public int layer = 1;
    public static Animator animate;
    [SerializeField] Grill grill;
    public override void Activate(GameObject food)
    {
        SoundEffect.Instance.ChooseEffect();
        GameObject clone;
        if (food.gameObject.name == "pan" && !addBread)
        {
            addBread = true;
            layer = 1;
            clone = Instantiate(food, food.transform.position, Quaternion.identity);
            clone.GetComponent<PolygonCollider2D>().enabled = false;
            clone.GetComponent<BoxCollider2D>().enabled = true;
            animate = clone.GetComponent<Animator>();
            StartCoroutine(anim.ActivateCutting(animate));
        }
        else if (addBread && (food.gameObject.name == "Nom" || food.gameObject.name == "egg") && !Grill.grilling && !Method.GrillPan.isPanGrilling && Method.taken)
        {
            grill.Activate(food);
            return;
            
        }
        else if (addBread && food.gameObject.name != "Nom" && food.gameObject.name != "egg" && !Method.GrillPan.isPanGrilling)
        {
            clone = Instantiate(food.gameObject, food.transform.position, Quaternion.identity);
           // Debug.Log("Da tao ra " + clone.name);
        }
        else
        {
            Debug.Log(addBread);
            return;
        }

        if (!Grill.grilling && (food.gameObject.name == "grilledSausage(Clone)" || food.gameObject.name == "grillegg(Clone)"))
        {
            if (food.gameObject.name == "grilledSausage(Clone)") GameObject.Find("grilledSausage(Clone)(Clone)").GetComponent<Animator>().SetBool("grilled", true);
            else GameObject.Find("grillegg(Clone)(Clone)").GetComponent<Animator>().SetBool("drop", false);
            Destroy(food);
            Method.taken = true;
            
        }
        if (clone.transform.gameObject == null) return;
        // clone = Instantiate(food, food.transform.position, food.transform.rotation);
        clone.GetComponent<SpriteRenderer>().sortingOrder = layer++;
        Vector3 originalScale = clone.gameObject.transform.localScale;//Animation
        clone.gameObject.transform.localScale = new Vector3(0, 0, clone.transform.localScale.z);
        clone.gameObject.transform.DOScale(originalScale, 0.3f);
        //Animation Foods
        if (clone.gameObject.name != "pate(Clone)") clone.gameObject.transform.DOMove(Method.dish.transform.position, 0.5f);
        else clone.gameObject.transform.DOMove(Method.dish.transform.position, 0.5f).SetDelay(3f);
        if (clone.gameObject.name == "meat(Clone)")
        {
            clone.transform.rotation = Quaternion.Euler(0, 0, 90);
            clone.transform.DOScale(new Vector3(0.8f, 0.8f, 0), 0.5f);
            DuplicateFood(clone, originalScale.x);
        }
        if (clone.gameObject.name == "Tomatoes(Clone)" ||  clone.gameObject.name =="Pickle(Clone)")
        {
            DuplicateFood(clone, originalScale.x);
        }
        //Sau khi đã pick and choose xong 
        OrderManager.Instance.AddFillings(clone.GetComponent<fillings>());
        // Debug.Log("Đã chọn để đặt lên bàn");
        // if (OrderManager.currentBread != null)
        // {
        //     Banhmy.Print();
        // }
        //else Debug.Log("Current bread is null");
        Gamemanager.MinusMoney(clone.GetComponent<fillings>().price);
        if (clone.gameObject.name == "pate(Clone)")
        {
            StartCoroutine(anim.ActivateSpread(clone.GetComponent<Animator>()));
            clone.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color32(255, 255, 255, 255), 0.5f);
        }

        clone.gameObject.tag = "ServedFood"; //Để thức ăn không bị di chuyển và sản sinh nữa  
    }
    private void DuplicateFood(GameObject clone, float scale)
    {
        GameObject newclone = Instantiate(clone.gameObject, clone.transform.position, clone.transform.rotation);
            newclone.transform.DOScale(new Vector3(scale, 0.8f, 1), 0.5f);
            newclone.tag = "ServedFood";
            GameObject newclone2 = Instantiate(newclone.gameObject, newclone.transform.position,clone.transform.rotation);
            newclone2.transform.DOScale(new Vector3(scale, 0.8f, 1), 0.5f);
            newclone.transform.DOMove(Method.dish.transform.position + new Vector3(-0.5f, 0, 0), 0.5f);
            newclone2.transform.DOMove(Method.dish.transform.position + new Vector3(0.5f, 0, 0), 0.7f);
            newclone2.tag = "ServedFood";
    }
    }
