using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_MainHall : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Door;
    [SerializeField] AudioSource characterSound;
    [SerializeField] AudioSource DoorSound;
    [SerializeField] AudioSource KeypadSound;
    [SerializeField] GameObject Player;
    bool HasBeenInteractedWith = false;
    public void Interact() 
    {
        //interact
        //play stuff or whatever
        Debug.Log("Object is doing stuff");
        HasBeenInteractedWith = true;
        KeypadSound.Play();
        if(Player.GetComponent<FPSController>().hasHeardCode == true)
        {
            StartCoroutine(waitforCode());
        }
        StartCoroutine(HasBeenInteractedWithTimer());
        //characterSound.Play();
    }
    public void Update()
    {
        if (HasBeenInteractedWith == true && Player.GetComponent<FPSController>().hasHeardCode == true)
        {

            if (Door.transform.localPosition.y >=  -5.5)
            {
                Door.transform.localPosition -= new Vector3(0, 2 * Time.deltaTime, 0);
            }
        }
    }
    
    IEnumerator waitforCode()
    {
        yield return new WaitForSeconds(1.5f);
        DoorSound.Play();
    }

    IEnumerator HasBeenInteractedWithTimer()
    {
        yield return new WaitForSeconds(5f);
        HasBeenInteractedWith = false;
    }
}
