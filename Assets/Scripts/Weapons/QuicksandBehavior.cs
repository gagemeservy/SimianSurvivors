using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuicksandBehavior : ProjectileWeaponBehavior
{
    //CapsuleCollider2D quicksandCollider;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //quicksandCollider = GetComponent<CapsuleCollider2D>();
        //quicksandCollider is only needed if I'm going to change the size of it
    }

    protected override void OnTriggerStay2D(Collider2D collide)
    { 
        base.OnTriggerStay2D (collide);

        if (collide.gameObject.CompareTag("Enemy") || collide.gameObject.CompareTag("Item"))
        {
            Rigidbody2D rigidbody2D = collide.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - collide.transform.position).normalized;
            rigidbody2D.AddForce(forceDirection * currentForce);
        }
    }
}
