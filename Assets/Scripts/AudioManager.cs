using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance_;
    public static AudioManager Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<AudioManager>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("AudioManager").AddComponent<AudioManager>();
                    instance_ = newSingleton;
                }
            }
            return instance_;
        }
        private set
        {
            instance_ = value;
        }
    }

    private AudioSource audioSource;
    public AudioClip[] SkillAudio = new AudioClip[10];
    public AudioClip[] WinLoseAu = new AudioClip[2];
    public AudioClip[] DollSta = new AudioClip[2];
    public AudioClip[] backMusic = new AudioClip[2];

    GameObject canvas;

    int i;

    private void Awake()
    {
        var objs = FindObjectsOfType<AudioManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        this.transform.GetChild(0).gameObject.GetComponent<AudioSource>().clip = backMusic[0];
        this.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
        canvas = this.transform.GetChild(1).gameObject;
        canvas.SetActive(false);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySkillClip(int i)
    {
        //audioSource.clip = SkillAudio[i];
        audioSource.PlayOneShot(SkillAudio[i]);
    }

    public void WinLoseAudio(int i)
    {
        //audioSource.PlayOneShot(WinLoseAu[i]);
    }

    public void DollSt(int i)
    {
        audioSource.PlayOneShot(DollSta[i]);
    }

    public void changeMusic(int i)
    {
        this.transform.GetChild(0).gameObject.GetComponent<AudioSource>().clip = backMusic[i];
        this.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
    }

    public void openOption()
    {
        canvas.SetActive(true);
    }

    public void closeOption()
    {
        canvas.SetActive(false);
    }
}
