using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : ProjectileWeaponBehavior
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    { 
        //transform.position += direction * weaponData.Speed * Time.deltaTime;

        transform.position += direction * weaponData.Speed * Time.deltaTime + new Vector3((Mathf.Sin(Time.time) * .01f), 0.0f, 0.0f); ;
    }
}
