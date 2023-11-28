using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVison : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] AudioSource vission;
    bool isplaying = false;
    bool inRange = false;
    [SerializeField] float setTime;
    [SerializeField] bool iskeyCodeScene;
    [SerializeField] GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (image.GetComponent<EyeController>().isClosed == true && isplaying == false && inRange == true)
        {
            vission.Play();
            Debug.Log("Vission: Playing");
            StartCoroutine(waitTime());
            isplaying = true;
        }
        if (image.GetComponent<EyeController>().isClosed == false && isplaying == true)
        {
            vission.Stop();
            StopCoroutine(waitTime());
            Debug.Log("Vission: Stopped");
            isplaying = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(setTime);
        if (iskeyCodeScene == true)
        {
            Player.GetComponent<FPSController>().hasHeardCode = true;
        }
        isplaying = false;
    }
}
