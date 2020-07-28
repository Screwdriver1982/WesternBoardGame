using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PaySmthCell : Cell
{
    [Header("Что должен заплатить или получить")]
    public float cashCoef; //процент от капитала если с минусом, то платит, если с плюсом то получает
    public int cashAdd;
    public int goldAdd;
    public int oilAdd;
    public int carsAdd;
    public int colaAdd;
    public int drugsAdd;
    [Header("Изменение награбленного и колоний")]
    public int robberyAdd;
    public int colonyAdd;

    public Player boss = null;
    [Header("Изменение цен")]
    public int goldCost;
    public int oilCost;
    public int carsCost;
    public int colaCost;
    [Header("Изменение инд.раб.силы игрока")]
    public int laborChanges;
    [Header("Изменение карточек игрока")]
    public int police;
    public int army;
    public int woolfy;
    public int rabby;
    public int taxFree;
    public int badHarvest;
    public int mediaCrisis;
    public int revenueBlock;
    [Header("Куда переместить в конце")]
    [SerializeField] Cell cellToMoveWithAction = null;
    [SerializeField] Cell cellToMoveWithoutAction = null;
    [SerializeField] Shares[] shares; //акции, которые проверяем, в большинстве случаев нулевую
    [SerializeField] CellPayType cellPayType; //тип клетки из предложенных ниже
    [SerializeField] float protezCoef;
    Player beneficiar = null;
    Player beneficiarSecond = null;
    [SerializeField] float taxRate; //налог от стоимости акции
    [SerializeField] int starMediaBonus; //сколько дают за каждую газету или телекомпанию
    [SerializeField] bool payBank; //если включено, то банк получит то, что нужно заплатить, даже если нет владельца
    [SerializeField] bool fine = false; //означает, что карточка такс фрии тут работает

    int beneficiarFreeCash;
    int beneficiarFreeCashSecond;
    int cashAddTemp;
    
    enum CellPayType
    {
        COMMON, //обычная оплата чего-то или получение денег игроком
        CORPORATION_TAX, //заплатить часть стоимости корпорации
        PAY_BOSS, //платим боссу кэш
        PAY_SHARE_OWNER_ONE_TIME_INCOME, //платим держателю акции разовый доход
        PAY_SHARE_OWNER_CASHSUM,
        PAY_SHARE_OWNER_GOODS_COST,//платим держателю акции сумму в кэше
        WASTE_DUMP, //держатель военного завода или банка оплачивает разовый доход со всех фирм игроку
        RAMBO, //забирает наличность у следующего за ним игрока
        PROTEZ, //оплачивает услуги протез миссии
        OWNER_GET_ONE_TIME_INCOME, //владелец акции просто получает доход, игрок за это не платит
        SHARES_INCOME_TO_PLAYER, //не совсем подходит под эту клетку, но смысл в том, что игрок сам получает доход с акций, если они ему принадлежат
        SHARES_INCOME_FROM_PLAYER, //игрок платит свой разовый доход с акций в банк
        WOOLFY, //попали на клетку с Вулфи
        RABBY, // попал на клетку с рэбби
        SHOOTING, // попал в перестрелку
        STARVATION, // голод в колонии
        COLONIAL_RIVAL, //восстание в колонии
        CORP_COST_CHANGE //изменение цен корпораций
    }


    public override void ActivateCell()
    {

        Player activePlayer = GameManager.Instance.WhoIsPlayer();
        beneficiar = null;
        beneficiarFreeCash = 0;
        beneficiarSecond = null;
        beneficiarFreeCashSecond = 0;
        cashAddTemp = cashAdd;
        Cell cellToMoveWithActionTemp = cellToMoveWithAction;
        Cell cellToMoveWithoutActionTemp = cellToMoveWithoutAction;

        switch (cellPayType)
        {
            case CellPayType.COMMON:
                if (fine && activePlayer.taxFreeCard)
                {
                    cashAddTemp = 0;
                }
                else
                { 
                    if (cashCoef != 0 && activePlayer.cash > 0)
                    {

                        cashAddTemp += Mathf.FloorToInt(activePlayer.cash * cashCoef);
                    }
                }


                if (way == TypesOfWays.CRIMINAL)
                {
                    boss = GameManager.Instance.WhoIsBossPlayer();
                    Player player = GameManager.Instance.WhoIsPlayer();
                    if (boss != null && boss != player && cashAddTemp > 0)
                    {
                        cashAddTemp = Mathf.FloorToInt(cashAddTemp * 0.5f);
                    }
                    robberyAdd = cashAddTemp;
                }
                else if (way == TypesOfWays.COLONIAL || way == TypesOfWays.KLONDIKE)
                {
                    colonyAdd = cashAddTemp;
                }
                beneficiar = null;
                beneficiarSecond = null;


                break;

            case CellPayType.CORPORATION_TAX:

                if (!activePlayer.taxFreeCard)
                {
                    for (int j = 0; j < shares.Length; j++)
                    {

                        if (activePlayer == GameManager.Instance.GetShareOwner(shares[j]))
                        {

                            if (shares[j].typeOfShares == "Corporation")
                            {

                                cashAddTemp -= Mathf.FloorToInt(GameManager.Instance.GiveCorporationPrice(shares[j]) * taxRate);

                            }

                        }
                    }
                }
                else
                {
                    cashAddTemp = 0;
                }

                beneficiar = null;
                beneficiarFreeCash = 0;


                break;

            case CellPayType.PAY_BOSS:

                beneficiar = GameManager.Instance.WhoIsBossPlayer();

                if (beneficiar == null || beneficiar == activePlayer)
                {

                    cashAddTemp = 0;

                }
                print(cashAdd);
                beneficiarFreeCash = 0;
                break;

            case CellPayType.PAY_SHARE_OWNER_ONE_TIME_INCOME:

                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                if (beneficiar != null && beneficiar != activePlayer)
                {
                    cashAddTemp = -GameManager.Instance.GetShareIncomeCash(shares[0]);
                }
                else
                {
                    cashAddTemp = 0;
                }
                beneficiarFreeCash = 0;
                break;

            case CellPayType.PAY_SHARE_OWNER_CASHSUM:
                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                if (beneficiar == null || beneficiar == activePlayer)
                {
                    cashAddTemp = 0;
                }
                beneficiarFreeCash = 0;
                break;
            



            case CellPayType.PAY_SHARE_OWNER_GOODS_COST:
                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                if ((beneficiar == null && !payBank)|| beneficiar == activePlayer)
                {
                    cashAddTemp = 0;
                }

                cashAddTemp -= goldAdd * GameManager.Instance.goodsCost[0];
                cashAddTemp -= oilAdd * GameManager.Instance.goodsCost[1];
                cashAddTemp -= carsAdd * GameManager.Instance.goodsCost[2];
                cashAddTemp -= colaAdd * GameManager.Instance.goodsCost[3];


                beneficiarFreeCash = 0;
                break;






            case CellPayType.WASTE_DUMP:

                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                cashAddTemp = activePlayer.AllSharesOneTimeCashIncome();

                if (beneficiar == null || beneficiar == activePlayer)
                {
                    cashAddTemp = 0;
                    beneficiar = null;
                }

                beneficiarFreeCash = 0;
                break;

            case CellPayType.RAMBO:
                beneficiar = GameManager.Instance.WhoIsPreviousPlayer();
                if (!beneficiar.armyCard)
                {
                    cashAddTemp = beneficiar.cash;
                }
                else
                {
                    cashAddTemp = 0;
                    beneficiar.ChangeCards(0, 0, -1, 0, 0, 0, 0, 0,0);
                }
                beneficiarFreeCash = 0;
                break;

            case CellPayType.PROTEZ:
                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                if (beneficiar == activePlayer)
                {
                    cashAddTemp = 0;
                    beneficiar = null;
                }
                else
                {
                    cashAddTemp = -Mathf.FloorToInt(protezCoef * GameManager.Instance.WhoIsPlayer().cash);
                }
                beneficiarFreeCash = 0;
                break;

            case CellPayType.OWNER_GET_ONE_TIME_INCOME:
                beneficiar = GameManager.Instance.GetShareOwner(shares[0]);
                beneficiarFreeCash = GameManager.Instance.GetShareIncomeCash(shares[0]);
                break;

            case CellPayType.SHARES_INCOME_TO_PLAYER:

                for (int i = 0; i < shares.Length; i++)
                {
                    if (activePlayer.playerShares.Contains(shares[i]))
                    {
                        cashAddTemp += GameManager.Instance.GetShareIncomeCash(shares[i]);
                    }
                }
                break;
            case CellPayType.SHARES_INCOME_FROM_PLAYER:
                if (!activePlayer.taxFreeCard)
                {
                    for (int i = 0; i < shares.Length; i++)
                    {
                        if (activePlayer.playerShares.Contains(shares[i]))
                        {
                            cashAddTemp -= GameManager.Instance.GetShareIncomeCash(shares[i]);
                        }
                    }
                }
                else
                {
                    cashAddTemp = 0;
                }
                break;

            case CellPayType.WOOLFY:
                if (activePlayer.woolfyCard)
                {
                    cashAddTemp += starMediaBonus * activePlayer.HowManyMedias();
                    activePlayer.ChangeCards(0, 0, 0, -1, 0, 0, 0, 0,0);
                }
                else
                {
                    cashAddTemp = 0;
                }
                beneficiar = null;
                beneficiarFreeCash = 0;
                break;

            case CellPayType.RABBY:
                if (activePlayer.rabbyCard)
                {
                    cashAddTemp += starMediaBonus * activePlayer.HowManyMedias();
                    activePlayer.ChangeCards(0, 0, 0, 0, -1, 0, 0, 0,0);
                }
                else
                {
                    cashAddTemp = 0;
                }
                beneficiar = null;
                beneficiarFreeCash = 0;
                break;


            case CellPayType.SHOOTING:
                if (activePlayer.armyCard)
                {
                    cellToMoveWithActionTemp = null;
                }
                beneficiar = null;
                beneficiarFreeCash = 0;
                break;

            case CellPayType.STARVATION:
                beneficiar = GameManager.Instance.GetShareOwner(shares[0]); //протез миссия
                beneficiarSecond = GameManager.Instance.GetShareOwner(shares[1]); // хлебная плантация

                if (beneficiar == activePlayer)
                {
                    cashAddTemp = 0;
                    beneficiar = null;
                }
                else
                {
                    cashAddTemp = -Mathf.FloorToInt(protezCoef * GameManager.Instance.WhoIsPlayer().cash);
                }
                beneficiarFreeCash = 0;

                if (beneficiarSecond != null)
                {
                    beneficiarFreeCashSecond = GameManager.Instance.GetShareIncomeCash(shares[1]);

                }
                else
                {
                    beneficiarFreeCashSecond = 0;
                }

                
                break;

            case CellPayType.COLONIAL_RIVAL:
                if (activePlayer.armyCard)
                {
                    cellToMoveWithActionTemp = null;
                }
                else if (activePlayer.colonyMoney > 0)
                {
                    
                    cashAddTemp = -activePlayer.colonyMoney;
                    
                }
                beneficiar = null;
                beneficiarFreeCash = 0;
                break;

            case CellPayType.CORP_COST_CHANGE:
                if (!GameManager.Instance.DoesPlayerHaveCorporation(activePlayer))
                {
                    goldAdd = 0;
                    break;
                }
                else if (activePlayer.gold + goldAdd < 0)
                {
                    activePlayer.PlayerCorpChange(-1);
                    goldAdd = 0;
                    break;
                }
                break;

        }

        print("cashAddTemp в конце скрипта клетки " + cashAddTemp);
        UIManager.Instance.ShowPaySmthWindow(cellIcon,
                                             cellTitle,
                                             cellDescription,
                                             cellWayTitle,
                                             cashAddTemp,
                                             goldAdd,
                                             oilAdd,
                                             carsAdd,
                                             colaAdd,
                                             drugsAdd,
                                             robberyAdd,
                                             colonyAdd,
                                             boss,
                                             beneficiar,
                                             beneficiarFreeCash,
                                             beneficiarSecond,
                                             beneficiarFreeCashSecond,
                                             goldCost,
                                             oilCost,
                                             carsCost,
                                             colaCost,
                                             laborChanges,
                                             police,
                                             army,
                                             woolfy,
                                             rabby,
                                             taxFree,
                                             badHarvest,
                                             mediaCrisis,
                                             cellToMoveWithActionTemp,
                                             cellToMoveWithoutActionTemp,
                                             revenueBlock
                                             );
    }
}
