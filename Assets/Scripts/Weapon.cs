using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int weaponNumber;
    
    [Header("General")]
    public Sprite weaponIcon;
    //
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform gunButt;
    
    [Header("Weapon Stats")]
    public float reloadTime = 50f;
    public float recoil = 50f;
    public float fireRate = 0f;
    // Bullet Stats
    public int magCapacity = 5;
    public int bullets;
    public bool isReloading = false;
    //
    public int remainingBullets;
    
    [Header("Unity Setup Attr.")]
    private weaponSwitching weaponHolder;
    private GunMagazine gunMagazine;
    
    [Header("Shop Prices")]
    public int ammoCost = 100;
    public int upgradeCost = 500;

    void Start() {
        gunMagazine = GunMagazine.instance;
        weaponHolder = transform.parent.gameObject.GetComponent<weaponSwitching>();
        bullets = magCapacity;
    }

    public void Shoot() {
        if (bullets > 0) {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            weaponHolder.Recoil();
            bullets--;
        } else {
            if (!isReloading) {
                isReloading = true;
                gunMagazine.Reload();
            }
        }

        if (gunMagazine.OnWeaponUsedCallback != null)
            gunMagazine.OnWeaponUsedCallback.Invoke();
    }

    public void Reload() {
        if (remainingBullets + bullets <= magCapacity) {
            bullets += remainingBullets;
            remainingBullets = 0;
        }
        else {
            remainingBullets -= (magCapacity - bullets);
            bullets = magCapacity;
        }

        isReloading = false;
        if (gunMagazine.OnWeaponUsedCallback != null)
            gunMagazine.OnWeaponUsedCallback.Invoke();
    } 

}
