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

    public void SpendMoney(int moneySpent) {
        spendMoney.text = "-$" + moneySpent.ToString();

        spendMoneyAnimator.SetBool("IsActive", true);
        StartCoroutine(TimeWait());
        StartCoroutine(SpendMoneyAnimateText(moneySpent));
    }

    IEnumerator SpendMoneyAnimateText(int moneySpent) {
        moneyText.text = "$" + PlayerStats.Money.ToString();
        int finalAmount = PlayerStats.Money - moneySpent;

        yield return new WaitForSeconds(.25f);

        while (finalAmount < PlayerStats.Money) {
            PlayerStats.Money--;
            moneyText.text = "$" + PlayerStats.Money.ToString();
            yield return new WaitForSeconds(.000001f);
        }
    }

    IEnumerator TimeWait()
    {
        yield return new WaitForSeconds(1.25f);
        spendMoneyAnimator.SetBool("IsActive", false);
    }

}
