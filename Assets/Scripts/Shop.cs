using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    public Animator shopUI_;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
            // Disable Shooting
            shopUI_.SetBool("inRange", true);
        }
        else {
            // Enable Shooting
            shopUI_.SetBool("inRange", false);
        }
    }
}
