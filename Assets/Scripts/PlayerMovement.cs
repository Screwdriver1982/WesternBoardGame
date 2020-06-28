using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerColors playerColor;

    [Header("Перемещение между клетками")]
    [SerializeField] float oneCellJumpTime;
    [SerializeField] float jumpPower =3f;
    [SerializeField] float rotateTime =1f;

    [SerializeField] Camera playerCamera;


    [SerializeField] int currentCellNum;
    [SerializeField] Cell currentCell;

    [SerializeField] bool playerTurn = true;
    [SerializeField] bool startMoveOrPass = true;
    [SerializeField] int moveLeft = 6;
    [SerializeField] bool allowMovement;
    [SerializeField] bool dialogIsOpen = false;

    Vector3 startPosition;

    enum PlayerColors
    { 
        RED,
        BLUE,
        GREEN,
        YELLOW
    
    }

    private void Start()
    {
        currentCell = GameManager.Instance.cells[currentCellNum];

        if (playerColor == PlayerColors.RED)
        {
            startPosition = currentCell.redPosition.transform.position;
            transform.position = startPosition;
        }
        if (playerColor == PlayerColors.BLUE)
        {
            startPosition = currentCell.bluePosition.transform.position;
            transform.position = startPosition;
        }
        if (playerColor == PlayerColors.GREEN)
        {
            startPosition = currentCell.greenPosition.transform.position;
            transform.position = startPosition;
        }
        if (playerColor == PlayerColors.YELLOW)
        {
            startPosition = currentCell.yellowPosition.transform.position;
            transform.position = startPosition;
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && playerTurn && !dialogIsOpen && allowMovement && moveLeft > 0)
        {
            
            if (startMoveOrPass && currentCell.startMovingUp !=0   )
            {
                
                int nextCellNum =  currentCell.startMovingUp;
                Cell nextCell = GameManager.Instance.cells[nextCellNum];
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else 
                {
                    needRotation = true;
                }

                currentCellNum = currentCell.startMovingUp;
                currentCell = GameManager.Instance.cells[currentCellNum];
                allowMovement = false;
                moveLeft -= 1;
                if (moveLeft > 0)
                {
                    startMoveOrPass = false;
                }
                else
                {
                    startMoveOrPass = true;
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
            else if (!startMoveOrPass && currentCell.passUp !=0 )
            {

                int nextCellNum = currentCell.passUp;
                Cell nextCell = GameManager.Instance.cells[nextCellNum];
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCellNum = currentCell.passUp;
                currentCell = GameManager.Instance.cells[currentCellNum];
                allowMovement = false;
                moveLeft -= 1;
                if (moveLeft > 0)
                {
                    startMoveOrPass = false;
                }
                else
                {
                    startMoveOrPass = true;
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
            

        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) && playerTurn && !dialogIsOpen && allowMovement && moveLeft > 0)
        {

            if (startMoveOrPass && currentCell.startMovingDown != 0)
            {

                int nextCellNum = currentCell.startMovingDown;
                Cell nextCell = GameManager.Instance.cells[nextCellNum];
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCellNum = currentCell.startMovingDown;
                currentCell = GameManager.Instance.cells[currentCellNum];
                allowMovement = false;
                moveLeft -= 1;
                if (moveLeft > 0)
                {
                    startMoveOrPass = false;
                }
                else
                {
                    startMoveOrPass = true;
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
            else if (!startMoveOrPass && currentCell.passDown != 0)
            {

                int nextCellNum = currentCell.passDown;
                Cell nextCell = GameManager.Instance.cells[nextCellNum];
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCellNum = currentCell.passDown;
                currentCell = GameManager.Instance.cells[currentCellNum];
                allowMovement = false;
                moveLeft -= 1;
                if (moveLeft > 0)
                {
                    startMoveOrPass = false;
                }
                else
                {
                    startMoveOrPass = true;
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

        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && playerTurn && !dialogIsOpen && allowMovement && moveLeft > 0)
        {
            if (startMoveOrPass && currentCell.startMovingLeft != 0)
            {

                int nextCellNum = currentCell.startMovingLeft;
                Cell nextCell = GameManager.Instance.cells[nextCellNum];
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCellNum = currentCell.startMovingLeft;
                currentCell = GameManager.Instance.cells[currentCellNum];
                allowMovement = false;
                moveLeft -= 1;
                if (moveLeft > 0)
                {
                    startMoveOrPass = false;
                }
                else
                {
                    startMoveOrPass = true;
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
            else if (!startMoveOrPass && currentCell.passLeft != 0)
            {

                int nextCellNum = currentCell.passLeft;
                Cell nextCell = GameManager.Instance.cells[nextCellNum];
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCellNum = currentCell.passLeft;
                currentCell = GameManager.Instance.cells[currentCellNum];
                allowMovement = false;
                moveLeft -= 1;
                if (moveLeft > 0)
                {
                    startMoveOrPass = false;
                }
                else
                {
                    startMoveOrPass = true;
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
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) && playerTurn && !dialogIsOpen && allowMovement && moveLeft > 0)
        {
            if (startMoveOrPass && currentCell.startMovingRight != 0)
            {

                int nextCellNum = currentCell.startMovingRight;
                Cell nextCell = GameManager.Instance.cells[nextCellNum];
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCellNum = currentCell.startMovingRight;
                currentCell = GameManager.Instance.cells[currentCellNum];
                allowMovement = false;
                moveLeft -= 1;
                if (moveLeft > 0)
                {
                    startMoveOrPass = false;
                }
                else
                {
                    startMoveOrPass = true;
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
            else if (!startMoveOrPass && currentCell.passRight != 0)
            {

                int nextCellNum = currentCell.passRight;
                Cell nextCell = GameManager.Instance.cells[nextCellNum];
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCellNum = currentCell.passRight;
                currentCell = GameManager.Instance.cells[currentCellNum];
                allowMovement = false;
                moveLeft -= 1;
                if (moveLeft > 0)
                {
                    startMoveOrPass = false;
                }
                else
                {
                    startMoveOrPass = true;
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

        transform.DOJump(newPosition, jumpPower, 1, oneCellJumpTime).OnComplete(AllowMovement);
    }
    private void AllowMovement()
    {
        allowMovement = true;
    }

    public void GiveTheTurnToPlayer()
    {
        playerCamera.gameObject.SetActive(true);
        playerTurn = true;
    }

    public void TakeTurnFromPlayer()
    {
        playerCamera.gameObject.SetActive(false);
        playerTurn = false;
    }
}
