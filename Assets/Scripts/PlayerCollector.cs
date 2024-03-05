using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;
    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        playerCollector.radius = player.currentMagnet;
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if(collide.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //THIS IS HOW WE'LL NEED TO MAKE QUICK SAND TO PULL ENEMIES LATER
            Rigidbody2D rigidbody2D = collide.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - collide.transform.position).normalized;
            rigidbody2D.AddForce(forceDirection * pullSpeed);

            collectible.Collect();
        }
    }
}
