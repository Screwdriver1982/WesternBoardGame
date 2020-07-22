using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCardWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] Text cardCostTxt;
    int cost=0;
    int bossC = 0;
    int policeC = 0;
    int armyC = 0;
    int woolfyC = 0;
    int rabbyC = 0;

    public void OpenWindow(int cardCost, bool boss, bool police, bool army, bool woolfy, bool rabby)
    {
        cost = cardCost;
        if (boss)
        {
            bossC = 1;
        }
        else
        {
            bossC = 0;
        }

        if (police)
        {
            policeC = 1;
        }
        else
        {
            policeC = 0;
        }

        if (army)
        {
            armyC = 1;
        }
        else
        {
            armyC = 0;
        }

        if (woolfy)
        {
            woolfyC = 1;
        }
        else
        {
            woolfyC = 0;
        }

        if (rabby)
        {
            rabbyC = 1;
        }
        else
        {
            rabbyC = 0;
        }


        cardCostTxt.text = "" + cost + "$";
        cardCostTxt.color = Color.red;

    }


    public void BuyButton()
    {
        if (GameManager.Instance.WhoIsPlayer().cash +cost >0)
        {
            GameManager.Instance.ChangePlayerWallet(cost, 0, 0, 0, 0, 0, 0, 0);
            GameManager.Instance.ChangePlayerCards(bossC, policeC, armyC, woolfyC, rabbyC, 0,0,0,0 );
            GameManager.Instance.NextPlayerTurn();
            UIManager.Instance.HideWindow(window);
        } 
    }

    public void DontBuyButton()
    {
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }
}
