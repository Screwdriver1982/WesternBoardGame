using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cell : MonoBehaviour
{
    public Action onCellTurnEnded = delegate { };


    [Header("Куда идешь при прохождении")]
    public int passRight;
    public int passLeft;
    public int passUp;
    public int passDown;

    [Header("Куда идешь при старте движения")]
    public int startMovingRight;
    public int startMovingLeft;
    public int startMovingUp;
    public int startMovingDown;

    [Header("Оформление клетки")]
    [SerializeField] Sprite cellIcon;
    [SerializeField] string cellTitle;
    [TextArea(minLines: 10, maxLines: 20)] [SerializeField] string cellDescription;
    [SerializeField] TypesOfWays way;


    [Header("Вид на клетку")]
    public TypesOfView cellView;
    public Vector3 view;

    [Header("Положения плееров на клетке")]
    [SerializeField] public GameObject redPosition;
    [SerializeField] public GameObject bluePosition;
    [SerializeField] public GameObject greenPosition;
    [SerializeField] public GameObject yellowPosition;

    

    public enum TypesOfWays
    {
        BUSINESS,
        CRIMINAL,
        COLONIAL,
        KLONDIKE
    }

    public enum TypesOfView
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

        string cellWayTitle = "BUSINESS";
        if (way == TypesOfWays.CRIMINAL)
        {
            cellWayTitle = "CRIMINAL";
        }
        else if (way == TypesOfWays.COLONIAL)
        {
            cellWayTitle = "COLONIAL";
        }
        else if (way == TypesOfWays.KLONDIKE)
        {
            cellWayTitle = "KLONDIKE";
        }

        UIManager.Instance.ShowDescriptionWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
    }

    private void Start()
    {
        if (cellView == TypesOfView.FROM_DOWN)
        {
            view = Vector3.zero;
        }
        else if (cellView == TypesOfView.FROM_RIGHT)
        {
            view = new Vector3(0, -90f, 0);
        }
        else if (cellView == TypesOfView.FROM_LEFT)
        {
            view = new Vector3(0, 90f, 0);
        }
        else if (cellView == TypesOfView.FROM_UP)
        {
            view = new Vector3(0, 180f, 0);
        }
    }
}
