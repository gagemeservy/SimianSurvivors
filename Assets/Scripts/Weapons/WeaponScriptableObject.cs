using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponScriptableObject", menuName ="ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    //Base Weapon Stats
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    [SerializeField]
    float knockback;
    public float Knockback { get => knockback; private set => knockback = value; }
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }
    [SerializeField]
    float freezeDuration;
    public float FreezeDuration { get => freezeDuration; private set => freezeDuration = value; }
    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }
    [SerializeField]
    int numberOfAttacksToDo;
    public int NumberOfAttacksToDo { get => numberOfAttacksToDo; private set => numberOfAttacksToDo = value; }
    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    float currentForce;
    public float CurrentForce { get => currentForce; private set => currentForce = value; }

    [SerializeField]
    int level;
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [Header("Spawn Points")]
    [SerializeField]
    List<Transform> relativeSpawnPoints;
    public List<Transform> RelativeSpawnPoints { get => relativeSpawnPoints; private set => relativeSpawnPoints = value; }

    [SerializeField]
    string description;
    public string Description { get => description; private set => description = value; }

}
