using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    public Action onCellTurnEnded = delegate { };
    

    [Header("Куда идешь при прохождении")]
    public Cell passRight;
    public Cell passLeft;
    public Cell passUp;
    public Cell passDown;

    [Header("Куда идешь при старте движения")]
    public Cell startMovingRight;
    public Cell startMovingLeft;
    public Cell startMovingUp;
    public Cell startMovingDown;

    [Header("Альтернативный путь есть?")]
    public bool alternativeWay;
    public bool colonyCircleCounter; //клетка считает круги в колонии (ставится на клетку 130)
    public bool colonyCircle; // означает, что это круг колонии и в нем не нужно снимать отметку альтернативного пути


    [Header("Альтернативный путь при прохождении")]
    public Cell passRightA;
    public Cell passLeftA;
    public Cell passUpA;
    public Cell passDownA;

    [Header("Альтернативный путь при старте движения")]
    public Cell startMovingRightA;
    public Cell startMovingLeftA;
    public Cell startMovingUpA;
    public Cell startMovingDownA;





    [Header("Оформление клетки")]
    public Sprite cellIcon;
    public string cellTitle;
    [TextArea(minLines: 10, maxLines: 20)] public string cellDescription;
    public TypesOfWays way;
    public string cellWayTitle;


   [Header("Вид на клетку")]
    public TypesOfView cellView;
    public Vector3 view;

    [Header("Положения плееров на клетке")]
    [SerializeField] public GameObject redPosition;
    [SerializeField] public GameObject bluePosition;
    [SerializeField] public GameObject greenPosition;
    [SerializeField] public GameObject yellowPosition;

    int direction;

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
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            UIManager.Instance.ShowDescriptionWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
        }
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


        cellWayTitle = "BUSINESS";
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

    }

    //используется в свободной камере, чтобы знать как она стоит, чтобы правильно обрабатывать движение
    public int WhatIsRotation()
    {
        
        if (cellView == TypesOfView.FROM_DOWN)
        {
            direction = 0;
        }
        else if (cellView == TypesOfView.FROM_RIGHT)
        {
            direction = 1;
        }
        else if (cellView == TypesOfView.FROM_LEFT)
        {
            direction = 2;
        }
        else if (cellView == TypesOfView.FROM_UP)
        {
            direction = 3;
        }
        return direction;
    }

    public virtual void ActivateCell()
    {
        GameManager.Instance.NextPlayerTurn();
    }

    public virtual bool CheckCellStartingChoose()
    {
        return false;
    }
}
