using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Счет игрока")]
    public int cash;
    public int gold;
    public int oil;
    public int cars;
    public int cola;
    public int drugs;
    public int brains;

    [Header("Финансы")]
    public int capital;
    public int robberiedMoney;
    public int colonyMoney;
    public int startingLoan;
    public int colonyLoan;

    [Header("Карточки игрока")]
    public bool policeCard;
    public bool bossCard;
    public bool armyCard;
    public bool woolfyCard;
    public bool rabbyCard;
    public bool taxFreeCard;
    public bool badHarvestCard;
    public bool mediaCrisisCard;

    [Header("Кредиты игрока")]
    public List<int> loans;

    [Header("Депозиты игрока")]
    public List<int> deposites;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
