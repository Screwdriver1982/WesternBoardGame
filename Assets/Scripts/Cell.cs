using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System

public class Cell : MonoBehaviour
{
    public Action onCellTurnEnded = delegate { };


    [Header("Куда идешь при прохождении")]
    [SerializeField] int passRight;
    [SerializeField] int passLeft;
    [SerializeField] int passUp;
    [SerializeField] int passDown;

    [Header("Куда идешь при старте движения")]
    [SerializeField] int startMovingRight;
    [SerializeField] int startMovingLeft;
    [SerializeField] int startMovingUp;
    [SerializeField] int startMovingDown;

    [Header("Оформление клетки")]
    [SerializeField] Sprite cellIcon;
    [SerializeField] string cellTitle;
    [TextArea(minLines: 10, maxLines: 20)] [SerializeField] string cellDescription;
    [SerializeField] TypesOfWays way;


    [Header("Вид на клетку")]
    [SerializeField] TypesOfView cellVeiw;

    [Header("Положения плееров на клетке")]
    [SerializeField] GameObject redPosition;
    [SerializeField] GameObject bluePosition;
    [SerializeField] GameObject greenPosition;
    [SerializeField] GameObject yellowPosition;

    

    public enum TypesOfWays
    {
        BUSINESS,
        CRIMINAL,
        COLONIAL,
        KLONDIKE
    }

    enum TypesOfView
    { 
        FROM_DOWN,
        FROM_RIGHT,
        FROM_LEFT,
        FROM_UP

    }

    public void TurnEndedOnCell()
    {
        onCellTurnEnded();
    }

    private void OnMouseDown()
    {
        //TODO открыть инфо окно
    }


}
