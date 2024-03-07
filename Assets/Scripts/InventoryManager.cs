using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(4);
    public int[] weaponLevels = new int[4];
    public List<Image> weaponUIslotsImages = new List<Image>(3);
    public List<TMP_Text> weaponUIslotsText = new List<TMP_Text>(3);

    public EnemySpawner enemySpawner;

    private bool SupremeSimianSpawned = false;

    public GameObject levelDownScreen;

    private void Awake()
    {
        enemySpawner.GetComponent<EnemySpawner>();
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUIslotsImages[slotIndex].sprite = weapon.weaponData.Icon;
        weaponUIslotsText[slotIndex].SetText("Level " + weapon.weaponData.Level + "\n" + weapon.weaponData.Title);
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if(weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];

            if (!weapon.weaponData.NextLevelPrefab)
            {
                //Debug.Log("No next level");
                //Need to double check here that the other weapons in inventory aren't also at the end. Because then we need to spawn big monkey Supreme Simian
                CheckForFinalLevels();
                return;
            }

            GameObject upgradeWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradeWeapon.transform.SetParent(transform);

            AddWeapon(slotIndex, upgradeWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradeWeapon.GetComponent<WeaponController>().weaponData.Level;

            //Need to check all weapon slots real fast to see if they're all fully upgraded, if so, call the big monke Supreme Simian
            CheckForFinalLevels();
        }

        CheckForFinalLevels();

        levelDownScreen.SetActive(false);
        Time.timeScale = 1;
        PlayerController.isPaused = false;
    }

    void CheckForFinalLevels()
    {
        if (!SupremeSimianSpawned)
        {
            for (int i = 0; i < weaponLevels.Length; i++)
            {
                //Debug.Log("weapon at " + i + " is level " + weaponLevels[i]);
                if (weaponLevels[i] != 1)
                {
                    return;
                }
            }

            //call big monke Supreme Simian
            //Debug.Log("Calling supreme simian from inventory manager");
            enemySpawner.SpawnSupremeSimian();
            SupremeSimianSpawned = true;
        }
    }
}
