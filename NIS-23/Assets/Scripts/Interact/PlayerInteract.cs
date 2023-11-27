using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Diagnostics;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 2f;
    [SerializeField] float detectionRange = 4f;

    bool hasClickedRecently = false;
    bool WaitTimeHasPassed = false;
    Transform InteractablePos;
    [SerializeField] Transform Cam;
    Transform lookDirectionHolder;

    private void Start()
    {
        InteractablePos = this.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                //Debug.Log(collider.gameObject.name);
                //do stuff
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                    Debug.Log("item " + interactable);
                    //Vector3 tempPos = interactable.transform.position;
                    InteractablePos = interactable.transform;
                    //InteractablePos.position = (interactable as MonoBehaviour).transform.position;
                    Debug.Log("position " + InteractablePos.position);

                    if (hasClickedRecently == false)
                    {
                        this.GetComponent<FPSController>().canMove = false;
                        StartCoroutine(HoldPlayerMovement());
                        lookDirectionHolder = this.transform;
                        hasClickedRecently = true;
                    }

                }

            }
        }
        if (hasClickedRecently == true)
        {
            //this.GetComponent<FPSController>().enabled = !enabled;
            if (WaitTimeHasPassed == false)
            {
                Cam.transform.rotation = Quaternion.Slerp(Cam.transform.rotation, InteractablePos.rotation, 2 * Time.deltaTime);
                Debug.Log(InteractablePos.position);
            }
            else
            {
                Cam.transform.rotation = Quaternion.Slerp(Cam.transform.rotation, lookDirectionHolder.rotation, 2 * Time.deltaTime);
            }           

        }
    }

    /*public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableObjectsList = new List<IInteractable>();

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactableObject))
            {
                interactableObjectsList.Add(interactableObject);
            }
        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableObjectsList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {           // means if its closer
                if (Vector3.Distance(transform.position, interactable.transform.position) <
                Vector3.Distance(transform.position, closestInteractable.transform.position))
                {
                    closestInteractable = interactable;
                    terminalToMainHall.position = closestInteractable.transform.position;
                    Debug.Log("position " + closestInteractable.transform.position);
                }
            }
        }
        return closestInteractable;
    }*/

    IEnumerator HoldPlayerMovement()
    {
        yield return new WaitForSeconds(2);
        //Cam.transform.rotation = lookDirectionHolder.rotation;
        WaitTimeHasPassed = true;
        yield return new WaitForSeconds(1);
        this.GetComponent<FPSController>().canMove = true;
        hasClickedRecently = false;
        WaitTimeHasPassed = false;
    }
}
