using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; private set; }

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

    }
    #endregion
    


    [Header("Появление окон")]
    [SerializeField] float fadeDuration;
    public float timeToDrawShare = 0.1f; //задержка при отрисовке акций в окне списка акций игрока
    [SerializeField] UIActivePlayer activePlayerUI;
    [SerializeField] UINotActivePlayer secondPlayerUI;
    [SerializeField] UINotActivePlayer thirdPlayerUI;
    [SerializeField] UINotActivePlayer fourthPlayerUI;



    [Header("Различные желтые панели")]
    [SerializeField] GameObject yellowPanel;
    [SerializeField] GameObject throwDicePanel;
    [SerializeField] GameObject moveArrowsPanel;
    [SerializeField] GameObject planePanel;

    GameObject activeYellowPanel;

    
    [Header("Различные окна")]
    [SerializeField] GameObject descriptionWindow; //окно с описанием клетки
    [SerializeField] GameObject paySmthWindow; //окно, где игроку нужно что-то заплатить
    [SerializeField] GameObject paySmbWindow; //окно оплаты кому-то чего-то
    [SerializeField] GameObject unemployWindow; //окно на клетке 1
    [SerializeField] GameObject buyCardWindow; //окно покупки карточки
    [SerializeField] GameObject missTurnWindow; // окно пропуска хода
    [SerializeField] GameObject buyDrugsWindow; //окно покупки или сбыта наркоты
    [SerializeField] GameObject buyBrainsWindow; // окно вложения в мозги
    [SerializeField] GameObject goodsExchangeWindow; //окно покупки или сбыта товаров
    [SerializeField] GameObject playerSharesWindow; //окошко со всеми акциями игрока
    [SerializeField] GameObject speculationExchangeWindow; //окошко со всеми акциями и коэффициентом продажи
    [SerializeField] GameObject bankWindow; //окно с кредитами и депозитами
    [SerializeField] GameObject starWindow; //окно выбора звезды
    [SerializeField] GameObject chooseCompetitorWindow; //окно выбора противника, чтобы ему напакостить
    [SerializeField] GameObject buyShareWindow; //окно выбора противника, чтобы ему напакостить
    [SerializeField] GameObject donkeyWindow; //окно с осликом
    [SerializeField] CanvasGroup cultregerWindow; //окно с выбором в колонии идти или нет
    [SerializeField] GameObject movieWindow; //окно с предложением постоять и получить немного денег
    [SerializeField] GameObject smbMissTurnWindow; //окно, в котором показывается, что какой-то игрок пропускает ходы
    [SerializeField] GameObject tourBureauWindow; //окно, в котором предлагается отправиться в колонии
    [SerializeField] GameObject extraTurnWindow; // окно с доп.ходом
    [SerializeField] GameObject riffWindow;//окно, где игрок попадает на рифф и теряет безделушки
    [SerializeField] GameObject chanceWindow; //окно Шанса, движение назад
    [SerializeField] GameObject auctionWindow;//окно аукциона



    bool isDialogOpen = false;



    public void ShowWindow(CanvasGroup window)
    {
        isDialogOpen = true;
        window.gameObject.SetActive(true);
        window.alpha = 0;
        window.DOFade(1, fadeDuration).SetUpdate(true);

    }

    public void HideWindow(CanvasGroup window)
    {
        window.DOFade(0, fadeDuration).OnComplete(
               () => {
                   window.gameObject.SetActive(false);
                   isDialogOpen = false;
               }
           ).SetUpdate(true);
    }

    public void SetPlayersUI(Player activePlayer, Player secondPlayer, Player thirdPlayer, Player fourthPlayer)
    {
        activePlayerUI.SetPlayer(activePlayer);
        secondPlayerUI.SetPlayer(secondPlayer);
        thirdPlayerUI.SetPlayer(thirdPlayer);
        fourthPlayerUI.SetPlayer(fourthPlayer);

    }

    //
    // работа с желтыми панелями 
    //


    public void ShowDicePanel()
    {
        if (activeYellowPanel != null)
        {
            activeYellowPanel.SetActive(false);
        }
        yellowPanel.SetActive(true);
        throwDicePanel.SetActive(true);
        activeYellowPanel = throwDicePanel;
    }

    public void ShowMoveArrowPanel()
    {
        if (activeYellowPanel != null)
        {
            activeYellowPanel.SetActive(false);
        }
        yellowPanel.SetActive(true);
        moveArrowsPanel.SetActive(true);
        activeYellowPanel = moveArrowsPanel;
        moveArrowsPanel.GetComponent<UIMovementLeft>().LinkToPlayerMovement();
    }

    public void ShowPlanePanel()
    {
        if (activeYellowPanel != null)
        {
            activeYellowPanel.SetActive(false);
        }
        yellowPanel.SetActive(true);
        planePanel.SetActive(true);
        activeYellowPanel = planePanel;


    }

    public void HideYellowPanel()
    {
        yellowPanel.SetActive(false);
    }


    // РАБОТА С ОКНАМИ

        //общее отображение описания окна
    private void ChangeContentDescriptionAndShowWindow(GameObject window, Sprite cellIcon, string cellTitle, string cellDescription, string cellWayTitle)
    {
        window.SetActive(true);
        DescriptionWindow description = window.GetComponent<DescriptionWindow>();
        description.ChangeDescriptionWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
        CanvasGroup windowCanvasGr = window.GetComponent<CanvasGroup>();
        ShowWindow(windowCanvasGr);
    }




    public void ShowDescriptionWindow(Sprite cellIcon, string cellTitle, string cellDescription, string cellWayTitle)
    {
        descriptionWindow.SetActive(true);
        DescriptionWindow description = descriptionWindow.GetComponent<DescriptionWindow>();
        description.ChangeDescriptionWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
        CanvasGroup descrWindowCanvasGr = descriptionWindow.GetComponent<CanvasGroup>();
        ShowWindow(descrWindowCanvasGr);
    }

    private void ShowWindowWithoutDescription(GameObject window)
    {
        window.SetActive(true);
        CanvasGroup windowCanvasGr = window.GetComponent<CanvasGroup>();
        ShowWindow(windowCanvasGr);
    }


    //Окно, где игроку либо что-то дают, либо что-то забирают
    public void ShowPaySmthWindow(Sprite cellIcon,
                                  string cellTitle,
                                  string cellDescription,
                                  string cellWayTitle,
                                  int cashAdd,
                                  int goldAdd,
                                  int oilAdd,
                                  int carsAdd,
                                  int colaAdd,
                                  int drugsAdd,
                                  int robberyAdd,
                                  int colonyAdd,
                                  Player boss,
                                  Player beneficiar,
                                  int beneficiarFreeCash,
                                  Player beneficiarSecond,
                                  int beneficiarFreeCashSecond,
                                  int goldCost,
                                  int oilCost,
                                  int carsCost,
                                  int colaCost,
                                  int laborChanges,
                                  int police,
                                  int army,
                                  int woolfy,
                                  int rabby,
                                  int taxFree,
                                  int badHarvest,
                                  int mediaCrisis,
                                  Cell cellToMoveWithAction,
                                  Cell cellToMoveWithoutAction,
                                  int revenueBlock)
    {
        ChangeContentDescriptionAndShowWindow(paySmthWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        PayWindow paySmth = paySmthWindow.GetComponent<PayWindow>();
        paySmth.OpenWindow(cashAdd, goldAdd, oilAdd, carsAdd, colaAdd, drugsAdd, robberyAdd, colonyAdd, 
                            boss, beneficiar, beneficiarFreeCash, beneficiarSecond, beneficiarFreeCashSecond,
                            goldCost, oilCost, carsCost, colaCost, laborChanges, police, army, woolfy,  rabby, taxFree, badHarvest, mediaCrisis, 
                            cellToMoveWithAction, cellToMoveWithoutAction, revenueBlock);
    }

    public void ShowPaySmbWindow(Sprite cellIcon,
                              string cellTitle,
                              string cellDescription,
                              string cellWayTitle,
                              int cashAdd,
                              int goldAdd,
                              int oilAdd,
                              int carsAdd,
                              int colaAdd,
                              int drugsAdd,
                              int robberyAdd,
                              int colonyAdd,
                              Player beneficiar,
                              int beneficiarFreeCash)
    {
        ChangeContentDescriptionAndShowWindow(paySmbWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        PaySmbWindow paySmbW = paySmbWindow.GetComponent<PaySmbWindow>();
        paySmbW.OpenWindow(cashAdd, goldAdd, oilAdd, carsAdd, colaAdd, drugsAdd, robberyAdd, colonyAdd, beneficiar, beneficiarFreeCash);
    }




    public void ShowCultregerWindow()
    {
        ShowWindow(cultregerWindow);

    }

    public void ShowMovieWindow(Sprite cellIcon,
                                     string cellTitle,
                                     string cellDescription,
                                     string cellWayTitle)
    {
        ChangeContentDescriptionAndShowWindow(movieWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
    }


    public void ShowUnemployWindow(Sprite cellIcon,
                                     string cellTitle,
                                     string cellDescription,
                                     string cellWayTitle)
    {
        ChangeContentDescriptionAndShowWindow(unemployWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);

    }

    public void ShowCardBuyWindow(Sprite cellIcon,
                                     string cellTitle,
                                     string cellDescription,
                                     string cellWayTitle,
                                     int cashCost,
                                     bool boss,
                                     bool police,
                                     bool army,
                                     bool woolfy,
                                     bool rabby)
    {
        ChangeContentDescriptionAndShowWindow(buyCardWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        BuyCardWindow buyCardW = buyCardWindow.GetComponent<BuyCardWindow>();
        buyCardW.OpenWindow(cashCost, boss, police, army, woolfy, rabby);

    }

    public void ShowMissTurnWindow(Sprite cellIcon,
                                     string cellTitle,
                                     string cellDescription,
                                     string cellWayTitle,
                                     int cost, 
                                     int miss, 
                                     string missType, 
                                     Cell prisonCell,
                                     Cell rescueCell)
    {
        ChangeContentDescriptionAndShowWindow(missTurnWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        MissTurnWindow missTurnW = missTurnWindow.GetComponent<MissTurnWindow>();
        missTurnW.OpenWindow(cost, miss, missType, prisonCell, rescueCell);

    }

    public void ShowBuyDrugsWindow(Sprite cellIcon,
                                     string cellTitle,
                                     string cellDescription,
                                     string cellWayTitle,
                                    int drugsCost,
                                    int drugsMaxNumber,
                                    string goodsType, 
                                    string wayType)
    {
        ChangeContentDescriptionAndShowWindow(buyDrugsWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        BuyDrugsWindow buyDrugsW = buyDrugsWindow.GetComponent<BuyDrugsWindow>();
        buyDrugsW.OpenWindow(drugsCost, drugsMaxNumber, goodsType, wayType);
    }


    public void ShowBuyBrainsWindow(Sprite cellIcon,
                                     string cellTitle,
                                     string cellDescription,
                                     string cellWayTitle,
                                    int brainsCost,
                                    int brainsMaxNumber)
    {
        ChangeContentDescriptionAndShowWindow(buyBrainsWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        BuyBrainsWindow buyBrainsW = buyBrainsWindow.GetComponent<BuyBrainsWindow>();
        buyBrainsW.OpenWindow(brainsCost, brainsMaxNumber);
    }


    public void ShowGoodsExchangeWindow(int maxGold, int maxOil, int maxCars, int maxCola)
    {
        goodsExchangeWindow.SetActive(true);
        GoodsExchangeWindow goodExW = goodsExchangeWindow.GetComponent<GoodsExchangeWindow>();
        goodExW.OpenWindow(maxGold, maxOil, maxCars, maxCola);
        ShowWindow(goodsExchangeWindow.GetComponent<CanvasGroup>());
    }

    public void ShowPlayerSharesWindow()
    {
        playerSharesWindow.SetActive(true);
        PlayerSharesWindow playerSharesW = playerSharesWindow.GetComponent<PlayerSharesWindow>();
        Player player = GameManager.Instance.WhoIsPlayer();
        playerSharesW.OpenWindow(player);
        ShowWindow(playerSharesWindow.GetComponent<CanvasGroup>());
    }



    public void ShowBankWindow()
    {
        bankWindow.SetActive(true);
        BankWindow bankW = bankWindow.GetComponent<BankWindow>();
        bankW.OpenWindow(GameManager.Instance.WhoIsPlayer());
        ShowWindow(bankWindow.GetComponent<CanvasGroup>());
    }

    public void ShowSpeculationExchangeWindow(float coef)
    {
        speculationExchangeWindow.SetActive(true);
        SpeculationExchange speculationExchangeW = speculationExchangeWindow.GetComponent<SpeculationExchange>();
        speculationExchangeW.OpenWindow(GameManager.Instance.WhoIsPlayer(), coef);
        ShowWindow(speculationExchangeWindow.GetComponent<CanvasGroup>());
    }


    public void ShowStarWindow(Sprite cellIcon,
                                     string cellTitle,
                                     string cellDescription,
                                     string cellWayTitle,
                                     int starCost,
                                     int starNum)
    {
        ChangeContentDescriptionAndShowWindow(starWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        StarWindow starW = starWindow.GetComponent<StarWindow>();
        starW.OpenWindow(starCost, starNum);

    }

    public void ShowChooseCompetitorWindow(Sprite cellIcon,
                                            string cellTitle,
                                            string cellDescription,
                                            string cellWayTitle,
                                            int secondPlayerNum,
                                            int thirdPlayerNum,
                                            int fourthPlayerNum,
                                            int killCost,
                                            Cell cellToMove,
                                            string typeOfWindow,
                                            int goodsChanges)
    {
        ChangeContentDescriptionAndShowWindow(chooseCompetitorWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        ChooseCompetitorWindow chooseCompetitorW = chooseCompetitorWindow.GetComponent<ChooseCompetitorWindow>();
        chooseCompetitorW.OpenWindow(secondPlayerNum,
                                     thirdPlayerNum,
                                     fourthPlayerNum,
                                     killCost,
                                     cellToMove,
                                     typeOfWindow,
                                     goodsChanges);

    }

    public void ShowBuyShareWindow(Sprite cellIcon,
                                   string cellTitle,
                                   string cellDescription,
                                   string cellWayTitle,
                                   Player owner,
                                   Shares share,
                                   int cost
                                   )
    {
        ChangeContentDescriptionAndShowWindow(buyShareWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        BuyShareWindow buyShareW = buyShareWindow.GetComponent<BuyShareWindow>();
        buyShareW.OpenWindow(owner, share, cost);
    }

    public void ShowDonkeyWindow(Sprite cellIcon,
                               string cellTitle,
                               string cellDescription,
                               string cellWayTitle,
                               Player compet_1,
                               Player compet_2,
                               Player compet_3,
                               int cashSum
                               )
    {
        ChangeContentDescriptionAndShowWindow(donkeyWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        DonkeyWindow donkeyW = donkeyWindow.GetComponent<DonkeyWindow>();
        donkeyW.OpenWindow(compet_1, compet_2, compet_3, cashSum);
    }


    public void ShowSmbMissTurnWindow(Sprite cellIcon,
                                      string cellTitle,
                                      string cellDescription,
                                      string cellWayTitle,
                                      Player missPlayer,
                                      int missTurn)
    {
        ChangeContentDescriptionAndShowWindow(smbMissTurnWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        SmbMissTurnWindow smbMissTurnW = smbMissTurnWindow.GetComponent<SmbMissTurnWindow>();
        smbMissTurnW.OpenWindow(missPlayer, missTurn);
    }

    public void ShowTourBureauWindow(Sprite cellIcon,
                               string cellTitle,
                               string cellDescription,
                               string cellWayTitle)
    {
        ChangeContentDescriptionAndShowWindow(tourBureauWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);
        

    }

    public void ShowExtraTurnWindow(Sprite cellIcon,
                               string cellTitle,
                               string cellDescription,
                               string cellWayTitle)
    {
        ChangeContentDescriptionAndShowWindow(extraTurnWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);


    }

    public void ShowRiffWindow(Sprite cellIcon,
                           string cellTitle,
                           string cellDescription,
                           string cellWayTitle)
    {
        ChangeContentDescriptionAndShowWindow(riffWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);


    }

    public void ShowChanceWindow(Sprite cellIcon,
                           string cellTitle,
                           string cellDescription,
                           string cellWayTitle)
    {
        ChangeContentDescriptionAndShowWindow(chanceWindow, cellIcon, cellTitle, cellDescription, cellWayTitle);


    }

    //[SerializeField] Shares toAuction;
    public void ShowAuctionWindow(Shares share, int secondPlayerNum, int thirdPlayerNum, int fourthPlayerNum)
    {
        ShowWindowWithoutDescription(auctionWindow);
        AuctionWindow auctionW = auctionWindow.GetComponent<AuctionWindow>();
        auctionW.OpenWindow(share, secondPlayerNum, thirdPlayerNum, fourthPlayerNum);
        //auctionW.OpenWindow(toAuction, 1, 2, 3);
    }
}
