using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject UICanvas;
    public TMP_Text timerText;

    private float timeElapsed;
    float minutes;
    float seconds;
    float milliseconds;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = Time.deltaTime;

        minutes = Mathf.FloorToInt(timeElapsed / 60);
        seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //this.timerText.SetText(timeElapsed.ToString("0.##"));
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        //this.timerText.SetText(timeElapsed.ToString("0.##"));
        minutes = Mathf.FloorToInt(timeElapsed / 60);
        seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
