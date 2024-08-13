using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody2D rb;
    [HideInInspector]
    public Vector2 moveDirection;
    float moveSpeed;
    PlayerStats player;
    public static bool isPaused = false;
    public GameObject pauseScreen;
    public GameObject pauseScreenFirstButton;
    [SerializeField] private FloatingJoystick joystick;

    private void Awake()
    {
        if (pauseScreen)
        {
            pauseScreen.SetActive(false);
        }

        //This is to activate the joystick once because for some reason it doesn't show up the first time the user clicks unless we do this.
        /*EventSystem eventSystem = gameObject.AddComponent<EventSystem>();
        PointerEventData pointerEventData = new PointerEventData(null);
        joystick.OnPointerDown(pointerEventData);*/
        //joystick.background.gameObject.SetActive(true);

    }

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        if (!isPaused)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if(moveX == 0 && moveY == 0)
            {
                //Debug.Log("Current joystick diesction is" + joystick.Horizontal + ", " + joystick.Vertical);
                moveX = joystick.Horizontal;
                moveY = joystick.Vertical;
            }
            

            moveDirection = new Vector2(moveX, moveY).normalized;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            PauseButton();
        }
    }

    private void Move()
    {
        //Debug.Log("Current move speed is " + player.currentMoveSpeed);
        //Debug.Log("Current move diesction is " + moveDirection.x + ", " + moveDirection.y);
        rb.velocity = new Vector2(moveDirection.x * player.currentMoveSpeed, moveDirection.y * player.currentMoveSpeed);
    }

    public void PauseButton()
    {
        //ButtonPressSound();

        if (!isPaused)
        {
            player.DeactivateEventSystem();
            Pause(pauseScreen);
            player.ActivateEventSystem(pauseScreenFirstButton);
        }
        else
        {
            unPause(pauseScreen);
        }
    }

    public static void Pause(GameObject screenToShow)
    {
        Time.timeScale = 0;
        isPaused = true;
        screenToShow.SetActive(true);
    }

    public static void unPause(GameObject screenToHide)
    {
        Time.timeScale = 1;
        isPaused = false;
        screenToHide.SetActive(false);
    }

    public void Resume()
    {
        //ButtonPressSound();
        unPause(pauseScreen);
    }

    private void ButtonPressSound()
    {
        //audioPlayer.PlaySFX(audioPlayer.ButtonClicked);
    }

    public void Restart()
    {
        //ButtonPressSound();
        unPause(pauseScreen);
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        StartCoroutine(LoadAsyncScene("MainLevel"));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        StartCoroutine(LoadAsyncScene("MainMenu"));
    }

    public void KeepPlaying()
    {
        unPause(player.crownNoTakeScreen);
        unPause(player.crownTakeScreen);
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
