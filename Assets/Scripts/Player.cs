using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("Цвет игрока")]
    public Color playerColor;

    [Header("Счет игрока")]
    public int cash;
    public int gold;
    public int oil;
    public int cars;
    public int cola;
    public int drugs;
    public int trinkets;

    [Header("Финансы")]
    public int capital;
    public int robberiedMoney;
    public int colonyMoney;
    public int colonyLoan;

    [Header("Начальная ссуда")]
    public int startingLoan;

    [Header("Молодые мозги")]
    public int[] brains = { 0, 0, 0};

    [Header("Карточки игрока")]
    public bool policeCard;
    public bool bossCard;
    public bool armyCard;
    public bool woolfyCard;
    public bool rabbyCard;
    public bool taxFreeCard;
    public bool badHarvestCard;
    public bool mediaCrisisCard;
    public bool revenueBlockCard;

    [Header("Кредиты игрока")]
    public int loans;
    public int loanOnThisRound;
    public float loanPercents;

    [Header("Депозиты игрока")]
    public int deposites;
    public float depositePercents;

    [Header("Акции игрока")]
    public List<Shares> playerShares;
    
    [Header("Индекс Рабочей Силы")]
    public int laborIndex;

    [Header("Рассчитанные разовые доходы и изменения")]
    public int cashRevenue;
    public int goldRevenue;
    public int oilRevenue;
    public int carsRevenue;
    public int colaRevenue;

    [Header("Казино")]
    public int casinoPossibleReward; //последняя сумма, которую игрок мог выиграть в казино


    //изменяется или нет индекс
    public int stockExchangeIndex;
    
    //доход с депозитов
    public int depositeRevenue;

    //убыток с кредитов
    public int loanTax;

    //доход с мозгов после 3 кругов
    public int brainsGoldRevenue;

    //списание мозгов после 3 кругов
    public int brainsConversion;
    //разовый доход при Гербициде
    public int plantationRevenue;



    [Header("подписываемые акции скрипта")]
    //функция, вызывающия обновление кэша у неактивных игроков
    public Action onCashChanges = delegate { };

    //функция, вызывающия обновление кошелька активного игрока
    public Action onWalletChanges = delegate { };

    //функция, вызывающия обновление карточек активного игрока
    public Action onCardsChanges = delegate { };

    //функция, вызывающая обновление экрана со всеми наградами за раунд
    public Action onRevenueCount = delegate { };

    //функция, вызывающая обновление экрана с наградами за плантации
    public Action onPlantationCount = delegate { };

    //функция, вызывающая обновление индекса рабочей силы
    public Action onLaborIndexChanges = delegate { };


    private void Start()
    {
        loanPercents = GameManager.Instance.baseLoanPercents;
        depositePercents = GameManager.Instance.baseDepositePercents;
    }

    // Функция меняет кошелек игрока
    public void WalletChange(int cashAdd, int goldAdd, int oilAdd, int carsAdd, int colaAdd, int drugsAdd, int robberyAdd, int colonyAdd)
    {
        if (cashAdd > 0 && revenueBlockCard)
        {
            cashAdd = 0;
        }
        cash += cashAdd;
        gold = Mathf.Max(gold+goldAdd, 0);
        oil = Mathf.Max(oilAdd,0);
        cars = Mathf.Max(carsAdd, 0);
        cola = Mathf.Max(colaAdd, 0);
        drugs = Mathf.Max(drugsAdd, 0);
        robberiedMoney += robberyAdd;
        colonyMoney += colonyAdd;

        if (cashAdd != 0)
        {
            onCashChanges();
        }
        onWalletChanges();
    }

    //Вызывается GameManager-ом функция добавляет акцию игроку
    public void AddShare(Shares newShare)
    {
        playerShares.Add(newShare);
    }


    //удаляет акцию игроку, и говорит банку забрать себе обратно акцию (банк сам поменяет ей владельца и установит базовую цену)
    public void ReturnShare(Shares returnedShare)
    {
        GameManager.Instance.ReturnShareToBank(returnedShare);
        playerShares.Remove(returnedShare);

    }

    public void SellShare(Shares soldShare, float speculationCoef)
    {
        print(speculationCoef);
        int cost = 0;
        if (soldShare.typeOfShares != "Corporation")
        {
            cost = Mathf.FloorToInt(soldShare.cost * speculationCoef);
            print("sell from player: " + cost);
            WalletChange(cost, 0, 0, 0, 0, 0, 0, 0);
            ReturnShare(soldShare);
        }
        else
        {
            cost = Mathf.FloorToInt(GameManager.Instance.GiveCorporationPrice(soldShare)*speculationCoef);
            print("sell from player: " + cost);
            WalletChange(cost, 0, 0, 0, 0, 0, 0, 0);
            ReturnShare(soldShare);
        }
    }

    //чистит акции у игрока возвращает их все по-одной в банк
    public void ReturnAllShares()
    {
        while (playerShares.Count > 0)
        {
            ReturnShare(playerShares[0]);
        }
    }




    public int ShareTypeOneTimeCashIncome(string shareType)
    {
        int cashIncome = 0;
        for (int i = 0; i < playerShares.Count; i++)
        {
            Shares shareI = playerShares[i];
            if (shareI.typeOfShares == shareType)
            {
                cashIncome += GameManager.Instance.GetShareIncomeCash(shareI);
            }
            
        }
        return cashIncome;
    }

    public int AllSharesOneTimeCashIncome()
    {
        int cashIncome = 0;
        cashIncome += ShareTypeOneTimeCashIncome("Common");
        cashIncome += ShareTypeOneTimeCashIncome("Media");
        cashIncome += ShareTypeOneTimeCashIncome("DrugsPlantation");
        cashIncome += ShareTypeOneTimeCashIncome("Plantation");
        cashIncome += ShareTypeOneTimeCashIncome("Corporation");
        return cashIncome;
    }

    public int HowManyMedias()
    {
        int medias = 0;
        for (int i = 0; i < playerShares.Count; i++)
        {
            if (playerShares[i].typeOfShares == "Media")
            {
                medias+=1;
            }
        }
        return medias;
    }

    //Функция считает что получит игрок в конце круга
    public void RevenueCount()
    {
        //зануляем доход, записанный в прошлом круге
        cashRevenue = 0;
        goldRevenue = 0;
        oilRevenue = 0;
        carsRevenue = 0;
        colaRevenue = 0;
        stockExchangeIndex = 0;

        //идем подряд по всем акциями игрока и считаем, сколько он должен получить чего, тут только подсчет, без выдачи
        for (int i = 0; i < playerShares.Count; i++)
        {
            Shares shareI = playerShares[i];
            if (shareI.typeOfShares != "Corporation")
            {
                if ((shareI.typeOfShares == "Plantaion" || shareI.typeOfShares == "DrugsPlantation") && !badHarvestCard
                    || (shareI.typeOfShares == "Media" && !mediaCrisisCard)
                    || shareI.typeOfShares == "Common")
                {
                    cashRevenue += shareI.revenueFix;
                    cashRevenue += shareI.cost * Mathf.RoundToInt(shareI.revenuePercent);
                }

                goldRevenue += shareI.goldRevenueFix;
                if (shareI.typeOfShares == "Media" && !mediaCrisisCard)
                {   
                    //проверяем есть ли хоть одно средство массовой информации, если есть будем повышать курс акций
                    stockExchangeIndex = 1;
                }
            }
            else
            {
                cashRevenue += shareI.revenueFix;
                cashRevenue += GameManager.Instance.GiveCorporationPrice(shareI) * Mathf.RoundToInt(shareI.revenuePercent);
                carsRevenue += Mathf.FloorToInt(shareI.carsFromCapital * cash / 10000);
                colaRevenue += Mathf.FloorToInt(shareI.colaFromCapital * cash / 10000);

            }
        }
        
        //считаем заработок с депозитов
        depositeRevenue = Mathf.FloorToInt(deposites * depositePercents);
        
        //считаем траты на проценты по кредитам со знаком +
        if (!taxFreeCard)
        {
            loanTax = -Mathf.FloorToInt(loans * loanPercents);

        }

        //считаем заработал ли золото с мозгов
        brainsGoldRevenue = brains[0] * 20;

        //считаем должен ли отдать колониальный займ

        onRevenueCount();

    }

    //подсчитываем разовый доход с плантаций
    public void PlantationRevenue()
    {
        plantationRevenue = 0;
        for (int i = 0; i < playerShares.Count; i++)
        {
            Shares shareI = playerShares[i];
            if (shareI.typeOfShares == "Plantation")
            {
                plantationRevenue += shareI.revenueFix;
                plantationRevenue += shareI.cost * Mathf.RoundToInt(shareI.revenuePercent);
            }
        }
    }


    // Устанавливает значение всех карточек
    public void SetCard(bool boss, bool police, bool army, bool woolfy, bool rabby, bool taxFree, bool badHarvest, bool mediaCrisis, bool revenueBlock)
    {
        bossCard = boss;
        policeCard = police;
        armyCard = army;
        woolfyCard = woolfy;
        rabbyCard = rabby;
        


        if (boss || police || army || woolfy || rabby)
        {
            onCardsChanges();
        }

        taxFreeCard = taxFree;
        badHarvestCard = badHarvest;
        mediaCrisisCard = mediaCrisis;
        revenueBlockCard = revenueBlock;

    }

    public void ChangeCards(int boss, int police, int army, int woolfy, int rabby, int taxFree, int badHarvest, int mediaCrisis, int revenueBlock)
    {
        bossCard = (boss > 0) || ((boss >= 0) & bossCard);
        policeCard = (police > 0) || ((police >= 0) & policeCard);
        armyCard = (army > 0) || ((army >= 0) & armyCard);
        woolfyCard = (woolfy > 0) || ((woolfy >= 0) & woolfyCard);
        rabbyCard = (rabby > 0) || ((rabby >= 0) & rabbyCard);

        if (boss!=0 || police!=0 || army!=0 || woolfy!=0 || rabby!=0)
        {
            onCardsChanges();
        }

        taxFreeCard = (taxFree > 0) || ((taxFree >= 0) & taxFreeCard);
        badHarvestCard = (badHarvest > 0) || ((badHarvest >= 0) & badHarvestCard);
        mediaCrisisCard = (mediaCrisis > 0) || ((mediaCrisis >= 0) & mediaCrisisCard);
        revenueBlockCard = (revenueBlock > 0) || ((revenueBlock >= 0) & revenueBlockCard);
    }


    // вызывает изменение индекса рабочей силы, акций корпораций игрока и товаров от этих корпораций, причем в противоположные стороны
    public void LaborIndexChanges(int indexChanges)
    {
        if (indexChanges != 0)
        {
            int indexRealChanges = Mathf.Clamp(laborIndex + indexChanges, -5, 5);
            PlayerCorpChange(indexChanges);
            PlayerCorpGoodsChanges(-indexChanges);
            laborIndex += indexRealChanges;

            //если индекс больше 0, то базовый процент уменьшается в 2 раза, если меньше нуля то увеличивается в 2 раза
            loanPercents =  GameManager.Instance.baseLoanPercents* Mathf.Pow(2f, -Mathf.Clamp(laborIndex, -1f, 1f));

            onLaborIndexChanges();
        }
    }

    //индекс показывает на сколько нужно изменить стоимость товаров игрока, обратно к изменению индекса самих корпораций
    public void PlayerCorpGoodsChanges(int goodsPointsChanges) 
    {
        if (goodsPointsChanges != 0)
        {
            for (int i = 0; i < playerShares.Count; i++)
            {
                if (playerShares[i].typeOfShares == "Corporation" )
                {
                    GameManager.Instance.ChangeCorporatioGoodsCost(playerShares[i], goodsPointsChanges);

                }
            }
        }
    }


    // изменение индекса всем корпорациям игрока на определенное количество пунктов
    public void PlayerCorpChange(int corpChanges)
    {
        if (corpChanges != 0)
        { 
            for (int i = 0; i < playerShares.Count; i++)
            {
                if (playerShares[i].typeOfShares == "Corporation")
                {
                    GameManager.Instance.SetCorporationCost(playerShares[i], Mathf.FloorToInt(corpChanges * 0.1f * playerShares[i].cost));

                }
            }
        }

    }


    //функция начисляющая и меняющая все после круга
    public void RoundChanges()
    {
        int cashAdd = GameManager.Instance.roundBaseMoney + cashRevenue + depositeRevenue - loanTax - colonyLoan;
        int goldAdd = goldRevenue + brainsGoldRevenue;
        int oilAdd = oilRevenue;
        int carsAdd = carsRevenue;
        int colaAdd = colaRevenue;

        //обновляем кошелек и удаляем карточки
        WalletChange(cashAdd, goldAdd, oilAdd, carsAdd, colaAdd, 0, 0, 0);
        SetCard(false, false, false, false, false, false, false, false, false);

        //прокручиваем мозги
        brains[0] = brains[1];
        brains[1] = brains[2];
        brains[2] = 0;
        //возвращаем колонияльный кредит
        colonyLoan = 0;

        //меняем индекс корпораций игрока, если у него были медиа
        PlayerCorpChange(stockExchangeIndex);



    }

   

}
