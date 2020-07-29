using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        dice.onDiceStopped += DiceResult;
        dice.enabled = false;


    }
    #endregion


    [Header("Игроки")]
    [SerializeField] PlayerMovement[] playerMovements;
    public int activePlayerNum;
    [SerializeField] float changePlayerTime;
    public int winnerPlayerNum; //номер победившего игрока

    [Header("Работа с UI")]
    [SerializeField] UIManager UIManager;


    [Header("Настройки старта игры")]
    public bool  gameStarted = false;
    [SerializeField] int startingLoan = 50000;
    public int roundBaseMoney = 5000;
    public int movieMoney = 2000;
    public float baseLoanPercents = 0.1f;
    public int maxRoundLoan;
    public float baseDepositePercents = 0.25f;
    public int colonyMovementCost = -200;
    public Cell slavaryMarket; //клетка, куда отправляешься из колоний, если кончились деньги на движение
    public float casinoRewardCoef; // коэффициент казино
    [SerializeField] int maxRoundBeforeEnd;
    [SerializeField] int retiredPlayers = 0;

    [Header("Фондовая Биржа")]
    [SerializeField] Shares[] tradableShares;
    public Action onCorpCostChanges = delegate { };
    public int[] corpCosts;
    [SerializeField] int[] corpGoodsNumber;
    public Shares[] donors; //акции, которые делятся прибылью
    public Shares recepient; //акция с владельцем которой делятся, если он есть (ХММ)

    [Header("Товарная биржа")]
    [SerializeField] string[] goodsNames = { "gold", "oil", "cars", "cola" };
    public int[] goodsCost = { 100, 1000, 1000, 200 };
    public int[] goodsPointCost = { 50, 250, 250, 50 };

    [Header("Генераторы товаров")] // должны соответствовать по номерам товарам на товарной бирже, дублируют корпорации, но уж что поделать
    [SerializeField] Shares[] goodsGenerators;

    [Header("Все акции в игре")]
    [SerializeField] Shares[] allShares;
    [SerializeField] Player[] shareOwner;
    


    [Header("Кубик")]
    [SerializeField] Dice dice;
    [SerializeField] GameObject diceCameraObj;
    [SerializeField] AudioClip throwAudio;


    [Header("Камеры")]
    public Camera activeCamera;
    [SerializeField] Camera diceCamera;
    [SerializeField] FreeCamera freeCamera;

    [Header("Правильный порядок клеток, для кнопки Рэмбо")]
    [SerializeField] Cell[] cellOrderFromRamboCell;

    [Header("Игровое поле")]
    public Cell[] cellsNumber; //клетки в правильном порядке, чтобы правильно перелетать
    
    //[SerializeField] int[] unsoldShares;



    private void Start()
    {
        Player playerJ;

        for (int j = 0; j < playerMovements.Length; j++)
        {
            playerJ = playerMovements[j].GetComponent<Player>();
            InitiatePlayer(playerJ);

        }
        activePlayerNum = 0;
        SetPlayerInUI();
        ChangeGMState(GameStates.START_DICE_THROW_WAITING);
        for (int i = 0; i < tradableShares.Length; i++)
        {
            corpCosts[i] = tradableShares[i].cost;

        }
        onCorpCostChanges();

        

    }

    private void Update()
    {
        StateUpdate();
    }


    void SetPlayerInUI()
    {
        Player activePlayer = playerMovements[activePlayerNum].GetComponent<Player>();
        Player secondPlayer = playerMovements[(activePlayerNum + 1) % 4].GetComponent<Player>();
        Player thirdPlayer = playerMovements[(activePlayerNum + 2) % 4].GetComponent<Player>();
        Player fourthPlayer = playerMovements[(activePlayerNum + 3) % 4].GetComponent<Player>();

        if (activePlayer != null && secondPlayer != null && thirdPlayer != null && fourthPlayer != null)
        {

            UIManager.SetPlayersUI(activePlayer, secondPlayer, thirdPlayer, fourthPlayer);
        }


    }

    public int GiveCorporationPrice(Shares corporation)
    {

        int corporationIndex = -1;

        for (int i = 0; i < tradableShares.Length; i++)
        {

            if (corporation == tradableShares[i])
            {

                corporationIndex = i;
                break;
            }
        }
        return corpCosts[corporationIndex];
    }

    //устанавливает стоимость корпорации
    public void SetCorporationCost(Shares corporation, int costChanges)
    {
        

        for (int i = 0; i < tradableShares.Length; i++)
        {
            if (corporation == tradableShares[i])
            {
                
                corpCosts[i] = Mathf.Clamp(corpCosts[i] + costChanges,
                                            Mathf.FloorToInt(corporation.cost * 0.1f),
                                            corporation.cost * 2
                                            );
                
                onCorpCostChanges();
            }
        }
    }

    //отдает игрока, владельца той или иной акции
    public Player GetShareOwner(Shares checkedShare)
    {
        Player owner = null;
        for (int i = 0; i < allShares.Length; i++)
        {
            if (allShares[i] == checkedShare)
            {
                owner = shareOwner[i];
                break;
            }
        }
        return owner;
    }

    public int GetShareIncomeCash(Shares share)
    {
        int cashRevenue = 0;
        cashRevenue += share.revenueFix;
        

        if (share.revenuePercent != 0)
        {
        

            if (share.typeOfShares != "Corporation")
            {


                cashRevenue += Mathf.FloorToInt(share.cost * share.revenuePercent);

            }
            else
            {
                cashRevenue += Mathf.FloorToInt(GiveCorporationPrice(share) * share.revenuePercent);

            }

        }

        
        return cashRevenue;
    }


    public void ChangeGoodsCost(int goodsNumber, int changeIndex)
    {
        goodsCost[goodsNumber] = Mathf.Max(goodsCost[goodsNumber] + goodsPointCost[goodsNumber] * changeIndex, goodsPointCost[goodsNumber]);
    }

    public void ChangeCorporatioGoodsCost(Shares corporation, int changeIndex)
    {
        for (int i = 0; i < tradableShares.Length; i++)
        {
            if (tradableShares[i] == corporation)
            {
                ChangeGoodsCost(corpGoodsNumber[i], changeIndex);
            }
        }
    }


    public int HowMuchDoesGoodCost(int goodNumber)
    {
        return goodsCost[goodNumber];
    }

    //Вызывается скриптом игрока, возвращает акцию в банк, если это корпорация, то устанавливает ей базовую стоимость
    public void ReturnShareToBank(Shares returnedShare)
    {
        for (int i = 0; i < allShares.Length; i++)
        {
            if (allShares[i] == returnedShare)
            {
                shareOwner[i] = null;

                if (returnedShare.typeOfShares == "Corporation")
                {
                    SetCorporationCost(returnedShare, returnedShare.cost - GiveCorporationPrice(returnedShare));
                }
                break;
            }
        }
    }

    // говорит скрипту игрока начислить себе акцию, тот в свою очередь скажет какую цену ей установить если это корпорация
    public void GiveShareToPlayer(Player player, Shares share)
    {
        if (IsPlayerInGame(player))
        { 
            player.AddShare(share);
            for (int i = 0; i < allShares.Length; i++)
            {
                if (allShares[i] == share)
                {
                    shareOwner[i] = player;
                    break;
                }
            }
        }
    }




    public Shares GetRandomUnsoldShare()
    {

        float maxWeight = 0f;
        int maxWeightNumber = -1;
        for (int i = 0; i < allShares.Length; i++)
        {
            if (shareOwner[i] == null)
            {
                float weight = UnityEngine.Random.value;
                print("вес " + i + " : " + weight);
                if (weight > maxWeight)
                {
                    print("new max weight ");
                    maxWeight = weight;
                    maxWeightNumber = i;
                }
            }
        }

        if (maxWeightNumber != -1)
        {
            print("max weight number = " + maxWeightNumber);
            return allShares[maxWeightNumber];
        }
        else
        {
            return null;
        }




    }

    //функция возвращает ответ, есть ли у игрока с таким номером генератор товаров: прииск, бампер и т.п.
    public bool DoesPlayerHaveGoodsGeneratorNum(int playerNum)
    {
        bool generator = false;
        Player activePl = playerMovements[playerNum].GetComponent<Player>();
        for (int i = 0; i < goodsGenerators.Length; i++)
        {
            if (activePl.playerShares.Contains(goodsGenerators[i]))
            {
                generator = true;
            }
        }
        return generator;
    }

    //функция возвращает ответ, есть ли у игрока корпорации
    public bool DoesPlayerHaveCorporation(Player player)
    {
        bool haveCorp = false;

        for (int i = 0; i < tradableShares.Length; i++)
        {
            if (player.playerShares.Contains(tradableShares[i]))
            {
                haveCorp = true;
            }
        }
        return haveCorp;
    }

    public void ChangeGoodsForPlayerNum(int playerNum, int changes)
    {
        Player activePl = playerMovements[playerNum].GetComponent<Player>();
        for (int i = 0; i < goodsGenerators.Length; i++)
        {
            if (activePl.playerShares.Contains(goodsGenerators[i]))
            {
                ChangeGoodsCost(i, changes);
            }
        }

    }

    //возвращает цвет игрока с определенным номером
    public Color GetPlayerColorNum(int playerNum)
    {
        Player activePl = playerMovements[playerNum].GetComponent<Player>();
        return activePl.playerColor;
    }

    public Player GetPlayerByPlayerNum(int playerNum)
    {
        Player activePl = playerMovements[playerNum].GetComponent<Player>();
        return activePl;
    }

    //отправляет игркоа с определенным номером на клетку
    public void MovePlayerToCellNum(int playerNum, Cell cellToGo)
    {
        
        playerMovements[playerNum].MoveInactivePlayerToCell(cellToGo);
    }

    public void ChangePlayerWallet(int cashAdd, int goldAdd, int oilAdd, int carsAdd, int colaAdd, int drugsAdd, int robberyAdd, int colonyAdd)
    {
        Player player = playerMovements[activePlayerNum].GetComponent<Player>();
        player.WalletChange(cashAdd, goldAdd, oilAdd, carsAdd, colaAdd, drugsAdd, robberyAdd, colonyAdd);
    }

    public void ChangeBossWallet(int cashAdd, int goldAdd, int oilAdd, int carsAdd, int colaAdd, int drugsAdd, int robberyAdd, int colonyAdd)
    {
        Player bossPlayer = WhoIsBossPlayer();

        if (bossPlayer != null)
        {
            bossPlayer.WalletChange(cashAdd, goldAdd, oilAdd, carsAdd, colaAdd, drugsAdd, robberyAdd, colonyAdd);
        }
    }

    // возвращает номер игрока босса
    public int WhoIsBossNum()
    {
        int bossNum = -1;
        for (int i = 0; i < playerMovements.Length; i++)
        {
            Player player = playerMovements[i].GetComponent<Player>();
            if (player.bossCard && IsPlayerInGame(player))
            {
                bossNum = i;
                return bossNum;
            }
        }
        return bossNum;
    }

    //возвращает плеермувмент текущего босса
    public PlayerMovement WhoIsBossPlayerMvmnt()
    {
        int bossNum = WhoIsBossNum();
        if (bossNum != -1)
        {
            return playerMovements[bossNum];
        }
        else
        {
            return null;
        }
    }

    //возвращает плеера текущего босса
    public Player WhoIsBossPlayer()
    {
        PlayerMovement bossMvmnt = WhoIsBossPlayerMvmnt();
        if (bossMvmnt != null)
        {
            Player player = bossMvmnt.GetComponent<Player>();
            return player;
        }
        else
        {
            return null;
        }
    }

    public Player WhoIsPreviousPlayer()
    {
        PlayerMovement secondPlayer = playerMovements[(activePlayerNum + 1) % 4];
        PlayerMovement thirdPlayer = playerMovements[(activePlayerNum + 2) % 4];
        PlayerMovement fourthPlayer = playerMovements[(activePlayerNum + 3) % 4];
        Cell secondPlayerCell = secondPlayer.currentCell;
        Cell thirdPlayerCell = thirdPlayer.currentCell;
        Cell fourthPlayerCell = fourthPlayer.currentCell;
        Player victim = null;

        for (int i = 0; i < cellOrderFromRamboCell.Length; i++)
        {
            if (cellOrderFromRamboCell[i] == secondPlayerCell && secondPlayer.inGame)
            {
                victim = secondPlayer.GetComponent<Player>();
                break;
            }
            else if (cellOrderFromRamboCell[i] == thirdPlayerCell && thirdPlayer.inGame)
            {
                victim = thirdPlayer.GetComponent<Player>();
                break;
            }
            else if (cellOrderFromRamboCell[i] == fourthPlayerCell && fourthPlayer.inGame)
            {
                victim = fourthPlayer.GetComponent<Player>();
                break;
            }
        }
        print(victim);
        return victim;
    }

    //возвращает ответ, в игре ли все еще плеер с таким номером
    public bool IsPlayerInGameNum(int playerNum)
    {
        return playerMovements[playerNum].inGame;
    }

    public bool IsPlayerInGame(Player player)
    {
        PlayerMovement plMvmnt = player.GetComponent<PlayerMovement>();
        return plMvmnt.inGame;
    }

    public bool AreThereAnyPlayerForMovie()
    {
        bool exist = false;

        foreach (PlayerMovement playerI in playerMovements)
        {
            if (playerI.currentCell.cellWayTitle != "BUSINESS")
            {
                exist = true;

            }
        }
        return exist;
    }

    public void SetPlayerCards(bool boss, bool police, bool army, bool woolfy, bool rabby, bool taxFree, bool badHarvest, bool mediaCrisis, bool revenueBlock)
    {
        Player player = playerMovements[activePlayerNum].GetComponent<Player>();
        player.SetCard(boss, police, army, woolfy, rabby, taxFree, badHarvest, mediaCrisis, revenueBlock);
    }

    //если значение отрицательное, то забирает имеющуюся карточку, если положительное, то дает карточку, если 0, то ничего не делает
    public void ChangePlayerCards(int boss, int police, int army, int woolfy, int rabby, int taxFree, int badHarvest, int mediaCrisis, int revenueBlock)
    {
        Player player = playerMovements[activePlayerNum].GetComponent<Player>();
        player.ChangeCards(boss, police, army, woolfy, rabby, taxFree, badHarvest, mediaCrisis, revenueBlock);
    }


    void InitiatePlayer(Player player)
    {
        player.WalletChange(startingLoan, 0, 0, 0, 0, 0, 0, 0);
        player.startingLoan = startingLoan;
        player.SetCard(false, false, false, false, false, false, false, false, false);
        player.deposites = 0;
        player.loans = 0;
        player.colonyLoan = 0;
        player.laborIndex = 0;

    }


    public void SwitchCameraToPlayer(PlayerMovement playerMovement)
    {
        if (playerMovement.playerCamera != activeCamera)
        {
            activeCamera.gameObject.SetActive(false);
            playerMovement.playerCamera.gameObject.SetActive(true);
            activeCamera = playerMovement.playerCamera;
        }
    }

    public void SwitchCameraToFreeCamera()
    {
        if (freeCamera != activeCamera)
        {
            Transform activeCameraTransform = activeCamera.transform;
            activeCamera.gameObject.SetActive(false);
            freeCamera.gameObject.SetActive(true);
            activeCamera = freeCamera.cameraMain;
            freeCamera.FreeCameraMoveTo(activeCameraTransform, playerMovements[activePlayerNum].WhatIsDirection());
        }
    }

    public void NextPlayer()
    {
        activePlayerNum = (activePlayerNum + 1) % 4;
        if (IsPlayerInGameNum(activePlayerNum))
        {
            SetPlayerInUI();
            SwitchCameraToPlayer(playerMovements[activePlayerNum]);

            if (activeGMState != GameStates.START_DICE_THROW_WAITING)
            { 
                StartCoroutine(ChangePlayerCoroutine(changePlayerTime));
            }

        }
        else
        {
            NextPlayer();
        }
                  
    }

    public void NextPlayerTurn()
    {
        PlayerMovement playerMvmnt = WhoIsPlayerMVMNT();
        if (playerMvmnt.extraTurnNumber > 0 )
        {
            playerMvmnt.extraTurnNumber -= 1;
            StartCoroutine(ChangePlayerCoroutine(changePlayerTime));
        }
        else
        {
            NextPlayer();
        }
        



    }

    IEnumerator ChangePlayerCoroutine(float changePlayerTime)
    {
        yield return new WaitForSeconds(changePlayerTime);
        ChangeGMState(GameStates.TURN_CHECK_CHOOSE);
    }

    public void CurrentPlayerDiceTurn()
    {
        ChangeGMState(GameStates.TURN_DICE_THROW_WAITING);
    }


    //возвращает другим скриптам плеера, который сейчас активен
    public PlayerMovement WhoIsPlayerMVMNT()
    {
        return playerMovements[activePlayerNum];
    }

    public Player WhoIsPlayer()
    {
        return playerMovements[activePlayerNum].GetComponent<Player>();
    }

    //расставляем игроков в соответствии с тем, как они кинули кубики
    public void SortPlayerOnTheStart()
    {
        for (int i = 0; i < startDice.Length; i++)
        {
            for (int j = 0; j < startDice.Length - i - 1; j++)
            {
                if (startDice[j] < startDice[j + 1])
                {
                    int z = startDice[j];
                    PlayerMovement playerZ = playerMovements[j];

                    startDice[j] = startDice[j + 1];
                    playerMovements[j] = playerMovements[j + 1];

                    startDice[j + 1] = z;
                    playerMovements[j + 1] = playerZ;
                }
            }
        }



    }



    public void SlaveActivePlayer()
    {
        WhoIsPlayerMVMNT().JumpToCellAndActivateIt(slavaryMarket);
    }


    public void ThrowDice()
    {
        diceRolling = true;
        UIManager.HideYellowPanel();
        activeCamera.gameObject.SetActive(false);
        activeCamera = diceCamera;
        diceCameraObj.SetActive(true);
        dice.enabled = true;
        dice.ThrowDice();
    }


    // РАБОТА С СОСТОЯНИЯМИ
    //
    //
    //изменение состояния

    [Header("Состояния игры")]
    GameStates activeGMState; // активное состояние

    // STARTDICEWAITING
    //сколько каждый из игроков кинул на старте
    int[] startDice = { 0, 0, 0, 0 };


    bool diceRolling = false;

    //




    enum GameStates
    {

        START_DICE_THROW_WAITING, //ожидаем, когда игрок кинет кубик, для начальной сортировки игроков
        TURN_DICE_THROW_WAITING, // ожидаем, когда игрок кинет кубик, чтобы понять сколько ему нужно походить
        TURN_CAP_MOVING_WAITING, // ожидаем, когда игрок походит фишкой
        TURN_CHECK_CHOOSE //перед каждым броском кубика проверяем не стоит ли игрок на клетке с выбором

    }


    void ChangeGMState(GameStates newState)
    {

        activeGMState = newState;

        switch (newState)
        {
            case GameStates.START_DICE_THROW_WAITING:

                UIManager.Instance.ShowDicePanel(); //включаем панель броска кубика
                activePlayerNum = 0;
                SwitchCameraToPlayer(playerMovements[activePlayerNum]); //переводим камеру на игрока

                startDice[0] = 0;
                startDice[1] = 0;
                startDice[2] = 0;
                startDice[3] = 0;

                break;

            case GameStates.TURN_CHECK_CHOOSE: //стадия проверки, есть ли начальный выбор

                UIManager.HideYellowPanel();
                if (!playerMovements[activePlayerNum].CheckStartingChoose())
                {
                    ChangeGMState(GameStates.TURN_DICE_THROW_WAITING);
                }
                break;

            case GameStates.TURN_DICE_THROW_WAITING:

                if (playerMovements[activePlayerNum].turnMiss <= 0)
                {
                    UIManager.ShowDicePanel();
                    SwitchCameraToPlayer(playerMovements[activePlayerNum]);
                }
                else
                {
                    playerMovements[activePlayerNum].turnMiss -= 1;
                    NextPlayerTurn();
                }
                break;

            case GameStates.TURN_CAP_MOVING_WAITING:

                UIManager.ShowMoveArrowPanel();
                break;
        }

    }

    void StateUpdate()
    {
        switch (activeGMState)
        {
            case GameStates.START_DICE_THROW_WAITING:
                if (!diceRolling)
                {
                    if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))&&gameStarted)
                    {
                        ThrowDice();
                    }
                }


                break;

            case GameStates.TURN_DICE_THROW_WAITING:
                if (!diceRolling)
                {
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        ThrowDice();
                    }
                }


                break;
        }

    }


    void DiceResult()
    {
        switch (activeGMState)
        {
            case GameStates.START_DICE_THROW_WAITING:
                startDice[activePlayerNum] = dice.diceLastThrow;
                diceRolling = false;
                dice.enabled = false;

                if (activePlayerNum == 3)
                {
                    SortPlayerOnTheStart();
                    NextPlayer();
                    ChangeGMState(GameStates.TURN_CHECK_CHOOSE);
                }
                else
                {
                    NextPlayer();
                    UIManager.ShowDicePanel();
                }

                break;


            case GameStates.TURN_DICE_THROW_WAITING:
                diceRolling = false;
                dice.enabled = false;
                playerMovements[activePlayerNum].GiveTheTurnToPlayer(dice.diceLastThrow);
                ChangeGMState(GameStates.TURN_CAP_MOVING_WAITING);
                break;

            case GameStates.TURN_CAP_MOVING_WAITING:
                diceRolling = false;
                dice.enabled = false;
                if (dice.diceLastThrow == 6)
                {
                    Player player = WhoIsPlayer();
                    player.WalletChange(player.casinoPossibleReward, 0, 0, 0, 0, 0, 0, 0);
                }
                NextPlayerTurn();
                break;
        }
    }

    // Функция начисления всего в конце хода
    public void GiveRoundBonusToPlayer(Player player)
    {
        player.RevenueCount();
    }

    public void GiveRoundBonusToActivePlayer()
    {
        Player player = WhoIsPlayer();
        GiveRoundBonusToPlayer(player);
    }

    //Проигрыши и победы

    public void RetireActivePlayer()
    {
        int winner = -1;
        retiredPlayers += 1;
        if (retiredPlayers == 3)
        {
            winner = WhoIslastPlayer();
            GameOver(winner, true);

        }
        else
        { 
            PlayerMovement activePlayerMVMNT = WhoIsPlayerMVMNT();
            Player activePlayer = WhoIsPlayer();
            activePlayer.ReturnAllShares();
            activePlayerMVMNT.inGame = false;
            activePlayerMVMNT.GoToCellWithoutActivation(cellsNumber[0]);
            NextPlayer();
        
        }


    }

    int WhoIslastPlayer()
    {
        int foundNum = -1;
        for (int i = 0; i < 4; i++)
        {
            if (IsPlayerInGameNum(i))
            {
                foundNum =  i;
                break;
            }
        }
        return foundNum;
    }

    public void IfLastRound(int round)
    {
        if (round >= maxRoundBeforeEnd)
        {
            
            GameOver(CountWinner(), true);
        }
    }

    public int CountWinner()
    {
        int maxCash = playerMovements[3].GetComponent<Player>().cash;
        winnerPlayerNum = 3;
        for (int i = 0; i < 4; i++)
        {
            int cashI = playerMovements[3 - i].GetComponent<Player>().cash;
            if (cashI > maxCash)
            {
                winnerPlayerNum = 3 - i;
                maxCash = cashI;
            }
        }
        return winnerPlayerNum;
    }

    public void GameOver(int winnerPlayerNum, bool winOrDraw)
    {
        Player winner = playerMovements[winnerPlayerNum].GetComponent<Player>();
        UIManager.Instance.ShowGameOverWindow(winner, winOrDraw);

    }
}
