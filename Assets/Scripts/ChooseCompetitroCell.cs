using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCompetitroCell : Cell
{
    [SerializeField] CellCompetitorType cellType;
    [SerializeField] int killCost = -5000;
    [SerializeField] int goodsChanges = -1;
    [SerializeField] Cell cellToMove = null;
    string typeOfWindow;


    enum CellCompetitorType
    {
        KILL,
        DROP_GOODS

    }

    public override void ActivateCell()
    {
        int secondPlayerNum = -1;
        int thirdPlayerNum = -1;
        int fourthPlayerNum = -1;
        Cell cellToMoveTemp = cellToMove;
        

        int actPlNum = GameManager.Instance.activePlayerNum;

        switch (cellType)
        {
            case CellCompetitorType.KILL:
                typeOfWindow = "Kill";
                if (GameManager.Instance.IsPlayerInGameNum((actPlNum + 1) % 4))
                {
                    secondPlayerNum = (actPlNum + 1) % 4;
                }

                if (GameManager.Instance.IsPlayerInGameNum((actPlNum + 2) % 4))
                {
                    thirdPlayerNum = (actPlNum + 2) % 4;
                }

                if (GameManager.Instance.IsPlayerInGameNum((actPlNum + 3) % 4))
                {
                    fourthPlayerNum = (actPlNum + 3) % 4;
                }

                break;


            case CellCompetitorType.DROP_GOODS:
                typeOfWindow = "Goods";
                if (GameManager.Instance.IsPlayerInGameNum((actPlNum + 1) % 4) 
                    && GameManager.Instance.DoesPlayerHaveGoodsGeneratorNum((actPlNum + 1) % 4)
                    )
                {
                    secondPlayerNum = (actPlNum + 1) % 4;
                }

                if (GameManager.Instance.IsPlayerInGameNum((actPlNum + 2) % 4)
                    && GameManager.Instance.DoesPlayerHaveGoodsGeneratorNum((actPlNum + 2) % 4)
                    )
                {
                    thirdPlayerNum = (actPlNum + 2) % 4;
                }

                if (GameManager.Instance.IsPlayerInGameNum((actPlNum + 3) % 4)
                    && GameManager.Instance.DoesPlayerHaveGoodsGeneratorNum((actPlNum + 3) % 4)
                    )
                {
                    fourthPlayerNum = (actPlNum + 3) % 4;
                }
                break;
        }

        UIManager.Instance.ShowChooseCompetitorWindow(cellIcon,
                                                      cellTitle,
                                                      cellDescription,
                                                      cellWayTitle, 
                                                      secondPlayerNum, 
                                                      thirdPlayerNum, 
                                                      fourthPlayerNum, 
                                                      killCost,
                                                      cellToMoveTemp,
                                                      typeOfWindow,
                                                      goodsChanges);




    }
}
