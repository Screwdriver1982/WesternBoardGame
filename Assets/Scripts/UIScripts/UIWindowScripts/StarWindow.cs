using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] GameObject starContainer; //если звезда не выбрана еще, то показывается
    [SerializeField] GameObject choseContainer; //если звезда выбрана, то показывается
    [SerializeField] Text costWoolfy;
    [SerializeField] Text costRabby;
    [SerializeField] Text chosenStarName;

    int cost;
    int activeStarNum;


    public void OpenWindow(int starCost, int starNumber)
    {
        cost = starCost;
        activeStarNum = starNumber;
        print("OpenWindow starNum " + activeStarNum);
        starContainer.SetActive(activeStarNum==0);
        choseContainer.SetActive(activeStarNum != 0);

        if (activeStarNum == 0) //звезда не выбрана
        {
            
            costWoolfy.text = cost + "$";
            costRabby.text = cost + "$";

        }
        else //т.е. звезда уже выбрана
        {

            if (activeStarNum == 1)
            {
                chosenStarName.text = "WOOLFY";
            }
            else
            {
                chosenStarName.text = "RABBY";
            }
        }

    }

    public void WoolfyButton()
    {
        Player activePlayer = GameManager.Instance.WhoIsPlayer();
        if (cost + activePlayer.cash >= 0)
        {
            activePlayer.WalletChange(cost, 0, 0, 0, 0, 0, 0, 0);
            activePlayer.ChangeCards(0, 0, 0, 1, 0, 0, 0, 0,0);
            GameManager.Instance.NextPlayerTurn();
            UIManager.Instance.HideWindow(window);
        }
    }
    public void RabbyButton()
    {
        Player activePlayer = GameManager.Instance.WhoIsPlayer();
        if (cost + activePlayer.cash >= 0)
        {
            activePlayer.WalletChange(cost, 0, 0, 0, 0, 0, 0, 0);
            activePlayer.ChangeCards(0, 0, 0, 0, 1, 0, 0, 0,0);
            GameManager.Instance.NextPlayerTurn();
            UIManager.Instance.HideWindow(window);
        }
    }

    public void OkButton()
    {
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }


}
