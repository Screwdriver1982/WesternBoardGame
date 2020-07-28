using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Action onMovementDecrease = delegate { };
    public Action onEnoughMoneyToMoveUpdate = delegate { };

    [SerializeField] PlayerColors playerColor;
    [SerializeField] Player player;

    [Header("В игре и может ходить?")]
    public bool inGame = true;
    public bool enoughMoney = true;
    public bool negativeMoney = false;

    [Header("Перемещение между клетками")]
    [SerializeField] float oneCellJumpTime;
    [SerializeField] float jumpPower = 3f;
    [SerializeField] float rotateTime = 1f;
    [SerializeField] float activateTime = 1.2f;

    [Header("Камера игрока")]
    public Camera playerCamera;

    [Header("Ходы и движения")]
    public int moveLeft = 6; //осталось движений в этом ходу
    public int turnMiss; // ходы которые нужно пропустить
    public int extraTurnNumber = 0; //доп.ходы

    [Header("Параметры текущего перемещения")]
    [SerializeField] bool playerTurn = true;
    [SerializeField] bool startMoveOrPass = true;
    [SerializeField] bool allowMovement;
    [SerializeField] bool dialogIsOpen = false;

    [Header("Флаги альтернативного движения")]
    public bool toColony = false; //альтернативный путь в колонии, отключается, если такой же флаг у клетки отсутствует
    public bool secondCircleColony = false; //счетчик кругов, есть клетка активатор, есть клетка, которая дает альтерн.путь если включен
    public bool reversMovement; //при начале следующего хода игрока сбросится в False, пока действует позволяет пользоваться альт.путем назад
    public Cell cellCrimeOne; //клетки при переходе с одной на другую включается secondCircleColony
    public Cell cellCrimeSecond;

    [Header("Клетка")]
    public Cell currentCell; //текущая клетка игрока
    [SerializeField] Cell roundBonusCell; //клетка при прохождении или остановке на которой игрок получает доход

    Vector3 startPosition;
    [SerializeField]bool firstCellPassed = false; //флаг отвечающий за то, какой раз игрок проходит стартовую клетку
    [SerializeField] int roundPassed = 0; //количество пройденных игроком кругов





    enum PlayerColors
    {
        RED,
        BLUE,
        GREEN,
        YELLOW

    }

    private void Start()
    {
        player.onWalletChanges += EnoughMoneyToMove;
        GoToCellWithoutActivation(currentCell);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) && playerTurn && !dialogIsOpen && allowMovement && moveLeft > 0 && enoughMoney)
        {
            GameManager.Instance.SwitchCameraToPlayer(this);

            if ((toColony || secondCircleColony || reversMovement) && currentCell.alternativeWay)
            {
                if (startMoveOrPass && currentCell.startMovingUpA != null)
                {

                    Cell nextCell = currentCell.startMovingUpA;

                    Movement(nextCell);

                }
                else if (!startMoveOrPass && currentCell.passUpA != null)
                {

                    Cell nextCell = currentCell.passUpA;
                    Movement(nextCell);


                }
            }
            else
            { 
                if (startMoveOrPass && currentCell.startMovingUp != null)
                {

                    Cell nextCell = currentCell.startMovingUp;
                    Movement(nextCell);



                }
                else if (!startMoveOrPass && currentCell.passUp != null)
                {

                    Cell nextCell = currentCell.passUp;
                    Movement(nextCell);







                }
            }


        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) && playerTurn && !dialogIsOpen && allowMovement && moveLeft > 0 && enoughMoney)
        {
            GameManager.Instance.SwitchCameraToPlayer(this);

            if ((toColony || secondCircleColony || reversMovement) && currentCell.alternativeWay)
            {
                if (startMoveOrPass && currentCell.startMovingDownA != null)
                {

                    Cell nextCell = currentCell.startMovingDownA;
                    Movement(nextCell);

                }
                else if (!startMoveOrPass && currentCell.passDownA != null)
                {

                    Cell nextCell = currentCell.passDownA;
                    Movement(nextCell);

                }
            }
            else
            {

                if (startMoveOrPass && currentCell.startMovingDown != null)
                {

                    Cell nextCell = currentCell.startMovingDown;
                    Movement(nextCell);



                }
                else if (!startMoveOrPass && currentCell.passDown != null)
                {

                    Cell nextCell = currentCell.passDown;
                    Movement(nextCell);







                }
            }


        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && playerTurn && !dialogIsOpen && allowMovement && moveLeft > 0 && enoughMoney)
        {
            GameManager.Instance.SwitchCameraToPlayer(this);
            if ((toColony || secondCircleColony || reversMovement) && currentCell.alternativeWay)
            {
                if (startMoveOrPass && currentCell.startMovingLeftA != null)
                {

                    Cell nextCell = currentCell.startMovingLeftA;
                    Movement(nextCell);

                }
                else if (!startMoveOrPass && currentCell.passLeftA != null)
                {


                    Cell nextCell = currentCell.passLeftA;
                    bool needRotation = IfNeedRotation(nextCell);
                    TakeMoneyForMovement();
                    currentCell = currentCell.passLeftA;
                    MoveNextCell(needRotation);

                }
            }
            else
            { 
                if (startMoveOrPass && currentCell.startMovingLeft != null)
                {

                    Cell nextCell = currentCell.startMovingLeft;
                    Movement(nextCell);



                }
                else if (!startMoveOrPass && currentCell.passLeft != null)
                {


                    Cell nextCell = currentCell.passLeft;
                    Movement(nextCell);







                }
            }

        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) && playerTurn && !dialogIsOpen && allowMovement && moveLeft > 0 && enoughMoney)
        {
            GameManager.Instance.SwitchCameraToPlayer(this);

            if ((toColony || secondCircleColony || reversMovement) && currentCell.alternativeWay)
            {
                if (startMoveOrPass && currentCell.startMovingRightA != null)
                {

                    Cell nextCell = currentCell.startMovingRightA;
                    Movement(nextCell);



                }
                else if (!startMoveOrPass && currentCell.passRightA != null)
                {

                    Cell nextCell = currentCell.passRightA;
                    bool needRotation = IfNeedRotation(nextCell);
                    TakeMoneyForMovement();
                    currentCell = currentCell.passRightA;
                    MoveNextCell(needRotation);

                }
            }
            else
            {
                if (startMoveOrPass && currentCell.startMovingRight != null)
                {

                    Cell nextCell = currentCell.startMovingRight;
                    Movement(nextCell);



                }
                else if (!startMoveOrPass && currentCell.passRight != null)
                {

                    Cell nextCell = currentCell.passRight;
                    Movement(nextCell);

                }
            }
            
        }
    }

    public void Movement(Cell nextCell)
    {
        bool needRotation = IfNeedRotation(nextCell);
        TakeMoneyForMovement();

        if ((currentCell == cellCrimeOne || currentCell == cellCrimeSecond)
            && (nextCell == cellCrimeOne || nextCell == cellCrimeSecond))
        {
            secondCircleColony = true;
        }

        currentCell = nextCell;
        MoveNextCell(needRotation);
    }


    public void JumpToCellAndActivateIt(Cell nextCell)
    {
        moveLeft = 1;
        bool needRotation = IfNeedRotation(nextCell);
        currentCell = nextCell;
        MoveNextCell(needRotation);
    }

    private bool IfNeedRotation(Cell nextCell)
    {
        bool needRotation;
        if (nextCell.cellView == currentCell.cellView)
        {
            needRotation = false;
        }
        else
        {
            needRotation = true;
        }
        return needRotation;
    }

    private void MoveNextCell(bool needRotation)
    {
        allowMovement = false;
        moveLeft -= 1;
        toColony = false;

        //если идет второй круг и игрок все еще на колониальном круге, то этот флаг остается, если ушел с круга, то выключится
        if (secondCircleColony)
        {
            secondCircleColony = currentCell.colonyCircle;
        }


        onMovementDecrease();
        if (moveLeft > 0)
        {
            startMoveOrPass = false;
        }
        else
        {
            startMoveOrPass = true;
            playerTurn = false;
        }


        if (needRotation)
        {
            transform.DORotate(currentCell.view, rotateTime).SetEase(Ease.InOutSine).OnComplete(JumpToCell);
        }
        else
        {
            JumpToCell();
        }
    }

    private void TakeMoneyForMovement()
    {
        if (currentCell.cellWayTitle == "COLONIAL" || currentCell.cellWayTitle == "KLONDIKE")
        {
            int colonyPrice = GameManager.Instance.colonyMovementCost;
            player.WalletChange(colonyPrice, 0, 0, 0, 0, 0, 0, colonyPrice);

        }
    }

    private void JumpToCell()
    {
        Vector3 newPosition = Vector3.zero;
        if (playerColor == PlayerColors.RED)
        {
            newPosition = currentCell.redPosition.transform.position;

        }
        else if (playerColor == PlayerColors.BLUE)
        {
            newPosition = currentCell.bluePosition.transform.position;

        }
        else if (playerColor == PlayerColors.GREEN)
        {
            newPosition = currentCell.greenPosition.transform.position;

        }
        else if (playerColor == PlayerColors.YELLOW)
        {
            newPosition = currentCell.yellowPosition.transform.position;

        }

        transform.DOJump(newPosition, jumpPower, 1, oneCellJumpTime).OnComplete(MovementEnded);
    }
    private void AllowMovement()
    {
        allowMovement = true;
    }







    public void MoveInactivePlayerToCell(Cell cellToMove)
    {
        toColony = false;
        bool needRotation = IfNeedRotation(cellToMove);
        print(needRotation);
        currentCell = cellToMove;
        if (needRotation)
        {
            transform.DORotate(currentCell.view, rotateTime).SetEase(Ease.InOutSine).OnComplete(() => GoToCellWithoutActivation(cellToMove));
        }
        else
        {
            GoToCellWithoutActivation(cellToMove);
        }

    }










    public void GiveTheTurnToPlayer(int moves)
    {
        playerCamera.gameObject.SetActive(true);
        GameManager.Instance.activeCamera = playerCamera;
        playerTurn = true;
        moveLeft = moves;
        EnoughMoneyToMove();


    }

    public void TakeTurnFromPlayer()
    {
        playerCamera.gameObject.SetActive(false);
        playerTurn = false;
    }

    void MovementEnded()
    {
        //если попадаем на клетку счетчика кругов в колонии, то включаем поход на второй круг
        if (currentCell.colonyCircleCounter)
        {
            secondCircleColony = true;
        }

        if (currentCell == roundBonusCell)
        {
            if (firstCellPassed)
            {
                roundPassed += 1;
                GameManager.Instance.GiveRoundBonusToActivePlayer();
                UIManager.Instance.ShowRoundBonusWindow();
                GameManager.Instance.IfLastRound(roundPassed);

            }
            else
            {
                firstCellPassed = true;
            }
        }

        if (moveLeft == 0)
        {
            reversMovement = false;
            ActivateCell(currentCell);
        }

        AllowMovement();
    }
    void ActivateCell(Cell cell)
    {
        cell.ActivateCell();
    }

    public int WhatIsDirection()
    {
        return currentCell.WhatIsRotation();

    }

    //проверяем есть ли какой-то начальный выбор или нет
    public bool CheckStartingChoose()
    {
        return currentCell.CheckCellStartingChoose() ;
    }

    public void GoToCell(Cell targetCell)
    {
        GoToCellWithoutActivation(targetCell);
        StartCoroutine(ActivateCellCoroutine(activateTime));
    }

    IEnumerator ActivateCellCoroutine(float activateTime)
    {
        yield return new WaitForSeconds(activateTime);
        ActivateCell(currentCell);
    }

    public void GoToCellWithoutActivation(Cell targetCell)
    {
        
        
        if (playerColor == PlayerColors.RED)
        {
            startPosition = targetCell.redPosition.transform.position;
            transform.position = startPosition;
        }
        if (playerColor == PlayerColors.BLUE)
        {
            startPosition = targetCell.bluePosition.transform.position;
            transform.position = startPosition;
        }
        if (playerColor == PlayerColors.GREEN)
        {
            startPosition = targetCell.greenPosition.transform.position;
            transform.position = startPosition;
        }
        if (playerColor == PlayerColors.YELLOW)
        {
            startPosition = targetCell.yellowPosition.transform.position;
            transform.position = startPosition;
        }

    }

    void EnoughMoneyToMove()
    {
        bool oldValue = enoughMoney;
        enoughMoney = ((currentCell.cellWayTitle != "COLONIAL" && currentCell.cellWayTitle != "KLONDIKE" && player.cash >= 0)
            || (player.cash + GameManager.Instance.colonyMovementCost)>=0);
        negativeMoney = player.cash < 0;

            onEnoughMoneyToMoveUpdate();
        
    }

}
