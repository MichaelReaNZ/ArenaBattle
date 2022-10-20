using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    
    //keep track of how long the game has been running
    private TimeSpan timePlaying;
    private bool isTimerRunning;
    private float elapsedTime;
    
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
            case GameState.ShrinkingArena:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }

        OnGameStateChanged?.Invoke(newGameState);
    }
    
    public void StartTimer()
    {
        isTimerRunning = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer() as string);
    }
    
    private IEnumerable UpdateTimer()
    {
        while (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            Debug.Log(timePlayingStr);
            yield return null;
        }
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
    ShrinkingArena,
    GameOver
}
