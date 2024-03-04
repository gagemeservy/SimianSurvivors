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

    private float timeElapsed;
    float minutes;
    float seconds;

    PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        timeElapsed = Time.deltaTime;

        minutes = Mathf.FloorToInt(timeElapsed / 60);
        seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        this.healthText.SetText("Health: " + (int)player.currentHealth + "/" + (int)player.currentMaxHealth);

        this.movespeedText.SetText("Move Speed: " + player.currentMoveSpeed);

        this.recoveryspeedText.SetText("Recovery Speed: " + player.currentRecovery);

        this.attackDamageText.SetText("Attack Multiplier: " + player.currentAttackDamage);

        this.attackspeedText.SetText("Attack Speed: " + player.currentAttackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        minutes = Mathf.FloorToInt(timeElapsed / 60);
        seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        this.healthText.SetText("Health: " + (int)player.currentHealth + "/" + (int)player.currentMaxHealth);

        this.movespeedText.SetText("Move Speed: " + player.currentMoveSpeed);

        this.recoveryspeedText.SetText("Recovery Speed: " + player.currentRecovery);

        this.attackDamageText.SetText("Attack Multiplier: " + player.currentAttackDamage);

        this.attackspeedText.SetText("Attack Speed: " + player.currentAttackSpeed);
    }
}
