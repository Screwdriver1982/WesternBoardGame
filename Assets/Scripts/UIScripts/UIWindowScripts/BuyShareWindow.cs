using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyShareWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] Text boughtTxt;
    [SerializeField] GameObject buyButton;
    [SerializeField] Text costTxt;
    [SerializeField] GameObject ownerCap;
    [SerializeField] Image ownerCapColor;
    [SerializeField] Text okButtonTxt;
    [SerializeField] Shares pritonShare;
    [SerializeField] float lagTime;
    Player activePlayer;
    Player ownerTmp;
    PlayerMovement activePlayerMVMNT;
    Cell currCell;
    Shares shareTmp;
    int shareCost;

    public void OpenWindow(Player owner, Shares share, int cost)
    {
        print("Window owner = " + owner);
        activePlayer = GameManager.Instance.WhoIsPlayer();
        activePlayerMVMNT = GameManager.Instance.WhoIsPlayerMVMNT();
        currCell = activePlayerMVMNT.currentCell;
        ownerTmp = owner;

        print("owner = " + ownerTmp);
        shareTmp = share;
        shareCost = cost;


        ownerCap.SetActive(ownerTmp != null);
        boughtTxt.gameObject.SetActive(ownerTmp != null);
        buyButton.SetActive(ownerTmp == null);

        if (ownerTmp != null)
        {
            ownerCapColor.color = ownerTmp.playerColor;
            okButtonTxt.text = "Ok";

            if (ownerTmp == activePlayer)
            {
                boughtTxt.text = "Вы уже купили";
            }
            else
            {
                boughtTxt.text = "Уже куплено другим";
            }
        }
        else
        {
            if (cost > 0)
            {
                costTxt.text = "-" + cost + "$";
            }
            else
            {
                costTxt.text = "Бесплатно";
            }
        }
    }

    public void BuyButton()
    {
        if (activePlayer.cash - shareCost >= 0)
        {
            activePlayer.WalletChange(-shareCost, 0, 0, 0, 0, 0, 0, 0);
            GameManager.Instance.GiveShareToPlayer(activePlayer, shareTmp);

            if (shareTmp == pritonShare)
            {
                UIManager.Instance.ShowBuyDrugsWindow(currCell.cellIcon,
                                              currCell.cellTitle,
                                              currCell.cellDescription,
                                              currCell.cellWayTitle,
                                              300,
                                              50,
                                              "Drugs",
                                              "Other");
                UIManager.Instance.HideWindow(window);
            }
            else
            { 
                GameManager.Instance.NextPlayerTurn();
                UIManager.Instance.HideWindow(window);
            }
            
        }
        
    }


    public void OkButton()
    {
        if (shareTmp == pritonShare)
        {
            
            UIManager.Instance.ShowBuyDrugsWindow(currCell.cellIcon,
                                              currCell.cellTitle,
                                              currCell.cellDescription,
                                              currCell.cellWayTitle,
                                              300,
                                              50,
                                              "Drugs",
                                              "Other");
            UIManager.Instance.HideWindow(window);

        }
        else
        {
            GameManager.Instance.NextPlayerTurn();
            UIManager.Instance.HideWindow(window);
        }
    }


}
