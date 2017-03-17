using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHelper : MonoBehaviour
{
    private static AudioSource m_Source;
    void Awake()
    {
        m_Source = GetComponent<AudioSource>();
        UpdateVolume();
    }

    public static void UpdateVolume()
    {
        m_Source.volume = PlayerSingleton.instance.musicVol;
        if (m_Source.volume <= 0)
        {
            m_Source.mute = true;
        }
        else
            m_Source.mute = false;
    }

    public static void PlaySound(AudioClip clip, float volMod = 1)
    {
        m_Source.clip = clip;
        m_Source.volume = PlayerSingleton.instance.musicVol * volMod;
        m_Source.Play();
    }

    public static void Stop()
    {
        m_Source.Stop();
    }
}
