using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _playerNumber;
    public Controller Controller { get; private set; }
    private PlayerUI _playerUI;
    public bool HasController => Controller != null;
    public int PlayerNumber => _playerNumber;

    private void Awake()
    {
        _playerUI = GetComponent<PlayerUI>();
    }

    public void InitPlayer(Controller controller)
    {
        Controller = controller;
        gameObject.name = $"Player {_playerNumber} - {controller.gameObject.name}";
        _playerUI.HandleInitPlayer();
    }
}