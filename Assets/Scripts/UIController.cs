using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject UICanvas;
    public TMP_Text timerText;
    public TMP_Text healthText;
    public TMP_Text movespeedText;
    public TMP_Text recoveryspeedText;
    public TMP_Text attackDamageText;
    public TMP_Text attackspeedText;
    public TMP_Text levelText;
    public TMP_Text XPText;

    private float timeElapsed;
    float minutes;
    float seconds;

    PlayerStats player;

    float displayAttackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        

        player = FindObjectOfType<PlayerStats>();
        displayAttackSpeed = 1 + player.currentAttackSpeed;

        timeElapsed = Time.deltaTime;

        minutes = Mathf.FloorToInt(timeElapsed / 60);
        seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        this.healthText.SetText("Health: " + (int)player.currentHealth + "/" + (int)player.currentMaxHealth);

        this.movespeedText.SetText("Move Speed: " + player.currentMoveSpeed);

        this.recoveryspeedText.SetText("Recovery Speed: " + player.currentRecovery);

        this.attackDamageText.SetText("Attack Multiplier: " + player.currentAttackDamage);

        this.attackspeedText.SetText("Attack Speed: " + displayAttackSpeed);

        this.levelText.SetText("Level: " + player.level);

        this.XPText.SetText("XPnuts: " + player.experience + "/" + player.experienceCap);
    }

    // Update is called once per frame
    void Update()
    {
        displayAttackSpeed = 1 + player.currentAttackSpeed;

        timeElapsed += Time.deltaTime;
        minutes = Mathf.FloorToInt(timeElapsed / 60);
        seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        this.healthText.SetText("Health: " + (int)player.currentHealth + "/" + (int)player.currentMaxHealth);

        this.movespeedText.SetText("Move Speed: " + player.currentMoveSpeed);

        this.recoveryspeedText.SetText("Recovery Speed: " + player.currentRecovery);

        this.attackDamageText.SetText("Attack Multiplier: " + player.currentAttackDamage);

        this.attackspeedText.SetText("Attack Speed: " + displayAttackSpeed);

        this.levelText.SetText("Level: " + player.level);

        this.XPText.SetText("XPnuts: " + (int)player.experience + "/" + (int)player.experienceCap);
    }
}
