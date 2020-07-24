using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChancCell : Cell
{
    public override void ActivateCell()
    {
        UIManager.Instance.ShowChanceWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
    }
}
