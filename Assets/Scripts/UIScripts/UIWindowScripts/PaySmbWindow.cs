using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaySmbWindow : MonoBehaviour
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

    [SerializeField] CanvasGroup window;
    int payCashSum;
    int payGoldSum;
    int payOilSum;
    int payCarsSum;
    int payColaSum;
    int payDrugsSum;
    int robbery;
    int colony;
    Player beneficiarPlayer;
    int benefFreeCash;

    public void OpenWindow(int cashAdd, int goldAdd, int oilAdd, int carsAdd, int colaAdd, int drugsAdd, int robberyAdd, int colonyAdd, 
                            Player beneficiar, int beneficiarFreeCash) 
        //beneficiarFreeCash - бабки, которые бенефициар получит не от игрока, а просто
    {
        payCashSum = cashAdd;
        payGoldSum = goldAdd;
        payOilSum = oilAdd;
        payCarsSum = carsAdd;
        payColaSum = colaAdd;
        payDrugsSum = drugsAdd;
        robbery = robberyAdd;
        colony = colonyAdd;
        beneficiarPlayer = beneficiar;
        benefFreeCash = beneficiarFreeCash;
        bossTaxTxt.gameObject.SetActive(false);



        payCashTxt.gameObject.SetActive(payCashSum != 0);
        gold.SetActive(payGoldSum != 0);
        oil.SetActive(payOilSum != 0);
        cars.SetActive(payCarsSum != 0);
        cola.SetActive(payColaSum != 0);
        drugs.SetActive(payDrugsSum != 0);

        if (payCashSum > 0)
        {
            payCashTxt.text = "+" + payCashSum + "$";
            payCashTxt.color = Color.green;
        }
        else
        {
            payCashTxt.text = "" + payCashSum + "$";
            payCashTxt.color = Color.red;
        }
        ///
        if (payGoldSum > 0)
        {
            payGoldTxt.text = "+" + payGoldSum;
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
            payOilTxt.text = "+" + payOilSum;
            payOilTxt.color = Color.green;
        }
        else
        {
            payOilTxt.text = "" + payOilSum;
            payOilTxt.color = Color.red;
        }
        ///

        if (payCarsSum > 0)
        {
            payCarsTxt.text = "+" + payCarsSum;
            payCarsTxt.color = Color.green;
        }
        else
        {
            payCarsTxt.text = "" + payCarsSum;
            payCarsTxt.color = Color.red;
        }


        if (payColaSum > 0)
        {
            payColaTxt.text = "+" + payColaSum + "$";
            payColaTxt.color = Color.green;
        }
        else
        {
            payColaTxt.text = "" + payColaSum + "$";
            payColaTxt.color = Color.red;
        }

    }

    public void OkButton()
    {
        GameManager.Instance.ChangePlayerWallet(payCashSum, payGoldSum, payOilSum, payCarsSum, payColaSum, payDrugsSum, robbery, colony);
        if (beneficiarPlayer != null)
        {
            beneficiarPlayer.WalletChange(-payCashSum + benefFreeCash, 0, 0, 0, 0, 0, 0, 0);
        }
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);

    }
}
