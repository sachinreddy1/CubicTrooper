using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gate : MonoBehaviour
{
    public float range = 5f;
    public GameObject GateUI;
    //
    private Transform player;
    private Animator shopUIAnimator;
    //
    public Animator gateAnimator;
    //
    private bool isOpen = false;
    private BuildManager buildManager;
    //
    public int Cost = 1000;
    public Text costText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        buildManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BuildManager>();
        shopUIAnimator = GateUI.GetComponent<Animator>();

        costText.text = "$" + Cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
    }

    void ShowUI() {
        Vector3 dir = player.position - transform.position;
        if ((Vector3.Distance(transform.position, player.position) <= range) && !isOpen) {
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

    public void OpenGate() {
        isOpen = true;
        gateAnimator.SetBool("isOpen", isOpen);
        buildManager.SpendMoney(Cost);
    }
}
