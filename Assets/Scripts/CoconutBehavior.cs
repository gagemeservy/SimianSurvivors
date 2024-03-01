using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutBehavior : ProjectileWeaponBehavior
{

    CoconutController controller;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = FindObjectOfType<CoconutController>();
    }

    // Update is called once per frame
    void Update()
    { 
        //coconut movement
        //the direction doesn't have to come from the player's direction. 
        //for most if not all weapons it actually won't
        transform.position += direction * controller.speed * Time.deltaTime;
    }
}
