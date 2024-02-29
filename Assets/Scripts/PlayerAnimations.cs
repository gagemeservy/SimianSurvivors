using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    Animator animator;
    PlayerController playerMovement;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.playerMovement = GetComponent<PlayerController>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.moveDirection.x != 0 || playerMovement.moveDirection.y != 0)
        {
            animator.SetBool("Move", true);

            SetSpriteDirection();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    private void SetSpriteDirection()
    {
        if(playerMovement.moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerMovement.moveDirection.x > 0)
        {
            spriteRenderer.flipX= false;
        }
    }
}
