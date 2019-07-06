using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Weapon currentWeapon;
    //
    private GunMagazine gunMagazine;
    //
    public GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        gunMagazine = GunMagazine.instance;

        SelectWeapon();
        foreach (Transform weapon in transform)
        {
            Weapon curr_weapon = weapon.GetComponent<Weapon>();
            curr_weapon.bullets = curr_weapon.magCapacity;
        }

        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameEnded) 
            return;
            
        // Left click pressed (Shoot)
        if (Input.GetMouseButtonDown(0)) {
            if (!currentWeapon.isReloading)
                Shoot();
        }

        // Right click pressed (Reload)
        if (Input.GetMouseButtonDown(1)) {
            if (!currentWeapon.isReloading) {
                currentWeapon.isReloading = true;
                gunMagazine.Reload();
            }
        }
    }

    public void SelectWeapon() {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon) {
                weapon.gameObject.SetActive(true);
                currentWeapon = weapon.GetComponent<Weapon>();
            }
            else
                weapon.gameObject.SetActive(false);
            i++;    
        }
    }

    void Shoot() {
        if (currentWeapon.bullets > 0) {
            Instantiate(currentWeapon.bulletPrefab, currentWeapon.firePoint.position, currentWeapon.firePoint.rotation);
            Recoil();
            currentWeapon.bullets--;
        } else {
            if (!currentWeapon.isReloading) {
                currentWeapon.isReloading = true;
                gunMagazine.Reload();
            }
        }

        if (gunMagazine.OnWeaponUsedCallback != null)
            gunMagazine.OnWeaponUsedCallback.Invoke();
    }
    // -------------------------------------------------- //

    public void Reload() {
        currentWeapon.bullets = currentWeapon.magCapacity;
        currentWeapon.isReloading = false;

        if (gunMagazine.OnWeaponUsedCallback != null)
            gunMagazine.OnWeaponUsedCallback.Invoke();
    } 

    void Recoil() {
        // Recoil in the opposite direction of shoot
        Vector3 direction = currentWeapon.firePoint.position - currentWeapon.gunButt.position;
        rb.AddForceAtPosition(-direction.normalized * currentWeapon.recoil, transform.position);
    }

}
