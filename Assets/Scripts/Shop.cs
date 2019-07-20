using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    //
    private Transform player;
    private Transform weaponTransform;
    private weaponSwitching weaponHolder;
    private Animator shopUIAnimator;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        GameObject weaponHolder_ = GameObject.FindGameObjectWithTag("weaponController");
        // Needed for accessing the currentWeapons
        weaponTransform = weaponHolder_.GetComponent<Transform>();
        // Needed for activating/deactivating the Shooting
        weaponHolder = weaponHolder_.GetComponent<weaponSwitching>();
        
        shopUIAnimator = shopUI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
    }

    void ShowUI() {
        Vector3 dir = player.position - transform.position;
        if (Vector3.Distance(transform.position, player.position) <= 4f) {
            weaponHolder.canShoot = false;
            shopUIAnimator.SetBool("inRange", true);
        }
        else {
            weaponHolder.canShoot = true;
            shopUIAnimator.SetBool("inRange", false);
        }
    }    

}
