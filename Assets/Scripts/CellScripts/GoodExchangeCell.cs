using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodExchangeCell : Cell
{
    [Header("Товары")]
    [Tooltip("Золото, Нефть, Авто, Кола")][SerializeField] int[] goodsMaxNumber = { 100, 100, 100, 100};
    // Start is called before the first frame update
    public override void ActivateCell()
    {
        UIManager.Instance.ShowGoodsExchangeWindow(goodsMaxNumber[0], goodsMaxNumber[1], goodsMaxNumber[2], goodsMaxNumber[3]);
    }
}
