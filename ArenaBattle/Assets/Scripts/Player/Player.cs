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

    public void InitPlayer(Controller controller)
    {
        Controller = controller;
        gameObject.name = $"Player {_playerNumber} - {controller.gameObject.name}";
        _playerUI.HandleInitPlayer();
    }

    public void SpawnCharacter()
    {
        var character = Instantiate(Character, new Vector3(0,1,0), Quaternion.identity);
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
}