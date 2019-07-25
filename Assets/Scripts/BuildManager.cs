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
    private int moneyToSpend = 0;
    private int moneyToSpend_ = 0;
    //
    private int moneyToCollect = 0;
    private int moneyToCollect_ = 0;
    
    void Start () {
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }

    // ---------------------------------------------------------- //

    public void SpendMoney(int moneySpent) {
        moneyToSpend += moneySpent;
        moneyToSpend_ += moneySpent;
        spendMoney.text = "-$" + moneyToSpend_.ToString();

        StartCoroutine(DecreaseMoneyAnimateText());
        StartCoroutine(SpendMoneyAnimation());
    }

    public void CollectMoney(int moneyCollect) {
        moneyToCollect += moneyCollect;
        moneyToCollect_ += moneyCollect;
        spendMoney.text = "+$" + moneyToCollect_.ToString();

        StartCoroutine(IncreaseMoneyAnimateText());
        StartCoroutine(SpendMoneyAnimation());
    }

    // ---------------------------------------------------------- //

    IEnumerator DecreaseMoneyAnimateText() {
        yield return new WaitForSeconds(.25f);

        while (0 < moneyToSpend) {
            PlayerStats.Money--;
            moneyToSpend--;

            moneyText.text = "$" + PlayerStats.Money.ToString();
            yield return new WaitForSeconds(.000001f);
        }
        moneyToSpend_ = 0;
    }

    IEnumerator SpendMoneyAnimation()
    {
        spendMoneyAnimator.SetBool("IsActive", true);
        yield return new WaitForSeconds(1.25f);
        spendMoneyAnimator.SetBool("IsActive", false);
    }

    // ---------------------------------------------------------- //

    IEnumerator IncreaseMoneyAnimateText() {
        yield return new WaitForSeconds(.25f);

        while (0 < moneyToCollect) {
            PlayerStats.Money++;
            moneyToCollect--;

            moneyText.text = "$" + PlayerStats.Money.ToString();
            yield return new WaitForSeconds(.000001f);
        }
        moneyToCollect_ = 0;
    }


}
