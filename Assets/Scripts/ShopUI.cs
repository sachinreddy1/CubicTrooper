using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    public GameObject ui;
    public Text upgradeCost;
    public Text ammoCost;
    public Button upgradeButton;
    public Button ammoButton;

    public void Show() {
        upgradeCost.text = "DONE";
        upgradeButton.interactable = false;

        ammoCost.text = "$500";
        ui.SetActive(true);
    }

    public void Hide() {
        ui.SetActive(false);
    }

    public void UpgradeGun() {
        Debug.Log("Upgrade Gun.");
    }

    public void AddAmmo() {
        Debug.Log("Adding Ammo.");
    }
}
