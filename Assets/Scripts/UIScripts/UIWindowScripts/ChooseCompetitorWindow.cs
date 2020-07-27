using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCompetitorWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] GameObject[] competitorContainers;
    [SerializeField] Image[] competitorImages;
    [SerializeField] Text[] pricesToKill;
    [SerializeField] Text chooseTxt;
    [SerializeField] int[] playerNumbers = { -1, -1, -1 };
    [SerializeField] Shares protezShare;
    Player activePlayer;
    Player boss;
    int killCostTemp;
    string typeOfWindowTemp;
    Cell cellToMoveTemp;
    int goodsChangesTemp;

    public void OpenWindow(int secondPlayerNum, 
                            int thirdPlayerNum, 
                            int fourthPlayerNum, 
                            int killCost, 
                            Cell cellToMove, 
                            string typeOfWindow, 
                            int goodsChanges)
    {
        activePlayer = GameManager.Instance.WhoIsPlayer();
        boss = GameManager.Instance.WhoIsBossPlayer();
        playerNumbers[0] = secondPlayerNum;
        playerNumbers[1] = thirdPlayerNum;
        playerNumbers[2] = fourthPlayerNum;
        typeOfWindowTemp = typeOfWindow;
        killCostTemp = killCost;
        cellToMoveTemp = cellToMove;
        goodsChangesTemp = goodsChanges;
        bool ifCompetitorsExist = false;


        for (int i = 0; i < 3; i++)
        {
            competitorContainers[i].SetActive(playerNumbers[i] != -1);
            ifCompetitorsExist = ifCompetitorsExist || (playerNumbers[i] != -1);
            if (playerNumbers[i] != -1)
            {
                
                competitorImages[i].color = GameManager.Instance.GetPlayerColorNum(playerNumbers[i]);
                pricesToKill[i].gameObject.SetActive(typeOfWindow == "Kill" && activePlayer !=boss);
            }

        }

        if (!ifCompetitorsExist)
        {
            chooseTxt.text = "Нет подходящих конкурентов";
        }

    }

    public void competitorButton(int buttonOrder)
    {
        switch (typeOfWindowTemp)
        {
            case "Kill":
              
              
                if (activePlayer.cash + killCostTemp >= 0)
                {
                 
                    activePlayer.WalletChange(killCostTemp, 0, 0, 0, 0, 0, 0, 0);
                    Player victim = GameManager.Instance.GetPlayerByPlayerNum(playerNumbers[buttonOrder]);

              

                    GameManager.Instance.MovePlayerToCellNum(playerNumbers[buttonOrder], cellToMoveTemp );

                 

                    if (victim.cash > 0)
                    {
                        victim.WalletChange(-Mathf.FloorToInt(victim.cash * 0.1f), 0, 0, 0, 0, 0, 0, 0);
                        Player owner = GameManager.Instance.GetShareOwner(protezShare);
                        if (owner != null)
                        { 
                            owner.WalletChange(Mathf.FloorToInt(victim.cash * 0.1f), 0, 0, 0, 0, 0, 0, 0);
                        }
                    }
                    
                    if (boss != null)
                    {
                        boss.WalletChange(-killCostTemp, 0, 0, 0, 0, 0, 0, 0);
                    }
                }
                break;

            case "Goods":
                GameManager.Instance.ChangeGoodsForPlayerNum(playerNumbers[buttonOrder], goodsChangesTemp);
                break;
        }

        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }

    public void OkButton()
    {
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }
}