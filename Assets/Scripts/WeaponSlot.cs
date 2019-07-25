using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Text weaponNumber;
    public Image weaponIcon;
    public Image upgradeIcon;

    public Text ammoLeft;

    public Animator animator;
    public Weapon weapon;

    public void Toggle(bool val) {
        animator.SetBool("isEnabled", val);
    }

    public void UpdateSlot() {
        weaponIcon.sprite = weapon.weaponIcon;
        ammoLeft.text = weapon.remainingBullets.ToString();
    }

}
