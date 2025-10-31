using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class anim : MonoBehaviour
{
    public static IEnumerator ActivateCutting(Animator animator)
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetInteger("cut", 1);
        yield return new WaitForSeconds(0.3f);
        animator.SetInteger("cut", -1);
    }
    public static IEnumerator ActivateDropEgg(Animator animator)
    {
        yield return new WaitForSeconds(0f);
        animator.SetBool("drop", true);
        Debug.Log("set bool drop thanh cong");
        yield return new WaitForSeconds(4f);
    }
    public static IEnumerator ActivateGriller(Animator animator)
    {
        yield return new WaitForSeconds(1.8f);
        animator.SetFloat("grilling", -1);
        yield return new WaitForSeconds(4.5f);
        animator.SetFloat("grilling", 1);
    }
    public static IEnumerator ActivateGasFire(Animator animator)
    {
        animator.SetInteger("fire", -1);
        yield return new WaitForSeconds(0.5f);
        animator.SetInteger("fire", 1);
        yield return new WaitForSeconds(4f);
    }
    public static IEnumerator ActivateSpread(Animator animator)
    {
        animator.SetBool("activate", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("activate", false);
    }
    public static IEnumerator ActivateTransition(Animator animator, float timer)
    {
        animator.SetBool("activate", true);
        yield return new WaitForSeconds(timer);
        animator.SetBool("activate", false);
    }
    public static void Shaking(GameObject gameObject)
    {
        gameObject.transform.DOShakePosition(0.2f, strength: new Vector3(0.02f, 0.02f, 0), vibrato: 10, randomness: 90, snapping: false, fadeOut: true);
    }

}
