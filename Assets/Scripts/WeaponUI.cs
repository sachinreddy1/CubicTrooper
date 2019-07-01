using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    public weaponSwitching weaponHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        DisableUI();
        UpdateUI();
    }

    void DisableUI() {
        foreach (Transform child in this.transform) {
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = weaponHolder.selectedWeapon;

        if (Input.GetKeyDown("1")){
            weaponHolder.selectedWeapon = 0;
            weaponHolder.SelectWeapon();
            UpdateUI();
        }
        if (Input.GetKeyDown("2")){
            weaponHolder.selectedWeapon = 1;
            weaponHolder.SelectWeapon();
            UpdateUI();
        }
        if (Input.GetKeyDown("3")){
            weaponHolder.selectedWeapon = 2;
            weaponHolder.SelectWeapon();
            UpdateUI();
        }

        if (previousSelectedWeapon != weaponHolder.selectedWeapon) {
            if (GunMagazine.instance.OnWeaponUsedCallback != null)
                GunMagazine.instance.OnWeaponUsedCallback.Invoke();
        }
    }

    void UpdateUI () {
        for (int i = 0; i < transform.childCount; i++) {
            if (i < weaponHolder.gameObject.transform.childCount)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }

        // Set weapon to active or disabled based on weapons in inventory
        for (int i = 0; i < weaponHolder.gameObject.transform.childCount; i++)
        {
            WeaponSlot slot = transform.GetChild(i).gameObject.GetComponent<WeaponSlot>();
            slot.weapon = weaponHolder.gameObject.transform.GetChild(i).gameObject.GetComponent<Weapon>();
            slot.UpdateSlot();

            if (weaponHolder.selectedWeapon == i)
                slot.Toggle(true);
            else
                slot.Toggle(false);
        }
    }

}
