using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody2D rb;
    public CharacterScriptableObject characterData;
    [HideInInspector]
    public Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * characterData.MoveSpeed, moveDirection.y * characterData.MoveSpeed);
    }
}
