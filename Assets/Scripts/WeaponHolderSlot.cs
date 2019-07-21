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

    public bool BuyWeapon () {
        // --------- Need to be Added to BuildManager --------- //
        GameObject weaponHolder_ = GameObject.FindGameObjectWithTag("weaponController");
        weaponSwitching weaponHolder = weaponHolder_.GetComponent<weaponSwitching>();

        if (PlayerStats.Money < weapon.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return false;
        }

        PlayerStats.Money -= weapon.upgradeCost;
        isBought = true;

        // --------- Need to be Added to BuildManager --------- //
        // GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        // Destroy(effect, 5f);

        upgradeLevel = 0;
        SelectWeapon();
        weaponHolder.SelectWeapon();

        if (GunMagazine.instance.OnWeaponUsedCallback != null)
            GunMagazine.instance.OnWeaponUsedCallback.Invoke();

        if (WeaponUI.instance.OnWeaponUIUsedCallback != null)
            WeaponUI.instance.OnWeaponUIUsedCallback.Invoke();

        Debug.Log("Weapon bought!");
        return true;
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

        // --------- Need to be Added to BuildManager --------- //
        // GameObject effect = (GameObject)Instantiate(buildManager.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        // Destroy(effect, 5f);

        Debug.Log("Weapon upgraded!");
    }
}
