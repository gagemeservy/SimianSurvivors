using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterScriptableObject", menuName ="ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    //character stats
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float attackDamage;
    public float AttackDamage { get => attackDamage; private set => attackDamage = value; }

    [SerializeField]
    float attackSpeed;
    public float AttackSpeed { get => attackSpeed; private set => attackSpeed = value; }


    [SerializeField]
    float magnet;
    public float Magnet { get => magnet; private set => magnet = value; }
}
