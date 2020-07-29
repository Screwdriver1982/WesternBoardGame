using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnerMissTurnCell : Cell
{
    [SerializeField] Shares share;
    [SerializeField] int missTurn;
    Player owner;
    public override void ActivateCell()
    {
        owner = GameManager.Instance.GetShareOwner(share);
        print("owner Cell = " + owner);
        UIManager.Instance.ShowSmbMissTurnWindow(cellIcon,
                                               cellTitle,
                                               cellDescription,
                                               cellWayTitle,
                                               owner,
                                               missTurn);
        print("correct ask UImanager ");
    }
}
