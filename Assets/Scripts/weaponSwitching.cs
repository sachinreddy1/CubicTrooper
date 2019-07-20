using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitching : MonoBehaviour
{
    public bool canShoot = true;
    //
    public int selectedWeapon = 0;
    public Weapon currentWeapon;
    //
    public GameObject player;
    private Rigidbody2D rb;
    //
    private GunMagazine gunMagazine;

    void Start()
    {
        gunMagazine = GunMagazine.instance;
        
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.gameEnded) 
            return;
        
        SelectWeapon();

        // Left click pressed (Shoot)
        if (canShoot && currentWeapon != null) {
            if (currentWeapon.fireRate <= 0f) {
                if (Input.GetMouseButtonDown(0)) {
                    if (!currentWeapon.isReloading)
                        currentWeapon.Shoot();
                }
            }
            else {
                if (Input.GetMouseButtonDown(0)) {
                    if (!currentWeapon.isReloading)
                        currentWeapon.InvokeRepeating("Shoot", 0f, 1f/currentWeapon.fireRate);
                }
                else if (Input.GetMouseButtonUp(0)) {
                        currentWeapon.CancelInvoke("Shoot");
                }
            }
        }

        // Right click pressed (Reload)
        if (Input.GetMouseButtonDown(1)) {
            if (!currentWeapon.isReloading) {
                currentWeapon.isReloading = true;
                gunMagazine.Reload();
            }
        }
    }

    // -------------------------------------------------- //
    // Switching between weapons
    // -------------------------------------------------- //

    public void SelectWeapon() {
        int i = 0;
        foreach (Transform weaponHolderSlot in transform)
        {
            if (i == selectedWeapon) {
                weaponHolderSlot.gameObject.SetActive(true);
                // Check if weaponSlot is bought
                WeaponHolderSlot weaponHolderSlot_ = weaponHolderSlot.GetComponent<WeaponHolderSlot>();
                if (weaponHolderSlot_.isBought)
                    currentWeapon = weaponHolderSlot_.weapon;
            }
            else
                weaponHolderSlot.gameObject.SetActive(false);
            i++;    
        }
    }

    // -------------------------------------------------- //

    public void Recoil() {
        Vector3 direction = currentWeapon.firePoint.position - currentWeapon.gunButt.position;
        rb.AddForceAtPosition(-direction.normalized * currentWeapon.recoil, transform.position);
    }

}
