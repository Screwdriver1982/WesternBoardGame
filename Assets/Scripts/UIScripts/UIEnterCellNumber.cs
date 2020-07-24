using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnterCellNumber : MonoBehaviour
{
    [SerializeField] InputField cellNumberInputField;
    int cellNumber;

    public void EnterCell()
    {
        if (cellNumberInputField.text != null)
        {
            cellNumber = int.Parse(cellNumberInputField.text);

            if (cellNumber > 0 && cellNumber < GameManager.Instance.cellsNumber.Length)
            {
                PlayerMovement playerMvmnt = GameManager.Instance.WhoIsPlayerMVMNT();
                GameManager.Instance.SwitchCameraToPlayer(playerMvmnt);
                playerMvmnt.JumpToCellAndActivateIt(GameManager.Instance.cellsNumber[cellNumber]);
                UIManager.Instance.ShowMoveArrowPanel();
            }

        } 

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            EnterCell();
        }
    }

}
