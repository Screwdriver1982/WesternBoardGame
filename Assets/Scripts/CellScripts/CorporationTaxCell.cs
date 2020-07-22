using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporationTaxCell : Cell
{
    [Header("Кто должен заплатить")]
    [SerializeField] Shares eMA;
    [SerializeField] Shares mMM;
    [SerializeField] Shares bamper;
    [SerializeField] float taxRate;
    int cashAdd;

    public override void ActivateCell()
    {
        cashAdd = 0;
        Player emaOwner = GameManager.Instance.GetShareOwner(eMA);
        Player mMMOwner = GameManager.Instance.GetShareOwner(mMM);
        Player bamperOwner = GameManager.Instance.GetShareOwner(bamper);

        if (emaOwner == GameManager.Instance.WhoIsPlayer())
        {

            cashAdd -= Mathf.FloorToInt(GameManager.Instance.GiveCorporationPrice(eMA) * taxRate);
        }

        if (mMMOwner == GameManager.Instance.WhoIsPlayer())
        {

            cashAdd -= Mathf.FloorToInt(GameManager.Instance.GiveCorporationPrice(mMM) * taxRate);
        }

        if (bamperOwner == GameManager.Instance.WhoIsPlayer())
        {

            cashAdd -= Mathf.FloorToInt(GameManager.Instance.GiveCorporationPrice(bamper) * taxRate);
        }

        //UIManager.Instance.ShowPaySmthWindow(cellIcon,
        //                                     cellTitle,
        //                                     cellDescription,
        //                                     cellWayTitle,
        //                                     cashAdd,
        //                                     0,
        //                                     0,
        //                                     0,
        //                                     0,
        //                                     0,
        //                                     0,
        //                                     0,
        //                                     null);
    }
}
