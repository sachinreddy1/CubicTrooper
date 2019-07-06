using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int weaponNumber;
    //
    public Sprite weaponIcon;
    //
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform gunButt;
    //

    public int magCapacity = 5;
    public float reloadTime = 50f;
    public float recoil = 50f;
    //

    public int bullets;
    public bool isReloading = false;

}
