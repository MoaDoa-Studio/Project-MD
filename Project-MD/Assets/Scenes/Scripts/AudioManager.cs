using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;
    public Sound[] sounds;

    // �÷��� ����� �̸�
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
                // BGM�� audioMixerGroup[1]�� ��ġ��
               s.audioSource.outputAudioMixerGroup = audioMixerGroup[1];
           }
           else
           {   // SFX�� ����� �κ�
               s.audioSource.outputAudioMixerGroup = audioMixerGroup[2];
           }

        }
    
    }

    private void Start()
    {
        PlayBgm("Main_Bgm");
        Debug.Log("now listening Bgm : " + sounds);
    }
    // ���� Play
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


   // BGM ����
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

    // BGM �÷���
    public void PlayBgm(string name)
    {
        //������ �÷��� �Ǵ� ������� ���ο� ������� ������
        if (bgmName == name)
        {
            return;
        }

        //���� ����� �ߴ�
        StopBgm();

        //���ο� ����� �÷���
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
