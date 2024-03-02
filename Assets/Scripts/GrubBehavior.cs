using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrubBehavior : ProjectileWeaponBehavior
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    { 
        //coconut movement
        transform.position += direction * weaponData.Speed * Time.deltaTime;
    }
}
