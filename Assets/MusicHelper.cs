using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicHelper : MonoBehaviour
{
    private static AudioSource m_Source;
    void Awake()
    {
        m_Source = GetComponent<AudioSource>();
        m_Source.volume = PlayerSingleton.instance.musicVol;
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
