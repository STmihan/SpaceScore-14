using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class InMenuUIManager : MonoBehaviour
{
    public MenuUI MenuUI;
    private bool a = false;
    private bool b = true;
    private bool c = true;
    public AudioSource SoundUI;
    private void Start()
    {
        MenuUI.Help.onClick.AddListener(OnHelp);
        MenuUI.Play.onClick.AddListener(OnPlay);
        MenuUI.Music.onClick.AddListener(OnMusic);
        MenuUI.Sound.onClick.AddListener(OnSound);
        
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer
            .GetFloat("Music", out float music);
        if (music < 0) b = false;
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer
            .GetFloat("Sounds", out float sounds);
        if (sounds < 0) c = false;
    }

    void OnHelp()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        a = !a;       
        MenuUI.HelpUI.gameObject.SetActive(a);
    }

    void OnPlay()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        SceneManager.LoadScene(1);
    }

    void OnMusic()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        b = !b;
        if (b)
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer.SetFloat("Music", 0);
        else
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer.SetFloat("Music", -80);
    }

    void OnSound()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        c = !c;
        if (c)
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer.SetFloat("Sounds", 0);
        else
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer.SetFloat("Sounds", -80);
    }
}
