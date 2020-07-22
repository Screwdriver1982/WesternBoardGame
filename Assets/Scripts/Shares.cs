using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "NewShare")]
public class Shares : ScriptableObject
{
    [Header("Офомление")]
    [TextArea(minLines: 1, maxLines: 2)] public string shareTitle;
    [TextArea(minLines: 10, maxLines: 20)] public string shareDescription;
    public Sprite icon;
    
    [Header("Тип акции")]
    public string typeOfShares;
    
    [Header("Стоимость и доход")]
    public int cost;
    public float revenuePercent;
    public int revenueFix;
    public int goldRevenueFix;
    public int carsFromCapital;
    public int colaFromCapital;
}
