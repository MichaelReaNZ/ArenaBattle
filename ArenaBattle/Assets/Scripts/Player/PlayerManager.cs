using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public Player[] Players => _players;
    private Player[] _players;
    private bool levelLoaded = false;

    private void Awake()
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
        
        _players = FindObjectsOfType<Player>();
    }

    private void Update()
    {
        if (!levelLoaded && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(BeginGame());
            levelLoaded = true;
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

    /*public void LoadLevel()
    {
        StartCoroutine(BeginGame());
    }*/

    private IEnumerator BeginGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
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