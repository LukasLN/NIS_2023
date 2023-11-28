using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void awake()
    {
        StartCoroutine(endGameTimer());
    }

    IEnumerator endGameTimer()
    {
        yield return new WaitForSeconds(6);
        Application.Quit();
    }
}
