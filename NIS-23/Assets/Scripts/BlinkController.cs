using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour
{
    public GameObject image;
    [SerializeField] List<AudioSource> echoes;
    [SerializeField] List<AudioSource> ambience;
    [SerializeField] List<float> defaultVolumes;
    [SerializeField] AudioSource closestEcho;
    [SerializeField] float fadeDuration = 1.5f;
    [SerializeField] float echoVol;
    [SerializeField] float volumeScalingFactor = 0.1f;
    int currentClosest = 0;
    bool hasImplant;
    bool isPLayingVoiceOver;
    void Start()
    {
        if (closestEcho == null)
        {
            closestEcho = echoes[0];
        }
        
    }
    // Update is called once per frame
    void Update()
    {           //set closestEcho to actual closest
        

        //calculates the dist multiplied by our scaling factor

        float calc = volumeScalingFactor * Vector3.Distance(transform.position, closestEcho.transform.position);
        echoVol = 1f - (Mathf.Clamp(calc, 0.05f, 1));

        closestEcho.volume = echoVol;
        /*
        float dist = Vector3.Distance(transform.position, closestEcho.transform.position);
        Debug.Log("dist:"+dist);
        //normalize dist
        float normDist = (dist-0f)*1f/(1f-0f);
        Debug.Log("normDist"+normDist);

        echoVol = normDist;
        */

        Debug.Log("dist:" + Vector3.Distance(transform.position, closestEcho.transform.position));
        Debug.Log("volume:" + echoVol);

        if (Input.GetMouseButtonDown(1) && !image.GetComponent<EyeController>().isClosed /*&& hasImplant && notPlayingVoiceOver*/)
        {


            //close eyes animation()
            image.GetComponent<EyeController>().EyeAnim_Close();
            image.GetComponent<EyeController>().isClosed = true;
            //Debug.Log(isClosed);

            //Start coroutines fading in implant ecchoes, while fading out all other noices
            StartCoroutine(FadeAudioSource.StartFade(closestEcho, fadeDuration, echoVol));
            FadeAudList(ambience, 0f);
            closestEcho.Play();
            Debug.Log("closest:"+closestEcho);

        }

        else if (Input.GetMouseButtonUp(1) && image.GetComponent<EyeController>().isClosed /*&& hasImplant && notPlayingVoiceOver*/)
        {
            //open eyes animation()
            image.GetComponent<EyeController>().EyeAnim_Open();
            image.GetComponent<EyeController>().isClosed = false;
            //Debug.Log(isClosed);


            //Start coroutines fading out implant ecchoes, while fading in all other noices
            FadeAudList(echoes, 0f);
            RestoreAudVol();
        }



    }
    void FindClosestEcho()
    {   /* //Original method of updating closestEcho
        int i=0;
        foreach (AudioSource echo in echoes)
        {
            
            i++;
            //Debug.Log(i);
           if (closestEcho == null)
            {
                closestEcho = echo;
            }   //dist checker
            else if (Vector3.Distance(transform.position, echo.transform.position) <
                Vector3.Distance(transform.position, closestEcho.transform.position))
            {
                closestEcho = echo;

            }
        }
        */
        
    }
        //BS way, cuz dist checker above aint workin
    void NextEcho(){
        if(closestEcho=echoes[0]) {
            playVoiceOver(0);
            closestEcho=echoes[1];
        }
        else if(closestEcho=echoes[1]){
             playVoiceOver(1);
                closestEcho=echoes[2];
        }
        else if(closestEcho=echoes[2]){
            playVoiceOver(2);
        }
    }

    void playVoiceOver(int x){
        //play audiosource associated with the event, but based on echo[number]
        while (isPLayingVoiceOver){
            image.GetComponent<EyeController>().EyeAnim_Close();
            image.GetComponent<EyeController>().isClosed = true;
            
            //maybe use the fadeinsystem for playing voiceovers:
            //StartCoroutine(FadeAudioSource.StartFade("some list of audiosources"[x], voiceOverduration, echoVol));
        }
        //when voiceOverduration is over:
        isPLayingVoiceOver = false;

    }
    

    void FadeAudList(List<AudioSource> list, float volumeLevel)
    {
        foreach (AudioSource aud in list)
            //defaultVolumes.Add(aud.volume);
            StartCoroutine(FadeAudioSource.StartFade(aud, fadeDuration, volumeLevel));
    }

    void RestoreAudVol(/*List<AudioSource> list, List<float> defaultVolumes*/)
    {
        /*
        foreach (AudioSource aud in list){
            //foreach float in defaultvolumes, inser the element(float value)
            // at the index nr. of the aud into the aud.volume.

        }*/

        //redundant, hardcoded magic number way:

        StartCoroutine(FadeAudioSource.StartFade(ambience[0], fadeDuration, .49f));

        StartCoroutine(FadeAudioSource.StartFade(ambience[1], fadeDuration, .49f));

        StartCoroutine(FadeAudioSource.StartFade(ambience[2], fadeDuration, 1f));

    }

        //Disable visual effects when inside volume and closing eyes 
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="EchoTrigger" && image.GetComponent<EyeController>().isClosed){
            Debug.Log("OnTrigger called");
            if(other.gameObject.transform.GetChild(0)!=false){
                //instead of variable 0 insert variable fetchable int from other.
                isPLayingVoiceOver = true;
                playVoiceOver(0);
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                NextEcho();
            }

        }

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="EchoTrigger"  && currentClosest<2){
            Debug.Log("OnTrigger called");
            if(closestEcho.transform.GetChild(0)!=false){
                currentClosest+=1;
                closestEcho=echoes[currentClosest];

            }

        }
        if(other.gameObject.tag=="ET1"  && currentClosest<2){
            Debug.Log("OnTrigger called");
            if(closestEcho.transform.GetChild(0)!=false){
                currentClosest+=1;
                closestEcho=echoes[currentClosest];

            }

        }
        if(other.gameObject.tag=="ET2" && currentClosest<2){
            Debug.Log("OnTrigger called");
            if(closestEcho.transform.GetChild(0)!=false){
                currentClosest+=1;
                closestEcho=echoes[currentClosest];

            }

        }

    }*/

}
