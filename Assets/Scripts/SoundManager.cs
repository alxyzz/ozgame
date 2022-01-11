using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource sfxSource;
    public GameObject soundtrackObj;
    private AudioSource soundtrackSource;
    //sounds, add em here
    [Space(10)]
    public AudioClip TitleSong;
    public AudioClip MainTheme;
    [Space(10)]
    public AudioClip hit1;
    public AudioClip click;
    public AudioClip failure;
    public AudioClip hit4;
    public AudioClip hit5;



    // Start is called before the first frame update
    void Start()
    {
        soundtrackSource = soundtrackObj.GetComponent<AudioSource>();
        sfxSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFailureSound()
    {
 
    }

    public void PlayClickSound()
    {

    }

    public void ChangeSoundtrack(AudioClip ost)
    {//for soundtracks
        soundtrackSource.clip = ost;
        soundtrackSource.Play();
    }


    public void StopSoundtrack()
    {
        soundtrackSource.Stop();
    }

}
