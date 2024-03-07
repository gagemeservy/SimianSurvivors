using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenuManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject MainMenuScreen;
    public GameObject InstructionScreen;
    //AudioManager audioPlayer;

    private void Awake()
    {
        //this.audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        InstructionScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }

    public void PressStart()
    {
        ButtonPressSound();
        StartCoroutine(LoadAsyncScene("MainLevel"));
    }

    private void ButtonPressSound()
    {
        //audioPlayer.PlaySFX(audioPlayer.ButtonClicked);
    }

    public void Quit()
    {
        ButtonPressSound();
        Application.Quit();
    }

    public void Instructions()
    {
        ButtonPressSound();
        InstructionScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }

    public void OkButtonToMenu()
    {
        ButtonPressSound();
        InstructionScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
    }
    IEnumerator LoadAsyncScene(String sceneToSwitchTo)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToSwitchTo);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
