using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    public float range = 5f;
    public GameObject shopUI;
    //
    private Transform player;
    private Transform weaponTransform;
    private weaponSwitching weaponHolder;
    private Animator shopUIAnimator;
    //
    public Transform contentTransform;

    #region Singleton

    public static Shop instance;

    void Awake () {
        if (instance != null)
        {
            Debug.LogWarning("Multiple Shop instances.");
            return;
        }
        
        instance = this;
    }

    #endregion

    public delegate void OnShopUsed();
    public OnShopUsed OnShopUsedCallback;
    

    // Start is called before the first frame update
    void Start()
    {
        OnShopUsedCallback += UpdateUI;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        GameObject weaponHolder_ = GameObject.FindGameObjectWithTag("weaponController");
        // Needed for accessing the currentWeapons
        weaponTransform = weaponHolder_.GetComponent<Transform>();
        // Needed for activating/deactivating the Shooting
        weaponHolder = weaponHolder_.GetComponent<weaponSwitching>();
        
        shopUIAnimator = shopUI.GetComponent<Animator>();

        if (OnShopUsedCallback != null)
            OnShopUsedCallback.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
    }

    void ShowUI() {
        Vector3 dir = player.position - transform.position;
        if (Vector3.Distance(transform.position, player.position) <= range) {
            shopUI.SetActive(true);
            shopUIAnimator.SetBool("inRange", true);

            if (OnShopUsedCallback != null)
                OnShopUsedCallback.Invoke();
        }
        else {
            shopUIAnimator.SetBool("inRange", false);
            StartCoroutine(DisableAfterTime(0.2f));
        }
    }

    // ---------------------------------------------------------- //

    IEnumerator DisableAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        shopUI.SetActive(false);
    }

    // ---------------------------------------------------------- //

    void UpdateUI () {
        // Set weapon to active or disabled based on weapons in inventory
        for (int i = 0; i < weaponHolder.gameObject.transform.childCount; i++)
        {
            // Get the UI slot
            WeaponUpgradeSlot upgradeSlot = contentTransform.GetChild(i).gameObject.GetComponent<WeaponUpgradeSlot>();
            // Get slot from weaponHolder
            WeaponHolderSlot weaponHolderSlot = weaponTransform.GetChild(i).GetComponent<WeaponHolderSlot>();

            upgradeSlot.weaponHolderSlot = weaponHolderSlot;
            upgradeSlot.weaponNumber.text = (i+1).ToString();
            upgradeSlot.UpdateSlot();
        }
    }

}
