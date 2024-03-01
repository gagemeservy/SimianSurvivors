using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a base script that should be put on all projectile weapon prefabs
public class ProjectileWeaponBehavior : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 givenDirection)
    {
        this.direction = givenDirection;
    }
}
