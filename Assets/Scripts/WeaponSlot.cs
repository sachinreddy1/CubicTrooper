using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Weapon weapon;

    public Text weaponNumber;
    public Image weaponIcon;

    public Text ammoLeft;

    public Animator animator;

    public void Toggle(bool val) {
        animator.SetBool("isEnabled", val);
    }

    public void UpdateSlot() {
        weaponNumber.text = weapon.weaponNumber.ToString();
        weaponIcon.sprite = weapon.weaponIcon;
        ammoLeft.text = weapon.remainingBullets.ToString();
    }

}
