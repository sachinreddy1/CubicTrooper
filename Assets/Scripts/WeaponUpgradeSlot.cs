using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeSlot : MonoBehaviour
{
    public Text weaponNumber;
    public Image weaponIcon;
    public Image upgradeIcon;
    //
    public Text buttonText;
    public Button upgradeButton;
    //
    public Text upgradeCost;
    public Text ammoCost;
    //
    public WeaponHolderSlot weaponHolderSlot;
    private Weapon weapon;
    //
    private BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = "Buy";
        buildManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BuildManager>();
    }

    public void UpdateSlot() {
        // Set weapon and check
        weapon = weaponHolderSlot.weapon;
        if (weapon == null)
            return;
        
        // Set weaponIcon : Should be moved
        weaponIcon.sprite = weapon.weaponIcon;

        // Set upgradeIcon colors
        if (!weaponHolderSlot.isBought)
            upgradeIcon.color = WeaponUI.instance.unBoughtColor;
        else    
            upgradeIcon.color = WeaponUI.instance.colors[weaponHolderSlot.upgradeLevel];

        // Update button graphics
        UpdateButtons();
    }

    void UpdateButtons() {
        if(weaponHolderSlot.upgradeLevel != weaponHolderSlot.MaxUpgradeLevel-1) {
            upgradeCost.text = "$" + weapon.upgradeCost.ToString();
            ammoCost.text = "$" + weapon.ammoCost.ToString();

            upgradeButton.interactable = true;
        }
        else {
            buttonText.text = "DONE";
            upgradeCost.text = "";
            upgradeButton.interactable = false;
        }
    }

    // ---------------------------------------------------------- //

    public void BuyUpgradeWeapon() {
        if (!weaponHolderSlot.isBought) {
            if (weaponHolderSlot.BuyWeapon())
                buttonText.text = "Upgrade";
        }
        else {
            weaponHolderSlot.UpgradeWeapon();
        }

        if (WeaponUI.instance.OnWeaponUIUsedCallback != null)
            WeaponUI.instance.OnWeaponUIUsedCallback.Invoke();
    }

    public void AddAmmo() {
        Weapon weapon = weaponHolderSlot.weapon.GetComponent<Weapon>();

        buildManager.SpendMoney(weapon.ammoCost);

        weapon.remainingBullets += 10;

        if (WeaponUI.instance.OnWeaponUIUsedCallback != null)
            WeaponUI.instance.OnWeaponUIUsedCallback.Invoke();
    }


}
