using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeculationExchangeCell : Cell
{
    [SerializeField] float speculationCoef;
    public override void ActivateCell()
    {
        UIManager.Instance.ShowSpeculationExchangeWindow(speculationCoef);
    }
}
