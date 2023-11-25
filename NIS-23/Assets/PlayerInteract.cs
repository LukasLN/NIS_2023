using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                //Debug.Log(collider.gameObject.name);
                //do stuff
                if(collider.TryGetComponent(out interfaceInteract interactable)){
                    interactable.Interact();
                }

            }
        }

    }
    
    public ObjectInteract GetInteractableObject(){
        List<ObjectInteract> interactableObjectsList = new List<ObjectInteract>();

    }
}
