using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightblink : MonoBehaviour
{
    [SerializeField] GameObject Light;
    [SerializeField] GameObject LightBulb;
    [SerializeField] Material LightMAT;
    [SerializeField] Material NoLightMAT;
    Renderer MATRenderer;
    float startTime = 2;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        MATRenderer = LightBulb.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0)
        {
            Light.SetActive(false);
            MATRenderer.material = NoLightMAT;
            StartCoroutine(Blink());
            startTime += Random.Range(5f, 7f);
            currentTime = startTime;
        }
        currentTime -= Time.deltaTime;
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(0.1f);
        Light.SetActive(true);
        MATRenderer.material = LightMAT;
        yield return new WaitForSeconds(0.2f);
        Light.SetActive(false);
        MATRenderer.material = NoLightMAT;
        yield return new WaitForSeconds(1f);
        Light.SetActive(true);
        MATRenderer.material = LightMAT;
    }
}
