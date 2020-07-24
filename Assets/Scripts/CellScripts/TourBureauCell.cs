using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourBureauCell : Cell
{
    public override void ActivateCell()
    {
        UIManager.Instance.ShowTourBureauWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
    }
}
