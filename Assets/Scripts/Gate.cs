using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gate : MonoBehaviour
{
    public float range = 5f;
    public GameObject GateUI;
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
        
        shopUIAnimator = GateUI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
    }

    void ShowUI() {
        Vector3 dir = player.position - transform.position;
        if (Vector3.Distance(transform.position, player.position) <= range) {
            GateUI.SetActive(true);
            shopUIAnimator.SetBool("inRange", true);
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
        GateUI.SetActive(false);
    }
}
