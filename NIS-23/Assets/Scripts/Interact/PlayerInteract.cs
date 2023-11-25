using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 2f;
    [SerializeField] float detectionRange = 4f;

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
                }

            }
        }

    }

    public IInteractable GetInteractableObject()
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
                }
            }
        }
        return closestInteractable;
    }
}
