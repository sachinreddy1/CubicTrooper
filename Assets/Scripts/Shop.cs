using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    private Animator shopUI_;
    //
    private Transform player;
    private Transform weapons;
    private weaponSwitching weapons_;
    //
    public GameObject weaponHolder;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //
        weapons = weaponHolder.GetComponent<Transform>();
        weapons_ = weaponHolder.GetComponent<weaponSwitching>();
        //
        shopUI_ = shopUI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
    }

    void ShowUI() {
        Vector3 dir = player.position - transform.position;
        if (Vector3.Distance(transform.position, player.position) <= 4f) {
            weapons_.canShoot = false;
            shopUI_.SetBool("inRange", true);
        }
        else {
            weapons_.canShoot = true;
            shopUI_.SetBool("inRange", false);
        }
    }

    public void AddAmmo(int weaponNumber) {
        int i = 0;
        foreach (Transform weapon in weapons)
        {
            if (i == weaponNumber) {
                Weapon temp = weapon.GetComponent<Weapon>();
                temp.remainingBullets += 10;
                PlayerStats.Money -= temp.ammoCost;
            }
            i++;
        }
    }

}
