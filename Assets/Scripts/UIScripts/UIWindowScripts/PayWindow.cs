using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayWindow : MonoBehaviour
{
    [SerializeField] Text payCashTxt;
    [SerializeField] Text payGoldTxt;
    [SerializeField] GameObject gold;
    [SerializeField] Text payOilTxt;
    [SerializeField] GameObject oil;
    [SerializeField] Text payCarsTxt;
    [SerializeField] GameObject cars;
    [SerializeField] Text payColaTxt;
    [SerializeField] GameObject cola;
    [SerializeField] Text payDrugsTxt;
    [SerializeField] GameObject drugs;
    [SerializeField] Text bossTaxTxt;
    [SerializeField] GameObject bankTaxTxt;

    [SerializeField] CanvasGroup window;
    int payCashSum;
    int payGoldSum;
    int payOilSum;
    int payCarsSum;
    int payColaSum;
    int payDrugsSum;
    int robbery;
    int colony;
    Player bossPlayer;
    Player beneficiarW;
    int beneficiarFreeCashW;
    Player beneficiarSecondW;
    int beneficiarFreeCashSecondW;
    int goldCostW;
    int oilCostW;
    int carsCostW;
    int colaCostW;
    [SerializeField] int laborChangesW;
    int policeW;
    int armyW;
    int woolfyW;
    int rabbyW;
    int taxFreeW;
    int badHarvestW;
    int mediaCrisisW;
    int revenueBlockW;
    Cell cellToMoveWithActionW;
    Cell cellToMoveWithoutActionW;
    Player activePlayer;
    bool buttonDown = false;

    public void OpenWindow(int cashAdd,int goldAdd,int oilAdd,int carsAdd,int colaAdd,int drugsAdd, int robberyAdd, int colonyAdd, Player boss,
            Player beneficiar, int beneficiarFreeCash, Player beneficiarSecond, int beneficiarFreeCashSecond, 
            int goldCost, int oilCost, int carsCost, int colaCost, int laborChanges,
            int police, int army, int woolfy, int rabby, int taxFree, int badHarvest, int mediaCrisis,
                            Cell cellToMoveWithAction, Cell cellToMoveWithoutAction, int revenueBlock)
    {
        buttonDown = false;
        print("payCashSum " + payCashSum);
        activePlayer = GameManager.Instance.WhoIsPlayer();
        payCashSum = cashAdd;
        payGoldSum = goldAdd;
        payOilSum = oilAdd;
        payCarsSum = carsAdd;
        payColaSum = colaAdd;
        payDrugsSum = drugsAdd;
        robbery = robberyAdd;
        colony = colonyAdd;
        bossPlayer = boss;
        beneficiarW = beneficiar;
        beneficiarFreeCashW = beneficiarFreeCash;
        beneficiarSecondW = beneficiarSecond; // первый бенефициар получает как минус доход игрока, так и свой фрикэш
        beneficiarFreeCashSecondW = beneficiarFreeCashSecond; //второй бенифициар получает только фрикэш
        goldCostW = goldCost;
        oilCostW = oilCost;
        carsCostW = carsCost;
        colaCostW = colaCost;
        laborChangesW = laborChanges;
        policeW = police;
        armyW = army;
        woolfyW = woolfy;
        rabbyW = rabby;
        taxFreeW = taxFree;
        badHarvestW = badHarvest;
        mediaCrisisW = mediaCrisis;
        revenueBlockW = revenueBlock;
        cellToMoveWithActionW = cellToMoveWithAction;
        cellToMoveWithoutActionW = cellToMoveWithoutAction;


        bossTaxTxt.gameObject.SetActive(false);



        payCashTxt.gameObject.SetActive(payCashSum != 0);

        print("payCashSum " + (payCashSum != 0));

        gold.SetActive(payGoldSum != 0);
        oil.SetActive(payOilSum != 0);
        cars.SetActive(payCarsSum != 0);
        cola.SetActive(payColaSum != 0);
        drugs.SetActive(payDrugsSum != 0);
        bankTaxTxt.SetActive(false);

        if (payCashSum > 0)
        {
            payCashTxt.text = "+" + payCashSum +"$";
            payCashTxt.color = Color.green;
            if (bossPlayer != null)
            {
                bossTaxTxt.gameObject.SetActive(true);
            }

            bankTaxTxt.SetActive(activePlayer.revenueBlockCard);
        }
        else
        {
            payCashTxt.text = "" + payCashSum + "$";
            payCashTxt.color = Color.red;
        }
        ///
        if (payGoldSum > 0)
        {
            payGoldTxt.text = "+" + payGoldSum ;
            payGoldTxt.color = Color.green;
        }
        else
        {
            payGoldTxt.text = "" + payGoldSum;
            payGoldTxt.color = Color.red;
        }

        ///
        if (payOilSum > 0)
        {
            payOilTxt.text = "+" + payOilSum ;
            payOilTxt.color = Color.green;
        }
        else
        {
            payOilTxt.text = "" + payOilSum ;
            payOilTxt.color = Color.red;
        }
        ///

        if (payCarsSum > 0)
        {
            payCarsTxt.text = "+" + payCarsSum ;
            payCarsTxt.color = Color.green;
        }
        else
        {
            payCarsTxt.text = "" + payCarsSum ;
            payCarsTxt.color = Color.red;
        }


        if (payColaSum > 0)
        {
            payColaTxt.text = "+" + payColaSum;
            payColaTxt.color = Color.green;
        }
        else
        {
            payColaTxt.text = "" + payColaSum;
            payColaTxt.color = Color.red;
        }

        if (payDrugsSum > 0)
        {
            payDrugsTxt.text = "+" + payDrugsSum;
            payDrugsTxt.color = Color.green;
        }
        else
        {
            payDrugsTxt.text = "" + payDrugsSum;
            payDrugsTxt.color = Color.red;
        }


    }

    public void OkButton()
    {
        if (!buttonDown)
        {
            buttonDown = true;
            GameManager.Instance.ChangePlayerWallet(payCashSum, payGoldSum, payOilSum, payCarsSum, payColaSum, payDrugsSum, robbery, colony);
            GameManager.Instance.ChangeGoodsCost(0, goldCostW);
            GameManager.Instance.ChangeGoodsCost(1, oilCostW);
            GameManager.Instance.ChangeGoodsCost(2, carsCostW);
            GameManager.Instance.ChangeGoodsCost(3, colaCostW);
            GameManager.Instance.ChangePlayerCards(0, policeW, armyW, woolfyW, rabbyW, taxFreeW, badHarvestW, mediaCrisisW, revenueBlockW);
            activePlayer.LaborIndexChanges(laborChangesW);


            if (bossPlayer != null && payCashSum > 0)
            {
                GameManager.Instance.ChangeBossWallet(payCashSum, 0, 0, 0, 0, 0, 0, 0);
            }

            if (beneficiarW != null)
            {
                beneficiarW.WalletChange(-payCashSum + beneficiarFreeCashW, 0, 0, 0, 0, 0, 0, 0);
            }

            if (beneficiarSecondW != null)
            {
                beneficiarSecondW.WalletChange(beneficiarFreeCashSecondW, 0, 0, 0, 0, 0, 0, 0);
            }


            if (cellToMoveWithoutActionW != null)
            {
                GameManager.Instance.WhoIsPlayerMVMNT().GoToCellWithoutActivation(cellToMoveWithoutActionW);
                GameManager.Instance.NextPlayerTurn();
                UIManager.Instance.HideWindow(window);

            }
            else if (cellToMoveWithActionW != null)
            {
                GameManager.Instance.WhoIsPlayerMVMNT().JumpToCellAndActivateIt(cellToMoveWithActionW);
                UIManager.Instance.HideWindow(window);

            }
            else if (cellToMoveWithoutActionW == null && cellToMoveWithActionW == null)
            {
                GameManager.Instance.NextPlayerTurn();
                UIManager.Instance.HideWindow(window);
            }
        }

    }

}
