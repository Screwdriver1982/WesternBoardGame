using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraTurnCell: Cell
{
    public override void ActivateCell()
    {
        UIManager.Instance.ShowExtraTurnWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
    }
}

