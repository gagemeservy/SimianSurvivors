using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject MainMenuScreen;
    public GameObject InstructionScreen;
    public EventSystem eventSystem;
    public GameObject InstructionsFirstButton;
    public GameObject MainMenuFirstButton;

    private void Awake()
    {
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.sendNavigationEvents = false;
        //ButtonPressSound();
        InstructionScreen.SetActive(true);
        MainMenuScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(InstructionsFirstButton);
        EventSystem.current.sendNavigationEvents = true;
    }

    public void OkButtonToMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.sendNavigationEvents = false;

        //ButtonPressSound();
        InstructionScreen.SetActive(false);
        MainMenuScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(MainMenuFirstButton);
        EventSystem.current.sendNavigationEvents = true;
    }

    public void Youtube()
    {
        Application.OpenURL("https://www.youtube.com/@SubbyDoodles");
    }

    public void Twitter()
    {
        Application.OpenURL("https://twitter.com/Subby_Doodles");
    }

    public void BlueSky()
    {
        Application.OpenURL("https://bsky.app/profile/subbydoodles.bsky.social");
    }

    public void Itch()
    {
        Application.OpenURL("https://subbydoodles.itch.io/");
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
