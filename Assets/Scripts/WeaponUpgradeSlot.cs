using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeSlot : MonoBehaviour
{
    public Weapon weapon;
    //
    public Text weaponNumber;
    public Image weaponIcon;

    public Text buttonText;

    public Text upgradeCost;
    public Text ammoCost;

    public WeaponHolderSlot weaponHolderSlot;

    // Start is called before the first frame update
    void Start()
    {
        weaponNumber.text = weapon.weaponNumber.ToString() + ".";
        weaponIcon.sprite = weapon.weaponIcon;

        buttonText.text = "Buy";

        upgradeCost.text = "$" + weapon.upgradeCost.ToString();
        ammoCost.text = "$" + weapon.ammoCost.ToString();
    }

    public void BuyUpgradeWeapon() {
        if (!weaponHolderSlot.isBought) {
            weaponHolderSlot.BuyWeapon();
            buttonText.text = "Upgrade";
        }
        else {
            weaponHolderSlot.UpgradeWeapon();
        }
    }

    public void AddAmmo() {
        Weapon weapon = weaponHolderSlot.weapon.GetComponent<Weapon>();
        weapon.remainingBullets += 10;
        PlayerStats.Money -= weapon.ammoCost;
    }


}
