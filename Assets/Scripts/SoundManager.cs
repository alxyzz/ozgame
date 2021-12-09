using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    Dictionary<string, AudioClip> soundEffectList = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> backgroundMusic = new Dictionary<string, AudioClip>();
    public AudioSource sfxSource;
    public GameObject soundtrackObj;
    private AudioSource soundtrackSource;
    //sounds
    [Space(10)]
    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public AudioClip hit4;
    public AudioClip hit5;



    // Start is called before the first frame update
    void Start()
    {
        soundtrackSource = soundtrackObj.GetComponent<AudioSource>();
        sfxSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        GameManager.SoundManagerRef = this;
        soundEffectList.Add("hit1", hit1); //add em like this
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void PlaySoundByName(string soundID)
    {//meant for repetitively used, short sounds, like attack/miss sounds or monster mutterings
        AudioClip temp = null;
        if (soundEffectList.TryGetValue(soundID, out temp))
        {
            sfxSource.PlayOneShot(temp);
        }
    }

    public void ChangeSoundtrack(AudioClip ost)
    {//for soundtracks
        soundtrackSource.clip = ost;
        
    }

    public void StartSoundtrack()
    {
        soundtrackSource.Play();
    }

    public void StopSoundtrack()
    {
        soundtrackSource.Stop();
    }

}
