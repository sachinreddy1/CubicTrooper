using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
    public int upgradeLevel = 0;
    public bool isBought = false;
    // public GameObject buildEffect;

    public Weapon weapon;

    void Awake() {
        SelectWeapon();
    }

    public void BuyWeapon () {
        if (PlayerStats.Money < weapon.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= weapon.upgradeCost;
        isBought = true;

        // GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        // Destroy(effect, 5f);

        upgradeLevel = 0;
        SelectWeapon();
        
        Debug.Log("Weapon bought!");
    }

    public void SelectWeapon() {
        int i = 0;
        foreach (Transform _weapon in transform)
        {
            if (i == upgradeLevel) {
                _weapon.gameObject.SetActive(true);
                weapon = _weapon.GetComponent<Weapon>();
            }
            else
                _weapon.gameObject.SetActive(false);
            i++;    
        }
    }

    public void UpgradeWeapon() {
        if (PlayerStats.Money < weapon.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }
        upgradeLevel++;
        PlayerStats.Money -= weapon.upgradeCost;

        SelectWeapon();

        // GameObject effect = (GameObject)Instantiate(buildManager.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        // Destroy(effect, 5f);

        Debug.Log("Weapon upgraded!");
    }
}
