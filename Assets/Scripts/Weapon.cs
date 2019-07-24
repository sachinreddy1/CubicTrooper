using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public Sprite weaponIcon;
    
    [Header("General")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform gunButt;
    public GameObject fireEffect;
    
    [Header("Weapon Stats")]
    public float reloadTime = 50f;
    public float recoil = 50f;
    public float fireRate = 0f;
    // Bullet Stats
    public int magCapacity = 5;
    public int bullets;
    public int remainingBullets;
    [HideInInspector]
    public bool isReloading = false;
    
    [Header("Unity Setup Attr.")]
    private weaponSwitching weaponHolder;
    private GunMagazine gunMagazine;
    
    [Header("Shop Prices")]
    public int ammoCost = 100;
    public int upgradeCost = 1000;

    [Header("Shotgun")]
    public bool shotgun = false;
    public float bulletSpread = 30f;
    public GameObject shotgunBulletPrefab;
    public Transform firePoint_left;
    public Transform firePoint_right;

    void Start() {
        gunMagazine = GunMagazine.instance;
        weaponHolder = GameObject.FindGameObjectWithTag("weaponController").GetComponent<weaponSwitching>();
        bullets = magCapacity;
    }

    public void Shoot() {
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }

        if (bullets > 0) {
            if (!shotgun)
                Straight();
            if (shotgun)
                Shotgun();

            // Shoot Effect
            // GameObject effectInstance = (GameObject)Instantiate(fireEffect, firePoint.position, firePoint.rotation);
            // Destroy(effectInstance, 5f);

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

    void Straight() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Shotgun() {
        Instantiate(shotgunBulletPrefab, firePoint_left.position, firePoint.rotation * Quaternion.Euler(0, 0, bulletSpread));
        Instantiate(shotgunBulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(shotgunBulletPrefab, firePoint_right.position, firePoint.rotation * Quaternion.Euler(0, 0, -bulletSpread));
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

        if (WeaponUI.instance.OnWeaponUIUsedCallback != null)
            WeaponUI.instance.OnWeaponUIUsedCallback.Invoke();
    } 

}
