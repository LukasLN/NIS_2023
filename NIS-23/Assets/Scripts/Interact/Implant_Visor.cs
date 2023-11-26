using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Implant_Visor : MonoBehaviour, IInteractable
{
    bool HasBeenInteractedWith = false;
    public void Interact() 
    {
        //interact
        //play stuff or whatever
        Debug.Log("Object is doing stuff");
        StartCoroutine(DestroyObject());
    }
    public void Update()
    {

    }


    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);

    }
}
