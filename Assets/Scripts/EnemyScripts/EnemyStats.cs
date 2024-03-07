using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public GameObject announcementBubble;
    public TMP_Text playerAnnouncements;

    [Header("AnnounceDuration")]
    public float announceDuration;
    float announceTimer;

    [Header("FlashDuration")]
    float flashDuration = .1f;
    float flashTimer = 0;
    private Color colorToFlash = Color.red;
    private Color originalColor;
    private SpriteRenderer sprite;
    float widthVariation;
    Vector3 originalScale;
    Vector3 newScale;

    public float despawnDistance = 20f;
    Transform player;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        currentMoveSpeed = enemyData.MoveSpeed;
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
        //originalScale = sprite.transform.localScale;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    private void Update()
    {
        /*if (announceTimer > 0)
        {
            announceTimer -= Time.deltaTime;
        }
        else
        {
            announcementBubble.SetActive(false);
        }*/

        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }


        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
        }
        else
        {
            sprite.color = originalColor;
        }
        
        //sprite.transform.localScale
    }

    private void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }

    public void TakeDamage(float damage)
    {
        //DISPLAY DAMAGE NUMBERS BEING TAKEN ABOUT THE ENEMY'S TRANSFORM.POSITION
        //Announce(damage.ToString());
        FlashDamage();
        currentHealth -= damage;

        if(currentHealth <= 0) 
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    void Announce(string text)
    {
        announceTimer = announceDuration;
        announcementBubble.SetActive(true);
        this.playerAnnouncements.SetText(text);
    }

    private void FlashDamage()
    {
        flashTimer = flashDuration;
        sprite.color = colorToFlash;
    }

    private void OnDestroy()
    {
        //AudioManager audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //audioPlayer.PlaySFX(audioPlayer.enemyDeath1);

        //Debug.Log(enemyData.ToString());
        //replace false with statement to check that enemyData.ToString() == "SupremeSimian" something make sure you get the right title
        if (enemyData.ToString().Contains("SupremeSimian (EnemyScriptableObject)"))
        {
            //if supreme simian1, call supremeSimian2
            ////if supremeSimiman2 end the game. Probably have a function on PlayerStats or something and call it
            //Application.Quit();
            //player.FindChild("PlayerStats").
            Debug.Log("DIED");
        }
        else
        {
            EnemySpawner es = FindObjectOfType<EnemySpawner>();
            es.OnEnemyKilled();
        }
    }
}
