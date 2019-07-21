using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public weaponSwitching weaponHolder;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

     #region Singleton

    public static WeaponUI instance;

    void Awake () {
        if (instance != null)
        {
            Debug.LogWarning("Multiple WeaponUI instances.");
            return;
        }
        
        instance = this;
    }

    #endregion

    public delegate void OnWeaponUIUsed();
    public OnWeaponUIUsed OnWeaponUIUsedCallback;
    
    // Start is called before the first frame update
    void Start()
    {
        OnWeaponUIUsedCallback += UpdateUI;
        DisableUI();

        if (OnWeaponUIUsedCallback != null)
            OnWeaponUIUsedCallback.Invoke();
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

        for (int i = 0; i < keyCodes.Length; i++) {
            if (Input.GetKeyDown(keyCodes[i])) {
                    int numberPressed = i;
                    weaponHolder.selectedWeapon = numberPressed;
                    weaponHolder.SelectWeapon();
                    
                    if (OnWeaponUIUsedCallback != null)
                        OnWeaponUIUsedCallback.Invoke();
            }
        }

        if (previousSelectedWeapon != weaponHolder.selectedWeapon) {
            GunMagazine.instance.StopReloading();
            if (GunMagazine.instance.OnWeaponUsedCallback != null)
                GunMagazine.instance.OnWeaponUsedCallback.Invoke();
        }
    }

    void ToggleWeaponSlots() {
        // Setting weapon Icon UI based on weapons in weaponHolder and if item is bought
        for (int i = 0; i < transform.childCount; i++) {
            WeaponHolderSlot weaponHolderSlot = weaponHolder.GetComponent<Transform>().GetChild(i).GetComponent<WeaponHolderSlot>();
            if (weaponHolderSlot.isBought) {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void UpdateWeaponSlots() {
        int idx = 0;
        // Set weapon to active or disabled based on weapons in inventory
        for (int i = 0; i < weaponHolder.gameObject.transform.childCount; i++)
        {
            // Get the UI slot
            WeaponSlot slot = transform.GetChild(i).gameObject.GetComponent<WeaponSlot>();
            // Get slot from weaponHolder
            WeaponHolderSlot weaponHolderSlot = weaponHolder.GetComponent<Transform>().GetChild(i).GetComponent<WeaponHolderSlot>();
            // Set UI slot weapon from weaponSlot weapon
            Weapon weapon = weaponHolderSlot.weapon;

            if (weaponHolderSlot.isBought) {
                // Check for weapon
                if (weapon != null) {
                    slot.weapon = weapon;
                    slot.weaponNumber.text = (idx+1).ToString();
                    slot.UpdateSlot();

                    // Toggle Slot based on selected weapon
                    if (weaponHolder.selectedWeapon == idx)
                        slot.Toggle(true);
                    else
                        slot.Toggle(false);
                }
                idx++;
            }
        }
    }

    void UpdateUI () {
        ToggleWeaponSlots();
        UpdateWeaponSlots();
    }

}
