using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCell : Cell
{
    // Start is called before the first frame update
    public override void ActivateCell()
    {
        GameManager.Instance.SwitchCameraToFreeCamera();
        UIManager.Instance.ShowPlanePanel();
    }
}
