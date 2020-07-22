using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankCell : Cell
{
    public override void ActivateCell()
    {
        UIManager.Instance.ShowBankWindow();
    }
}
