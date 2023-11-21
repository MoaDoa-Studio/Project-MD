using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;
    public Sound[] sounds;

    // 플레이 배경음 이름
    public string bgmName = "";
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

    private void Start()
    {
        PlayBgm("Main_Bgm");
        Debug.Log("now listening Bgm : " + sounds);
    }
    // 사운드 Play
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


   // BGM 중지
    public void StopBgm()
    {
        Sound sound = null;

        foreach (Sound s in sounds)
        {
            if (s.sound_Name == bgmName)
            {
                sound = s;
                break;
            }
        }

        if (sound == null)
        {
            //Debug.Log("Stop Sound : " + name + "File not found!!!");
            return;
        }

        bgmName = "";
        sound.audioSource.Stop();
    }

    // BGM 플레이
    public void PlayBgm(string name)
    {
        //기존에 플레이 되는 배경음과 새로운 배경음이 같을때
        if (bgmName == name)
        {
            return;
        }

        //기존 배경음 중단
        StopBgm();

        //새로운 배경음 플레이
        Sound sound = null;

        foreach (Sound s in sounds)
        {
            if (s.sound_Name == name)
            {
                sound = s;
                break;
            }
        }

        if (sound == null)
        {
            Debug.Log("Play Sound : " + name + "File not found!!!");
            return;
        }

        sound.volume = 0.7f;
        bgmName = sound.sound_Name;

        sound.audioSource.Play();
    }


}
