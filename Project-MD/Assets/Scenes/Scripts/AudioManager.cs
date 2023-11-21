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
                // BGM�� audioMixerGroup[1]�� ��ġ��
               s.audioSource.outputAudioMixerGroup = audioMixerGroup[1];
           }
           else
           {   // SFX�� ����� �κ�
               s.audioSource.outputAudioMixerGroup = audioMixerGroup[2];
           }

        }
    
    }


    // ���带 Play ����ϴ� �Լ�
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
            // Play ����� �Ҹ��� ������
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
