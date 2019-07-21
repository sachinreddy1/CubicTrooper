using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
    public int upgradeLevel = 0;
    public bool isBought = false;
    //
    private BuildManager buildManager;
    

    public Weapon weapon;

    void Awake() {
        SelectWeapon();
        buildManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BuildManager>();
    }

    public bool BuyWeapon () {
        if (PlayerStats.Money < weapon.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return false;
        }

        PlayerStats.Money -= weapon.upgradeCost;
        isBought = true;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, buildManager.player.position, buildManager.player.rotation);
        Destroy(effect, 5f);

        upgradeLevel = 0;
        SelectWeapon();
        buildManager.weaponHolder.SelectWeapon();

        if (GunMagazine.instance.OnWeaponUsedCallback != null)
            GunMagazine.instance.OnWeaponUsedCallback.Invoke();

        if (WeaponUI.instance.OnWeaponUIUsedCallback != null)
            WeaponUI.instance.OnWeaponUIUsedCallback.Invoke();

        if (Shop.instance.OnShopUsedCallback != null)
            Shop.instance.OnShopUsedCallback.Invoke();

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

        if (Shop.instance.OnShopUsedCallback != null)
            Shop.instance.OnShopUsedCallback.Invoke();

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, buildManager.player.position, buildManager.player.rotation);
        Destroy(effect, 5f);

        Debug.Log("Weapon upgraded!");
    }
}
