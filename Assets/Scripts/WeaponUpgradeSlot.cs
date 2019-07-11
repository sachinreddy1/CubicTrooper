using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeSlot : MonoBehaviour
{
    public Weapon weapon;
    //
    public Text weaponNumber;
    public Image weaponIcon;
    public Text upgradeCost;
    public Text ammoCost;

    // Start is called before the first frame update
    void Start()
    {
        weaponNumber.text = weapon.weaponNumber.ToString() + ".";
        weaponIcon.sprite = weapon.weaponIcon;
        upgradeCost.text = "$" + weapon.upgradeCost.ToString();
        ammoCost.text = "$" + weapon.ammoCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
