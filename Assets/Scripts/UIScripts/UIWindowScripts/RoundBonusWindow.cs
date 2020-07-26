using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundBonusWindow : MonoBehaviour
{
    [Header("Показ окна")]
    [SerializeField] float refreshLag = 0.3f;

    [SerializeField] CanvasGroup window;
    [SerializeField] Text shareRev;
    [SerializeField] Text depositeRev;
    [SerializeField] Text roundBonus;
    [SerializeField] Text creditsTax;
    [SerializeField] Text colonialLoan;
    [SerializeField] Text goldRev;
    [SerializeField] Text oilRev;
    [SerializeField] Text carsRev;
    [SerializeField] Text colaRev;
    [SerializeField] Text corpIndex;
    [SerializeField] Text totalCashRev;
    [SerializeField] Color greenColor;
    [SerializeField] Color redColor;

    Player player;

    public void OpenWindow()
    {
        player = GameManager.Instance.WhoIsPlayer();
        StartCoroutine(RefreshDataCoroutine(refreshLag));
    }

    IEnumerator RefreshDataCoroutine(float refreshLag)
    {
        yield return new WaitForSeconds(refreshLag);
        ShowData();
    }

    public void ShowData()
    {
        shareRev.text = player.sharesRevenue +"$";
        depositeRev.text = player.depositeRevenue + "$";
        roundBonus.text = GameManager.Instance.roundBaseMoney + "$";
        creditsTax.text = player.loanTax + "$";
        colonialLoan.text = player.colonyLoanReturn + "$";
        totalCashRev.text = player.totalCashRevenue + "$";
        if (player.totalCashRevenue > 0)
        {
            totalCashRev.color = greenColor;
        }
        else if(player.totalCashRevenue == 0)
        {
            totalCashRev.color = Color.black;
        }
        else if (player.totalCashRevenue <0)
        {
            totalCashRev.color = redColor;
        }

        goldRev.text = player.goldRevenue + " кг";
        oilRev.text = player.oilRevenue + " бар.";
        carsRev.text = player.carsRevenue + " шт";
        colaRev.text = player.colaRevenue + " боч.";
        if (player.stockExchangeIndex > 0)
        {
            corpIndex.text = player.stockExchangeIndex + " пункт";
            corpIndex.color = greenColor;
        }
        else
        {
            corpIndex.text = "без изменений";
            corpIndex.color = Color.black;
        }

    }

    public void OkButton()
    {
        UIManager.Instance.HideWindow(window);
    }
}
