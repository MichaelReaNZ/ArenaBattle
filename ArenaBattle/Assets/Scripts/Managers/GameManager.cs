using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

public enum GameModeEnum
{
    KingOfTheHill,
    MostKills,
    MostResources,
}
public class GameManager : MonoBehaviour
{
    private bool modeSelected = false;
    public static GameManager Instance { get; private set; }
    public static event Action OnGameOver;
    public GameState currentGameState { get; private set; }
    public GameModeEnum GameMode { get; private set; }
    [SerializeField] private string levelToLoad;
    [SerializeField] private GameObject message;
    
    public Player[] Players => _players;
    private Player[] _players;
    
    //keep track of how long the game has been running
    private TimeSpan _timePlaying;
    private bool _isTimerRunning;
    private float _elapsedTime;
    
    public Player winningPlayer;
    public Player currentKingOfTheHillPlayer;

    [SerializeField] private GameObject modeSelectMenu;
    [SerializeField] public int shrinkAfterSeconds;
    private bool modeSelectMenuShowing = false;

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
        } 
        else 
        {
            Destroy(gameObject);
        }

        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        _players = FindObjectsOfType<Player>();
        ModeSelectMenu.OnSelectMode+= ModeSelect_OnSelectMode;
    }

    private void ModeSelect_OnSelectMode(GameModeEnum obj)
    {
        GameMode = obj;
        modeSelected = true;
        modeSelectMenu.SetActive(false);
        message.SetActive(true);
        Debug.Log("Current game mode set to: " + GameMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.MainMenu)
        {
            if (_players.Any(x=>x.HasController) && !modeSelectMenuShowing)
            {
                modeSelectMenu.SetActive(true);
                modeSelectMenuShowing = true;
            }
            
            if (!modeSelected)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_players.Any(x=>x.HasController) && modeSelected)
                {
                    Debug.Log("Starting game");
                    message.SetActive(false);
                    ChangeGameState(GameState.GameInProgress);
                }
            }
            
            
        }
        if (currentGameState == GameState.GameOver)
        { 
            //load UI to show winner
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowMainMenu();
            }
        }
        
        //Quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void ShowMainMenu()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(index);
        OnGameOver?.Invoke();
        ChangeGameState(GameState.MainMenu);
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
                ModeSelectMenu.OnSelectMode+= ModeSelect_OnSelectMode;
                break;
            }
            case GameState.GameInProgress:
                //TODO: Set a way to change game mode
                ModeSelectMenu.OnSelectMode-= ModeSelect_OnSelectMode;
                StartCoroutine(BeginGame());
                break;
            case GameState.GameOver:
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                winningPlayer = GetWinningPlayer();
               
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
    
    public void ChangeClass(Controller controller, Character.ClassType classType)
    {
        //get the player from _players that has the controller
        var player = _players.FirstOrDefault(x => x.Controller == controller);
        if (player != null)
        {
            player.character.SetClass(classType);
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
//            Debug.Log(timePlayingStr);
            
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
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);
        while (operation.isDone == false)
            yield return null;

        Debug.Log("Level Loaded");
        FindObjectOfType<LevelController>().SpawnPlayers();
        foreach (var player in _players) player.InitInGameUI();

        StartTimer();
    }
    
    //get winning player
    private Player GetWinningPlayer()
    {
        Player winningPlayer = null;
        switch (GameMode)
        {
            case GameModeEnum.KingOfTheHill:
                //find player with highest score
                winningPlayer = _players.OrderByDescending(x => x.timeAsKing).FirstOrDefault();
                break;
            case GameModeEnum.MostKills:
                //find player with highest kills
                winningPlayer = _players.OrderByDescending(x => x.numberOfKills).FirstOrDefault();
                break;
            case GameModeEnum.MostResources:
                //find player with highest resources
                winningPlayer = _players.OrderByDescending(x => x.numberOfResourcesCollected).FirstOrDefault();
                break;
        }

        return winningPlayer;
    }
    
}


