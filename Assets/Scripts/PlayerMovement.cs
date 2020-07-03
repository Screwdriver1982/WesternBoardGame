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
            
            if (startMoveOrPass && currentCell.startMovingUp !=null   )
            {
                
                Cell nextCell =  currentCell.startMovingUp;
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else 
                {
                    needRotation = true;
                }

                currentCell = currentCell.startMovingUp;
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
            else if (!startMoveOrPass && currentCell.passUp !=null )
            {

                Cell nextCell = currentCell.passUp;
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCell = currentCell.passUp;
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

            if (startMoveOrPass && currentCell.startMovingDown != null)
            {

                Cell nextCell = currentCell.startMovingDown;
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCell = currentCell.startMovingDown;
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
            else if (!startMoveOrPass && currentCell.passDown != null)
            {

                Cell nextCell = currentCell.passDown;
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCell = currentCell.passDown;
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
            if (startMoveOrPass && currentCell.startMovingLeft != null)
            {

                Cell nextCell = currentCell.startMovingLeft;
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCell = currentCell.startMovingLeft;
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
            else if (!startMoveOrPass && currentCell.passLeft != null)
            {

                
                Cell nextCell = currentCell.passLeft;
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCell = currentCell.passLeft;
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
            if (startMoveOrPass && currentCell.startMovingRight != null)
            {

                Cell nextCell = currentCell.startMovingRight;
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCell = currentCell.startMovingRight;
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
            else if (!startMoveOrPass && currentCell.passRight != null)
            {

                Cell nextCell = currentCell.passRight;
                bool needRotation;

                if (nextCell.cellView == currentCell.cellView)
                {
                    needRotation = false;
                }
                else
                {
                    needRotation = true;
                }

                currentCell = currentCell.passRight;
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
