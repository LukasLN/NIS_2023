using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Implant_Visor : MonoBehaviour, IInteractable
{
    bool HasBeenInteractedWith = false;
    [SerializeField] GameObject Player;
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
        Player.GetComponent<FPSController>().playAudio();
        Player.GetComponent<FPSController>().hasPickedUpHelmet = true;
        Destroy(this.gameObject);

    }
}
