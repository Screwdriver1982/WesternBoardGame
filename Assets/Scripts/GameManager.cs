using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
    #endregion

    
    [Header("Игроки")]
    [SerializeField] PlayerMovement[] players;
    [SerializeField] int activePlayerNum;
    GameStates activeGMState;


    enum GameStates
    { 
        WELCOME_WINDOW, //показываем приветственное окно
        PLAYER_TURN_ALARM, //сообщаем, что ходит определенный игрок
        START_DICE_THROW_WAITING, //ожидаем, когда игрок кинет кубик, для начальной сортировки игроков
        TURN_DICE_THROW_WAITING, // ожидаем, когда игрок кинет кубик, чтобы понять сколько ему нужно походить
        TURN_CAP_MOVING_WAITING, // ожидаем, когда игрок походит фишкой
        FIRM_BUY_WAITING, //ожидаем когда игрок решит покупает он или нет акцию
        NEW_CIRCLE_WINDOW //окно начала нового круга

    
    }





    private void Start()
    {
        activeGMState = GameStates.WELCOME_WINDOW;
        activePlayerNum = 0;
        players[activePlayerNum].GiveTheTurnToPlayer();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            players[activePlayerNum].TakeTurnFromPlayer();

            if (activePlayerNum != (players.Length - 1))
            {
                activePlayerNum += 1;
            }
            else 
            {
                activePlayerNum = 0;
            }
            players[activePlayerNum].GiveTheTurnToPlayer();

        }
    }


    void changeGMState(GameStates newState)
    { 
    
    }

    void stateUpdate()
    { 
    
    }

}
