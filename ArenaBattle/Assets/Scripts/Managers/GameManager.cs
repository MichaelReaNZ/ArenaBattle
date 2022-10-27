using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private string levelToLoad;
    
    public Player[] Players => _players;
    private Player[] _players;
   // private bool levelLoaded = false;

    public GameState currentGameState { get; private set; }

    //keep track of how long the game has been running
    private TimeSpan _timePlaying;
    private bool _isTimerRunning;
    private float _elapsedTime;

    [SerializeField] private int shrinkAfterSeconds;
    
    public enum GameState
    {
        MainMenu,
        GameInProgress,
        ShrinkingArena,
        [Description ("GameOver")] GameOver
    }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        _players = FindObjectsOfType<Player>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeGameState(GameState.MainMenu);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.MainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(BeginGame());
                ChangeGameState(GameState.GameInProgress);
                //levelLoaded = true;
            }
            
        }
    }

   // public static event Action<GameState> OnGameStateChanged; 
    
    


    public void ChangeGameState(GameState newGameState)
    {
        currentGameState = newGameState;
     
        switch (newGameState)
        {
            case GameState.MainMenu:
                break;
            case GameState.GameInProgress:
            {
                //BeginGame();
                //StartCoroutine(BeginGame());
                StartTimer();
            }
               
                break;
            case GameState.GameOver:
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                Debug.Log("End Game State");
            }
                break;
            case GameState.ShrinkingArena:{}
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }

        //OnGameStateChanged?.Invoke(newGameState);
    }
    
    public void AddPlayerToGame(Controller controller)
    {
        var firstAvailable = _players.OrderBy(t => t.PlayerNumber).
            FirstOrDefault(t => t.HasController == false);
        
        if (firstAvailable != null)
        {
            firstAvailable.InitPlayer(controller);
        }
    }
    
    public void StartTimer()
    {
        _isTimerRunning = true;
        _elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }
    
    private IEnumerator UpdateTimer()
    {
        while (_isTimerRunning)
        {
            _elapsedTime += Time.deltaTime;
            _timePlaying = TimeSpan.FromSeconds(_elapsedTime);
            string timePlayingStr = "Time: " + _timePlaying.ToString("mm':'ss'.'ff");
            Debug.Log(timePlayingStr);
            
            if (_timePlaying.Seconds >= shrinkAfterSeconds)
            {
                ChangeGameState(GameState.ShrinkingArena);
                _isTimerRunning = false;
            }
            
            yield return null;
        }
    }

    private IEnumerator BeginGame()
    {
        ChangeGameState(GameState.GameInProgress);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Single);
        while (operation.isDone == false)
        {
            yield return null;
        }
        
        Debug.Log("Level Loaded");
        FindObjectOfType<LevelController>().SpawnPlayers();
        foreach (var player in _players)
        {
            player.InitInGameUI();
        }
    }
    
}


