using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUI : MonoBehaviour
{
    private weaponSwitching weaponHolder;
    
    void Start()
    {
        weaponSwitching weaponHolder = GameObject.FindGameObjectWithTag("weaponController").GetComponent<weaponSwitching>();
    }

    void OnMouseEnter() {
        if(EventSystem.current.IsPointerOverGameObject()){
            Debug.Log("Entered");
            weaponHolder.canShoot = false;
            return;
        }
    }

    void OnMouseExit() {
        Debug.Log("Exited");
        weaponHolder.canShoot = true;
    }
}
