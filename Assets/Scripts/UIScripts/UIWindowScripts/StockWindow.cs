using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] Text[] corporationPrices;
    [SerializeField] Text[] priceGoods;
    [SerializeField] Text[] priceGoodsPoint;


    public void OpenWindow()
    {
        print("enterFunction");
        for (int i = 0; i < corporationPrices.Length; i++)
        {
            print("do corp " + i);
            corporationPrices[i].text = GameManager.Instance.corpCosts[i] + "$";
        }

        for (int j = 0; j < priceGoods.Length; j++)
        {
            print("do goods " + j);
            priceGoods[j].text = GameManager.Instance.goodsCost[j] + "$";
            priceGoodsPoint[j].text = GameManager.Instance.goodsPointCost[j] + "$";
        }
    }

    public void OkButton()
    {
        UIManager.Instance.HideWindow(window);
    }
}
