using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareUI : MonoBehaviour
{
    [SerializeField] Image shareImage;
    [SerializeField] Text shareTitle;
    [SerializeField] Text shareCostTxt;
    int cost;
    public float coef;
    [SerializeField] Player player;
    [SerializeField] Shares share;

    public void Initialize(Shares shareInit, float coefInit)
    {

        share = shareInit;
        shareImage.sprite = share.icon;
        shareTitle.text = share.shareTitle;
        coef = coefInit;
        print("coef: " + coef);
        if (share.typeOfShares == "Corporation")
        {
            cost = Mathf.FloorToInt(GameManager.Instance.GiveCorporationPrice(share)* coef);
        }
        else
        {
            cost = Mathf.FloorToInt(share.cost * coef);
        }
        shareCostTxt.text = cost + "$";
        print(share + " cost: " + cost + "coef = "+coef);
    }

    public void SellButton()
    {
        print("sell coef: " + coef);
        GameManager.Instance.WhoIsPlayer().SellShare(share, coef);
        Destroy(this.gameObject);
    }


}
