using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCardCell : Cell
{
    [Header("Что должен заплатить и какие карточки получить")]
    [SerializeField] int cashCost; //если платит игрок, то должна быть отрицательной
    [SerializeField] bool boss;
    [SerializeField] bool police;
    [SerializeField] bool army;
    [SerializeField] bool woolfy;
    [SerializeField] bool rabby;

    public override void ActivateCell()
    {
        UIManager.Instance.ShowCardBuyWindow(cellIcon,
                                             cellTitle,
                                             cellDescription,
                                             cellWayTitle,
                                             cashCost,
                                             boss,
                                             police,
                                             army,
                                             woolfy,
                                             rabby);
    }
}
