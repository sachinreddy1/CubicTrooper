using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
    public int upgradeLevel = 0;
    // public GameObject buildEffect;

    public Weapon weapon;    

    void Start() {
        SelectWeapon();
    }

    // void BuyWeapon (WeaponBlueprint blueprint) {
    //     if (PlayerStats.Money < blueprint.upgradeCosts[0])
    //     {
    //         Debug.Log("Not enough money to buy that!");
    //         return;
    //     }

    //     PlayerStats.Money -= blueprint.upgradeCosts[0];

    //     GameObject _weapon = (GameObject)Instantiate(blueprint.prefabs[0], transform.position, transform.rotation);
    //     weapon = _weapon;

    //     // GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
    //     // Destroy(effect, 5f);

    //     upgradeLevel = 0;
    //     Debug.Log("Weapon bought!");
    // }

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
