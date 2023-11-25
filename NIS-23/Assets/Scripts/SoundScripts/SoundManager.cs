using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource _musicSource, _effectSource;
    void Awake()
    {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        _musicSource.playOnAwake = true;
    }

    public void PlaySound(AudioClip clip){
        _effectSource.PlayOneShot(clip);
    }
}
