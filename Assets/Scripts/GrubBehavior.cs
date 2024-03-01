using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrubBehavior : ProjectileWeaponBehavior
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
        transform.position += direction * controller.speed * Time.deltaTime;
    }
}
