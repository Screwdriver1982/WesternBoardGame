using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DonkeyWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] List<Player> competitors;
    [SerializeField] Text cashTxt;

    Player activePlayer;
    int cash;

    public void OpenWindow(Player compet_1, Player compet_2, Player compet_3, int cashSum)
    {
        activePlayer = GameManager.Instance.WhoIsPlayer();
        cash = cashSum;
        cashTxt.text = cash + "$";
        if (compet_1 != null)
        {
            competitors.Add(compet_1);
        }
        if (compet_2 != null)
        {
            competitors.Add(compet_2);
        }
        if (compet_3 != null)
        {
            competitors.Add(compet_3);
        }

    }

    public void OkButton()
    {
        int cashForCompetitor = Mathf.FloorToInt(cash / competitors.Count); 
        foreach (Player competitor in competitors)
        {
            competitor.WalletChange(-cashForCompetitor, 0, 0, 0, 0, 0, 0, 0);
        }
        activePlayer.WalletChange(cash, 0, 0, 0, 0, 0, 0, 0);
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }
}
