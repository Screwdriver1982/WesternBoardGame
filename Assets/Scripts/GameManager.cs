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
    [SerializeField] PlayerMovement[] playerMovements;
    [SerializeField] int activePlayerNum;

    [Header("Работа с UI")]
    [SerializeField] UIManager UIManager;

    [Header("Состояния игры")]
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
        playerMovements[activePlayerNum].GiveTheTurnToPlayer();
        SetPlayerInUI();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovements[activePlayerNum].TakeTurnFromPlayer();

            activePlayerNum = (activePlayerNum + 1) % 4;
            playerMovements[activePlayerNum].GiveTheTurnToPlayer();
            SetPlayerInUI();

        }
    }


    void changeGMState(GameStates newState)
    {

    }

    void stateUpdate()
    {

    }

    void SetPlayerInUI()
    {
        Player activePlayer = playerMovements[activePlayerNum].GetComponent<Player>();
        Player secondPlayer = playerMovements[(activePlayerNum+1) % 4].GetComponent<Player>();
        Player thirdPlayer = playerMovements[(activePlayerNum + 2) % 4].GetComponent<Player>();
        Player fourthPlayer = playerMovements[(activePlayerNum + 3) % 4].GetComponent<Player>();

        if (activePlayer != null && secondPlayer != null && thirdPlayer != null && fourthPlayer != null)
        {

            UIManager.SetPlayersUI(activePlayer, secondPlayer, thirdPlayer, fourthPlayer);
        }


    }

}
