using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMovementLeft : MonoBehaviour
{
    [SerializeField] Text movementLeft;
    [SerializeField] GameObject retireButton;
    [SerializeField] GameObject slaveryButton;
    [SerializeField] GameObject colonyMovementCost;
    [SerializeField] Text colonyMovementCostTxt;
    PlayerMovement playerMovement;

    public void LinkToPlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.onMovementDecrease -= MovementChange;
            playerMovement.onEnoughMoneyToMoveUpdate -= ShowRetireButton;
        }

        playerMovement = GameManager.Instance.WhoIsPlayerMVMNT();
        playerMovement.onMovementDecrease += MovementChange;
        playerMovement.onEnoughMoneyToMoveUpdate += ShowRetireButton;
        MovementChange();
        ShowRetireButton();
    }
    
    public void MovementChange()
    {
        movementLeft.text = "" + playerMovement.moveLeft;
        string way = playerMovement.currentCell.cellWayTitle;
        colonyMovementCost.SetActive(way =="COLONIAL" || way == "KLONDIKE");
        colonyMovementCostTxt.text = "Колонии " + GameManager.Instance.colonyMovementCost + "$";
    }

    public void ShowRetireButton()
    {
        slaveryButton.SetActive(!playerMovement.negativeMoney && !playerMovement.enoughMoney);
        retireButton.SetActive(playerMovement.negativeMoney);
    }

}
