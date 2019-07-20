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
        }
        if (Input.GetKeyDown("2")){
            weaponHolder.selectedWeapon = 1;
            weaponHolder.SelectWeapon();
        }
        if (Input.GetKeyDown("3")){
            weaponHolder.selectedWeapon = 2;
            weaponHolder.SelectWeapon();
        }

        // Needs to be moved.
        UpdateUI();

        if (previousSelectedWeapon != weaponHolder.selectedWeapon) {
            GunMagazine.instance.StopReloading();
            if (GunMagazine.instance.OnWeaponUsedCallback != null)
                GunMagazine.instance.OnWeaponUsedCallback.Invoke();
        }
    }

    void UpdateUI () {
        // Setting weapon Icon UI based on weapons in weaponHolder and if item is bought
        for (int i = 0; i < transform.childCount; i++) {
            if (weaponHolder.GetComponent<Transform>().GetChild(i).GetComponent<WeaponHolderSlot>().isBought) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }

        // Set weapon to active or disabled based on weapons in inventory
        for (int i = 0; i < weaponHolder.gameObject.transform.childCount; i++)
        {
            // Get the UI slot
            WeaponSlot slot = transform.GetChild(i).gameObject.GetComponent<WeaponSlot>();
            // Get slot from weaponHolder
            WeaponHolderSlot weaponHolderSlot = weaponHolder.GetComponent<Transform>().GetChild(i).GetComponent<WeaponHolderSlot>();
            // Set UI slot weapon from weaponSlot weapon
            slot.weapon = weaponHolderSlot.weapon;
            slot.UpdateSlot();
            // Toggle Slot based on selected weapon
            if (weaponHolder.selectedWeapon == i)
                slot.Toggle(true);
            else
                slot.Toggle(false);
        }
    }

}
