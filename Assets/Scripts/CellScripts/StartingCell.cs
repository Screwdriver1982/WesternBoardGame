using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCell : Cell
{
    public override bool CheckCellStartingChoose()
    {
        if (GameManager.Instance.WhoIsPlayerMVMNT().turnMiss > 0)
        {
            return false;

        }
        else
        {
            UIManager.Instance.ShowUnemployWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
            return true;
        }


    }
}
