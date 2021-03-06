﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private Slider sfxSlider;
    private Slider musicSlider;
    private Slider masterSlider;

    private Slider cameraSlider;
    private Slider zoomSlider;

    public GameObject optionsBlock;
    public GameObject creditsBlock;
    public GameObject tutorialBlock;

    public void onChangeMaster()
    {
        SoundManager.Instance.SetMasterVolume(masterSlider.value);
    }

    public void onChangeMusic()
    {
        
        SoundManager.Instance.SetMusicVolume(musicSlider.value);
        

    }

    public void PlayStartSound()
    {
        SoundManager.Instance.PlayAudio(Utils.AudioType.EFFECT, "StartGame");
    }

    public void PlayButtonSound()
    {
        SoundManager.Instance.PlayAudio(Utils.AudioType.EFFECT, "Click");
    }

    public void onChangeEffects()
    {
        SoundManager.Instance.SetSfxVolume(sfxSlider.value);
        SoundManager.Instance.PlayAudio(Utils.AudioType.EFFECT, "Danger");
    }


    public void onChangeCameraSensibility()
    {
        GameManager.Instance.cameraSensibility = cameraSlider.value;
    }

    public void onChangeZoomSensibility()
    {
        GameManager.Instance.zoomSensibility = zoomSlider.value;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void OpenCredits(bool state)
    {
        if (state == true && creditsBlock.activeInHierarchy)
        {
            creditsBlock.SetActive(false);
        }
        else
        {
            creditsBlock.SetActive(state);
        }
    }

    public void OpenTutorial(bool state)
    {
        if (state == true && tutorialBlock.activeInHierarchy)
        {
            tutorialBlock.SetActive(false);
        }
        else
        {
            tutorialBlock.SetActive(state);
        }
    }

    public void OpenOptions(bool state)
    {
        if (state == true && optionsBlock.activeInHierarchy)
        {
            optionsBlock.SetActive(false);
        }
        else
        {
            optionsBlock.SetActive(state);
        }
    }

    void Awake()
    {
        sfxSlider = GameObject.Find("SfxSlider").GetComponent<Slider>();
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        masterSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();

        cameraSlider = GameObject.Find("CameraSlider").GetComponent<Slider>();
        zoomSlider = GameObject.Find("ZoomSlider").GetComponent<Slider>();

        sfxSlider.value = SoundManager.Instance.GetSfxVolume();
        musicSlider.value = SoundManager.Instance.GetMusicVolume();
        masterSlider.value = SoundManager.Instance.GetMasterVolume();

        cameraSlider.value = GameManager.Instance.cameraSensibility;
        zoomSlider.value = GameManager.Instance.zoomSensibility;
    }

    private void Start()
    {
        optionsBlock.SetActive(false);
        creditsBlock.SetActive(false);
        tutorialBlock.SetActive(false);


        SoundManager.Instance.playingNow = Utils.PlayingNow.MAINMENU;
        SoundManager.Instance.StopMusic();

        Cursor.visible = true;
    }

}
