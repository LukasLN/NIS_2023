using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Storage : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Door;
    bool HasBeenInteractedWith = false;
    public void Interact() 
    {
        //interact
        //play stuff or whatever
        Debug.Log("Object is doing stuff");
        HasBeenInteractedWith = true;
        Destroy(this.GetComponent<BoxCollider>());
    }
    public void Update()
    {
        if (HasBeenInteractedWith == true)
        {
            if (Door.transform.localPosition.y >=  -2.5)
            {
                Door.transform.localPosition -= new Vector3(0, 1 * Time.deltaTime, 0);
            }
        }
    }
}
