using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissTurnCell : Cell
{
    [SerializeField]bool doesPoliceSave; //помогает ли карточка полиции здесь
    [SerializeField] bool doesBossSave; //конфискация в пользу крестного отца
    [SerializeField] bool confiscation; //есть ли выбор пропуск или конфискация?
    [SerializeField] int goldCost; // есть ли возможность откупиться золотом, если 0, то нет
    [SerializeField] int missTurn; //количество ходов пропуска
    [SerializeField] int drugCoef; //во сколько раз больше пропускаешь ходов если везешь наркотики
    [SerializeField] Cell prisonCell; //клетка, где отдыхаешь =)
    [SerializeField] Cell rescueCell; // клетка, куда попадают откупившись
    Player activePlayer;

    public override void ActivateCell()
    {
        int finalMissTurn = missTurn;
        activePlayer = GameManager.Instance.WhoIsPlayer();
        if (activePlayer.drugs > 0 && drugCoef!=0)
        {
            finalMissTurn *= drugCoef;
        }

        if (goldCost < 0 && !activePlayer.armyCard)
        {
            UIManager.Instance.ShowMissTurnWindow(cellIcon,
                                                cellTitle,
                                                cellDescription,
                                                cellWayTitle,
                                                goldCost,
                                                finalMissTurn,
                                                "gold",
                                                prisonCell,
                                                rescueCell);

        }
        else if (goldCost < 0 && activePlayer.armyCard)
        {
            UIManager.Instance.ShowMissTurnWindow(cellIcon,
                                                cellTitle,
                                                cellDescription,
                                                cellWayTitle,
                                                goldCost,
                                                finalMissTurn,
                                                "army save",
                                                this,
                                                this);

        }
        else if (doesPoliceSave && activePlayer.policeCard)
        {
            UIManager.Instance.ShowMissTurnWindow(cellIcon,
                                    cellTitle,
                                    cellDescription,
                                    cellWayTitle,
                                    0,
                                    0,
                                    "save",
                                    this,
                                    this);

        }
        else if (doesBossSave && activePlayer.bossCard)
        {
            UIManager.Instance.ShowMissTurnWindow(cellIcon,
                                    cellTitle,
                                    cellDescription,
                                    cellWayTitle,
                                    0,
                                    0,
                                    "boss",
                                    prisonCell,
                                    rescueCell);
        }
        else if ((!doesPoliceSave || doesPoliceSave && !activePlayer.policeCard) && !confiscation)
        {
            UIManager.Instance.ShowMissTurnWindow(cellIcon,
                                    cellTitle,
                                    cellDescription,
                                    cellWayTitle,
                                    0,
                                    finalMissTurn,
                                    "miss",
                                    prisonCell,
                                    rescueCell);

        }
        else if ((!doesPoliceSave || doesPoliceSave && !activePlayer.policeCard) && confiscation)
        {
            UIManager.Instance.ShowMissTurnWindow(cellIcon,
                        cellTitle,
                        cellDescription,
                        cellWayTitle,
                        -activePlayer.robberiedMoney,
                        finalMissTurn,
                        "miss or robbery",
                        prisonCell,
                        rescueCell);

        }
        else if (doesBossSave && !activePlayer.bossCard)
        {
            UIManager.Instance.ShowMissTurnWindow(cellIcon,
                        cellTitle,
                        cellDescription,
                        cellWayTitle,
                        -activePlayer.robberiedMoney,
                        finalMissTurn,
                        "toBoss",
                        prisonCell,
                        rescueCell);
        }
    }
}