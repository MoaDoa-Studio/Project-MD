using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioMixer audioMixer;
    public Sound[] sounds;
    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //DontDestoryOnLoad(gameObject);


        // Matching Audio mixergroups
        AudioMixerGroup[] audioMixerGroup = audioMixer.FindMatchingGroups("Master");

        foreach(Sound s in sounds)
        {
            s.audioSource = this.gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            s.audioSource.loop = s.loop;
            s.audioSource.pitch = s.pitch;
            s.audioSource.volume = s.volume * 0.5f;
        
        // BGM Loop
           if(s.audioSource.loop)
           {
                // BGM은 audioMixerGroup[1]에 배치됨
               s.audioSource.outputAudioMixerGroup = audioMixerGroup[1];
           }
           else
           {   // SFX가 재생될 부분
               s.audioSource.outputAudioMixerGroup = audioMixerGroup[2];
           }

        }
    
    }


    // 사운드를 Play 기능하는 함수
    public void Play(string name)
    {
        Sound sound = null;

        foreach(Sound s in sounds)
        {
            if(s.sound_Name == name)
            {
                sound = s;
                break;
            }

        }
            // Play 재생될 소리가 없을때
        if(sound == null)
        {
            Debug.Log("Sound :  " + name + "File not found!!");
            return;
        }

        sound.audioSource.Play();
    }


    public void StopBgm(string name)
    {
        Sound sound = null;


    }


}
