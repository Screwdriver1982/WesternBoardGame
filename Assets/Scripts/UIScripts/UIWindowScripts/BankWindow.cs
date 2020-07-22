using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] InputField putDeposite;
    [SerializeField] InputField getDeposite;
    [SerializeField] InputField returnLoan;
    [SerializeField] InputField takeLoan;
    [SerializeField] GameObject depositePanel;
    [SerializeField] GameObject creditPanel;
    [SerializeField] Text currentDepositeTxt;
    [SerializeField] Text currentDepositePercentTxt;
    [SerializeField] Text currentLoanTxt;
    [SerializeField] Text currentLoanPercentTxt;
    [SerializeField] Text availabeLoanTxt;

    [SerializeField] int maxDepositeSum;
    [SerializeField] int maxLoanSum;
    Player player;

    int currentDeposite;
    float currentDepPercent;
    int currentLoan;
    int currentLoanOnRound;
    float currentLoanPercent;

    int putDepositeInt;
    int getDepositeInt;
    int returnLoanInt;
    int takeLoanInt;
    int availabeLoanInt;



    public void OpenWindow(Player playerInit)
    {
        player = playerInit;
        currentDeposite = player.deposites;
        currentLoan = player.loans;


        if (currentDeposite >= 0)
        {
        
            currentDepPercent = player.depositePercents;
            currentDepositeTxt.text = currentDeposite + "$";
            currentDepositePercentTxt.text = currentDepPercent * 100f + "%";

        }
        
        if (currentLoan >= 0)
        {
            
            currentLoanTxt.text = currentLoan + "$";
            currentLoanPercent = player.loanPercents;
            currentLoanPercentTxt.text = currentLoanPercent * 100f + "%";
            currentLoanOnRound = player.loanOnThisRound;
            availabeLoanInt = GameManager.Instance.maxRoundLoan - currentLoanOnRound;
            availabeLoanTxt.text = availabeLoanInt + "$";

        }

        creditPanel.SetActive(currentDeposite <= 0);
        depositePanel.SetActive(currentLoan <= 0);


    }

    public void GetDepositeButton()
    {

        if (getDeposite.text != null)
        { 
            getDepositeInt = int.Parse(getDeposite.text);


            if (getDepositeInt > 0 && getDepositeInt <= currentDeposite)
            { 
                player.WalletChange(getDepositeInt, 0, 0, 0, 0, 0, 0, 0);
                player.deposites -= getDepositeInt;
                getDeposite.text = "";
                OpenWindow(player);
            }
            else
            {
                print("set new input");
                getDeposite.text = Mathf.Clamp(getDepositeInt, 0, currentDeposite)+"";
                print(getDeposite.text);
        
            }
        }



    }
    public void PutDepositeButton()
    {
        if (putDeposite.text != null)
        {
            putDepositeInt = int.Parse(putDeposite.text);
            if (putDepositeInt <= player.cash && putDepositeInt >= 0)
            {
                player.WalletChange(-putDepositeInt, 0, 0, 0, 0, 0, 0, 0);
                putDeposite.text = "";
                player.deposites += putDepositeInt;
                OpenWindow(player);

            }
            else if (putDepositeInt > player.cash)
            {
                putDeposite.text = player.cash + "";
            }
            else if (putDepositeInt < 0)
            {
                putDeposite.text = "0";
            }
            
        }
    }


    public void TakeLoanButton()
    {

        if (takeLoan.text != null)
        {
            takeLoanInt = int.Parse(takeLoan.text);


            if (takeLoanInt > 0 && takeLoanInt <= availabeLoanInt)
            {
                player.WalletChange(takeLoanInt, 0, 0, 0, 0, 0, 0, 0);
                player.loans += takeLoanInt;
                player.loanOnThisRound += takeLoanInt;
                takeLoan.text = "";
                OpenWindow(player);
            }
            else
            {
                takeLoan.text = Mathf.Clamp(takeLoanInt, 0, availabeLoanInt) + "";

            }
        }



    }

    public void ReturnLoanButton()
    {
        if (returnLoan.text != null)
        {
            returnLoanInt = int.Parse(returnLoan.text);
            if (returnLoanInt <= player.cash && returnLoanInt<=currentLoan && returnLoanInt>=0)
            {
                player.WalletChange(-returnLoanInt, 0, 0, 0, 0, 0, 0, 0);
                returnLoan.text = "";
                player.loans -= returnLoanInt;
                OpenWindow(player);

            }
            else
            {
                if (returnLoanInt<0)
                {
                    returnLoan.text = "0";
                }
                else if (returnLoanInt > currentLoan)
                {
                    returnLoan.text = currentLoan + "";
                }
                else if (returnLoanInt <= player.cash)
                {
                    returnLoan.text = player.cash + "";
                }
            }

        }
    }

    public void OkButton()
    {
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }

}
