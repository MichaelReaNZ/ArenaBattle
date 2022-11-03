using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _playerNumber;
    public Controller Controller { get; private set; }
    public Character Character;

    private PlayerUI _playerUI;
    
    public bool HasController => Controller != null;
    public int PlayerNumber => _playerNumber;

    private void Awake()
    {
        _playerUI = GetComponent<PlayerUI>();
    }

    private void Start()
    {
        GameManager.OnGameOver += GameManager_OnGameOver;
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver()
    {
        ResetPlayer();
    }

    public void InitPlayer(Controller controller)
    {
        Controller = controller;
        gameObject.name = $"Player {_playerNumber} - {controller.gameObject.name}";
        _playerUI.HandleInitPlayer();
    }

    public void SpawnCharacter(Vector3 pos)
    {
        var character = Instantiate(Character, pos, Quaternion.identity);
        character.SetController(Controller);
        _playerUI.DisableInitText();
    }

    public void InitInGameUI()
    {
        _playerUI.DisableInitText();
        if (HasController && Character!= null)
        {
            //ShowGameUI - Health bar etc.
        }
    }

    private void ResetPlayer()
    {
        Controller = null;
        _playerNumber = 0;
        _playerUI.ResetUI();
    }
}