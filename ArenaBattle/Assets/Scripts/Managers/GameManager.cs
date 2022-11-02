using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState currentGameState { get; private set; }
    [SerializeField] private string levelToLoad;
    
    public Player[] Players => _players;
    private Player[] _players;
    
    //keep track of how long the game has been running
    private TimeSpan _timePlaying;
    private bool _isTimerRunning;
    private float _elapsedTime;

    [SerializeField] public int shrinkAfterSeconds;
    
    public enum GameState
    {
        MainMenu,
        GameInProgress,
        ShrinkingArena,
        GameOver
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
                ChangeGameState(GameState.GameInProgress);
            }
        }
        if (currentGameState == GameState.GameOver)
        { 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeGameState(GameState.MainMenu);
            }
        }
        
        //Quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ChangeGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        switch (newGameState)
        {
            case GameState.MainMenu:
            {
                _players = FindObjectsOfType<Player>();
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
            }
            case GameState.GameInProgress:
                StartCoroutine(BeginGame());
                break;
            case GameState.GameOver:
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                break;
            case GameState.ShrinkingArena:
            {
            }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }
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

        StartCoroutine(UpdateGamePlayingTimer());
    }
    
    private IEnumerator UpdateGamePlayingTimer()
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
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Single);
        while (operation.isDone == false)
            yield return null;

        Debug.Log("Level Loaded");
        FindObjectOfType<LevelController>().SpawnPlayers();
        foreach (var player in _players) player.InitInGameUI();

        StartTimer();
    }
    
}


