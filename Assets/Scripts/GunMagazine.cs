using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunMagazine : MonoBehaviour
{
    public GameObject bulletIconPrefab;

    public weaponSwitching weaponHolder;

    public Slider reloadTimer;

    public Coroutine reloadCoroutine;


    #region Singleton

    public static GunMagazine instance;

    void Awake () {
        if (instance != null)
        {
            Debug.LogWarning("Multiple inventory instances.");
            return;
        }
        
        instance = this;
    }

    #endregion

    public delegate void OnWeaponUsed();
    public OnWeaponUsed OnWeaponUsedCallback;

    void Start()
    {
        OnWeaponUsedCallback += UpdateUI;
        reloadTimer.gameObject.SetActive(false);
        DisableAmmo();

        if (OnWeaponUsedCallback != null)
            OnWeaponUsedCallback.Invoke();
    }

    void DisableAmmo() {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Reload() {
        if (weaponHolder.currentWeapon.bullets < weaponHolder.currentWeapon.magCapacity && weaponHolder.currentWeapon.remainingBullets > 0) {
            reloadTimer.gameObject.SetActive(true);
            reloadCoroutine = StartCoroutine(ReloadDelay());
        }
        else {
            weaponHolder.currentWeapon.isReloading = false;
        }
    }

    public void StopReloading() {
        if (weaponHolder.currentWeapon == null)
            return;
            
        reloadTimer.gameObject.SetActive(false);
        if (reloadCoroutine != null)
            StopCoroutine(reloadCoroutine);

        weaponHolder.currentWeapon.isReloading = false;
    }

    public IEnumerator ReloadDelay() {
        reloadTimer.value = 1f;
        while (reloadTimer.value >= 0.05f) {
            yield return new WaitForSeconds(Time.deltaTime);
            reloadTimer.value = reloadTimer.value - (Time.deltaTime/weaponHolder.currentWeapon.reloadTime);
        }
        weaponHolder.currentWeapon.Reload();
        reloadTimer.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        DisableAmmo();
        if (weaponHolder.currentWeapon == null)
            return;

        for (int i = 0; i < weaponHolder.currentWeapon.magCapacity; i++)
        {
            if (i + 1 <= weaponHolder.currentWeapon.bullets)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
