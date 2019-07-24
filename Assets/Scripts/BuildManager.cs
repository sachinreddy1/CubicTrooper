using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public GameObject buildEffect;
    //
    public Transform player;
    public Transform weaponTransform;
    public weaponSwitching weaponHolder;
    //
    public Animator spendMoneyAnimator;
    public Text spendMoney;
    public Text moneyText;
    //
    
    void Start () {
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }

    public void SpendMoney(int moneySpent) {
        spendMoney.text = "-$" + moneySpent.ToString();

        StartCoroutine(DecreaseMoneyAnimateText(moneySpent));

        StartCoroutine(SpendMoneyAnimation());
    }

    IEnumerator DecreaseMoneyAnimateText(int moneySpent) {
        moneyText.text = "$" + PlayerStats.Money.ToString();
        int finalAmount = PlayerStats.Money - moneySpent;

        yield return new WaitForSeconds(.25f);

        while (finalAmount < PlayerStats.Money) {
            PlayerStats.Money--;
            moneyText.text = "$" + PlayerStats.Money.ToString();
            yield return new WaitForSeconds(.000001f);
        }
    }

    IEnumerator SpendMoneyAnimation()
    {
        spendMoneyAnimator.SetBool("IsActive", true);
        yield return new WaitForSeconds(1.25f);
        spendMoneyAnimator.SetBool("IsActive", false);
    }

}
