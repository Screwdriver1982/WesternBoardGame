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

    private void Start()
    {
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

}
