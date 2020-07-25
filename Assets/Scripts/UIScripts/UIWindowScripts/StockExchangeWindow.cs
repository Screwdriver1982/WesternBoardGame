using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockExchangeWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] Text[] corporationPrices;
    [SerializeField] Text[] priceGoods;
    [SerializeField] Text[] priceGoodsPoint;
    

    public void OpenWindow()
    {
        for (int i = 0; i < corporationPrices.Length; i++)
        {
            corporationPrices[i].text = GameManager.Instance.corpCosts[i] + "$";
        }

        for (int j = 0; j < priceGoods.Length; j++)
        {
            priceGoods[j].text = GameManager.Instance.goodsCost[j] + "$";
            priceGoodsPoint[j].text = GameManager.Instance.goodsPointCost[j] + "$";
        }
    }

    public void OkButton()
    {
        UIManager.Instance.HideWindow(window);
    }

}
