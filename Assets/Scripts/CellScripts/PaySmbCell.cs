using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaySmbCell : PaySmthCell
{
    [SerializeField] Shares[] shares; //акции, которые проверяем, в большинстве случаев нулевую
    [SerializeField] CellPaySmbType cellPaySmbType; //тип клетки из предложенных ниже
    [SerializeField] float protezCoef;
    Player beneficiar = null;
    int beneficiarFreeCash;
    enum CellPaySmbType
    { 
        PAY_BOSS, //платим боссу кэш
        PAY_SHARE_OWNER_ONE_TIME_INCOME, //платим держателю акции разовый доход
        PAY_SHARE_OWNER_CASHSUM, //платим держателю акции сумму в кэше
        WASTE_DUMP, //держатель военного завода или банка оплачивает разовый доход со всех фирм игроку
        RAMBO, //забирает наличность у следующего за ним игрока
        PROTEZ, //оплачивает услуги протез миссии
        OWNER_GET_ONE_TIME_INCOME, //владелец акции просто получает доход, игрок за это не платит
        SHARES_INCOME_TO_PLAYER, //не совсем подходит под эту клетку, но смысл в том, что игрок сам получает доход с акций, если они ему принадлежат
        SHARES_INCOME_FROM_PLAYER //игрок платит свой разовый доход с акций в банк
    }

    public override void ActivateCell()
    {
        Player activePlayer = GameManager.Instance.WhoIsPlayer();
        switch (cellPaySmbType)
        {
            case CellPaySmbType.PAY_BOSS:
                beneficiar = GameManager.Instance.WhoIsBossPlayer();
                if (beneficiar == null && beneficiar == activePlayer)
                {
                    cashAdd = 0;

                }
                
                beneficiarFreeCash = 0;
            break;

            case CellPaySmbType.PAY_SHARE_OWNER_ONE_TIME_INCOME:

                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                if (beneficiar != null && beneficiar != activePlayer)
                {
                    cashAdd = -GameManager.Instance.GetShareIncomeCash(shares[0]);
                }
                else
                {
                    cashAdd = 0;
                }
                beneficiarFreeCash = 0;
                break;

            case CellPaySmbType.PAY_SHARE_OWNER_CASHSUM:
                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                if (beneficiar == null || beneficiar == activePlayer)
                {
                    cashAdd = 0;
                }
                beneficiarFreeCash = 0;
                break;

            case CellPaySmbType.WASTE_DUMP:

                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                cashAdd = activePlayer.AllSharesOneTimeCashIncome();

                if (beneficiar == null && beneficiar == activePlayer)
                {
                    beneficiar = null;
                }

                beneficiarFreeCash = 0;
                break;

            case CellPaySmbType.RAMBO:
                beneficiar = GameManager.Instance.WhoIsPreviousPlayer();
                if (!beneficiar.armyCard)
                {
                    cashAdd = beneficiar.cash;
                }
                else
                {
                    cashAdd = 0;
                    beneficiar.ChangeCards(0,0,-1,0,0,0,0,0,0);
                }
                beneficiarFreeCash = 0;
                break;

            case CellPaySmbType.PROTEZ:
                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                if (beneficiar == activePlayer)
                {
                    cashAdd = 0;
                    beneficiar = null;
                }
                else
                { 
                    cashAdd = -Mathf.FloorToInt(protezCoef * GameManager.Instance.WhoIsPlayer().cash);
                }
                beneficiarFreeCash = 0;
                break;

            case CellPaySmbType.OWNER_GET_ONE_TIME_INCOME:
                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                beneficiarFreeCash = GameManager.Instance.GetShareIncomeCash(shares[0]);
                break;

            case CellPaySmbType.SHARES_INCOME_TO_PLAYER:
                
                for (int i = 0; i < shares.Length; i++)
                {
                    if (activePlayer.playerShares.Contains(shares[i]))
                    {
                        cashAdd += GameManager.Instance.GetShareIncomeCash(shares[i]);
                    }
                }
                break;
            case CellPaySmbType.SHARES_INCOME_FROM_PLAYER:
                
                for (int i = 0; i < shares.Length; i++)
                {
                    if (activePlayer.playerShares.Contains(shares[i]))
                    {
                        cashAdd -= GameManager.Instance.GetShareIncomeCash(shares[i]);
                    }
                }
                break;




        }


        UIManager.Instance.ShowPaySmbWindow(cellIcon,
                                             cellTitle,
                                             cellDescription,
                                             cellWayTitle,
                                             cashAdd,
                                             goldAdd,
                                             oilAdd,
                                             carsAdd,
                                             colaAdd,
                                             drugsAdd,
                                             robberyAdd,
                                             colonyAdd, beneficiar, beneficiarFreeCash);


    }


    void CountSum()
    {
        Player activePlayer = GameManager.Instance.WhoIsPlayer();
        if (cashCoef != 0 && activePlayer.cash > 0)
        {

            cashAdd += Mathf.FloorToInt(activePlayer.cash * cashCoef);
        }

    }
}

