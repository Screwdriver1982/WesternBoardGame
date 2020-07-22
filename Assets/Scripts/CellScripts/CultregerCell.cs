using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultregerCell : DrugsBuyCell
{
    public override bool CheckCellStartingChoose()
    {
        if (GameManager.Instance.WhoIsPlayerMVMNT().turnMiss > 0)
        {
            return false;

        }
        else
        { 
            UIManager.Instance.ShowCultregerWindow();
            return true;
        }
    }
}
