using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutBehavior : ProjectileWeaponBehavior
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.isPaused)
        {
            transform.position += direction * weaponData.Speed * Time.deltaTime;
        }
    }
}
