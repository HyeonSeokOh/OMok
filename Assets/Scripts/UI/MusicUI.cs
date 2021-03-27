using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MusicUI : MonoBehaviour
{
    //배경음
    public Slider BackGroundVolume;
    public AudioSource audio1;
    private float backVol = 1f; 

    //효과음 
    public Slider SfxVolume;
    public AudioSource audio2;
    private float backVol2 = 1f;

    void Start()
    {
        //배경음
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        BackGroundVolume.value = backVol;
        if (audio1 != null)
            audio1.volume = BackGroundVolume.value;

        //효과음
        backVol2 = PlayerPrefs.GetFloat("backvo2", 1f);
        BackGroundVolume.value = backVol2;
        if (audio2 != null)
            audio2.volume = SfxVolume.value;
    }

    void Update()
    {
        SoundSilder();
        SoundSilder2();
    }

    public void SoundSilder() //배경음악 조절
    {
        if (audio1 != null)
            audio1.volume = BackGroundVolume.value;

        backVol = BackGroundVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }

    public void SoundSilder2() //효과음 조절
    {
        if (audio2 != null)
            audio2.volume = SfxVolume.value;

        backVol2 = SfxVolume.value;
        PlayerPrefs.SetFloat("backvol2", backVol2);
    }
}
