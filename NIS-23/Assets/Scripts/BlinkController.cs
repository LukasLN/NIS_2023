using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour
{   
    public GameObject image;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !image.GetComponent<EyeController>().isClosed)
        {
            //close eyes animation()
            image.GetComponent<EyeController>().EyeAnim_Close();
            image.GetComponent<EyeController>().isClosed = true;
            Debug.Log("Closed");
        }

        else if (Input.GetMouseButtonUp(1) && image.GetComponent<EyeController>().isClosed)
        {
            //open eyes animation()
            image.GetComponent<EyeController>().EyeAnim_Open();
            image.GetComponent<EyeController>().isClosed = false;
            Debug.Log("Open");
        }
    }
}
