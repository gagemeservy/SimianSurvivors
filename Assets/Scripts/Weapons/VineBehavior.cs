using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBehavior : ProjectileWeaponBehavior
{
    Transform player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerStats>().transform;
    }

    private void Update()
    {
        transform.position = direction + player.position;
    }
}
