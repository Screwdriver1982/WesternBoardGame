using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankCloseCell : Cell
{
    public override void ActivateCell()
    {
        Player player = GameManager.Instance.WhoIsPlayer();

        int cashAdd = - player.loans;
        cashAdd -= player.startingLoan;
        cashAdd -= player.colonyLoan;
        player.loans = 0;
        player.colonyLoan = 0;
        player.startingLoan = 0;

        UIManager.Instance.ShowPaySmthWindow(cellIcon, cellTitle, cellDescription, cellWayTitle,
                                             cashAdd,
                                             0, 0, 0, 0, 0, 0, 0, null, null, 0, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, 0);
    }
}
