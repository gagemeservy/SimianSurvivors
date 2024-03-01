using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();   
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedCoconut = Instantiate(prefab);
        spawnedCoconut.transform.position = transform.position; //this sets it to the player's position
        spawnedCoconut.GetComponent<CoconutBehavior>().DirectionChecker(playerMovement.moveDirection);
    }
}
