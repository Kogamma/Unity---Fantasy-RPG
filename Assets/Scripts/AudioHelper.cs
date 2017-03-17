using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    private static AudioSource m_Source;
    void Awake()
    {
        m_Source = GetComponent<AudioSource>();
        UpdateVolume();
    }

    public static void UpdateVolume()
    {
        m_Source.volume = PlayerSingleton.instance.sfxVol;
        if (m_Source.volume <= 0)
        {
            m_Source.mute = true;
        }
        else
            m_Source.mute = false;
    }

    public static void PlaySound(AudioClip clip, float volMod = 1)
    {
        m_Source.pitch = 1;
        m_Source.PlayOneShot(clip, PlayerSingleton.instance.sfxVol * volMod);
    }
    public static void PlayPitched(AudioClip clip, float minPitch, float maxPitch, float volMod = 1)
    {
        m_Source.pitch = Random.Range(minPitch, maxPitch);
        m_Source.PlayOneShot(clip, PlayerSingleton.instance.sfxVol * volMod);
    }
    public static void PlayDelayed(AudioClip clip, float delay)
    {
        m_Source.clip = clip;
        m_Source.PlayDelayed(delay);
    }
}
