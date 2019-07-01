using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Weapon weapon;

    public Text weaponNumber;
    public Sprite weaponIcon;

    public Animator animator;

    public void Toggle(bool val) {
        animator.SetBool("isEnabled", val);
    }

    public void UpdateSlot() {
        weaponNumber.text = weapon.weaponNumber.ToString();
        weaponIcon = weapon.weaponIcon;
    }

}
