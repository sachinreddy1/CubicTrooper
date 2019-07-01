using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public int space = 5;
    public weaponSwitching weaponHolder;
    public List<Weapon> weapons = new List<Weapon>();

    #region Singleton

    public static WeaponInventory instance;

    void Awake () {
        if (instance != null)
        {
            Debug.LogWarning("Multiple inventory instances.");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnWeaponChanged();
    public OnWeaponChanged OnWeaponChangedCallback;

    void Start() {

    }

    // 
    
    public bool Add(Weapon weapon) {
        if (weapons.Count >= space) {
            Debug.Log("Not enough room!");
            return false;
        }
        weapons.Add(weapon);
        return true;
    }

    public void Remove(Weapon weapon) {
        weapons.Remove(weapon);
    }

    //
    public void ChangeWeapon() {
        // If the current weapon is reloading -> stop reloading
        if (weapons[weaponHolder.selectedWeapon].isReloading)
            GunMagazine.instance.StopReloading();

        // If selectedWeapon is less than the weapon Count -> Set current weapon
        if (weaponHolder.selectedWeapon < weapons.Count) 
            weaponHolder.currentWeapon = weapons[weaponHolder.selectedWeapon];
        else
            Debug.Log("Not a proper item number.");

        // Call WeaponUI Update
        if (OnWeaponChangedCallback != null)
            OnWeaponChangedCallback.Invoke();
        if (GunMagazine.instance.OnWeaponUsedCallback != null)
            GunMagazine.instance.OnWeaponUsedCallback.Invoke();
    }

}
