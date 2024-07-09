using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

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
                moveX = joystick.Horizontal;
                moveY = joystick.Vertical;
            }
            

            moveDirection = new Vector2(moveX, moveY).normalized;

            //if (Input.GetMouseButtonDown(0))
            //if(Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0)
            //{
             //   float mouseXValue = Input.GetAxisRaw("Mouse X");
              //  float mouseYValue = Input.GetAxisRaw("Mouse Y");
                //moveDirection = Input.mousePosition.normalized;
              //  moveDirection = new Vector2(mouseXValue, mouseYValue);
            //}
        }

        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    PauseButton();
        //}

        if (Input.GetButtonDown("Cancel"))
        {
            PauseButton();
        }
    }

    private void Move()
    {
        //Debug.Log("Current move speed is " + player.currentMoveSpeed);
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
