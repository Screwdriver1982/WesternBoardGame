using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffCell : Cell
{
    public override void ActivateCell()
    {
        UIManager.Instance.ShowRiffWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
    }
}
