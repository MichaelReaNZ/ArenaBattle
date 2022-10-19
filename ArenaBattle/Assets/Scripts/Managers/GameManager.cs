using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public static event Action<GameState> OnGameStateChanged; 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.MainMenu:
                break;
            case GameState.GameInProgress:
                break;
            case GameState.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }

        OnGameStateChanged?.Invoke(newGameState);

    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Manage the game
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}

public enum GameState
{
    MainMenu,
    GameInProgress,
    GameOver
}
