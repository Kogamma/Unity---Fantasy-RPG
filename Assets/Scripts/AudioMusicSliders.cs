using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMusicSliders : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public void MusicSlider()
    {
        PlayerSingleton.instance.musicVol = musicSlider.value;
        MusicHelper.UpdateVolume();
    }
    public void SfxSlider()
    {
        PlayerSingleton.instance.sfxVol = sfxSlider.value;
        AudioHelper.UpdateVolume();
    }
}
