using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Storage : MonoBehaviour
{
    [SerializeField] GameObject Door;
    [SerializeField] GameObject Player;


    public void Update()
    {
        if (Player.GetComponent<FPSController>().hasPickedUpHelmet == true)
        {
            if (Door.transform.localPosition.y >=  -2.5)
            {
                Door.transform.localPosition -= new Vector3(0, 1 * Time.deltaTime, 0);
            }
        }
    }
}
