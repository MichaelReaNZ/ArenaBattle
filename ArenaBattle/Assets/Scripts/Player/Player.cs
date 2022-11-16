using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private int _playerNumber;
    public Controller Controller { get; private set; }
    public Character character;

    private PlayerUI _playerUI;
    
    public bool HasController => Controller != null;
    public int PlayerNumber => _playerNumber;
    
    public int numberOfKills = 0;
    public float timeAsKing = 0;
    //timer to track time as kind
    public float currentTimeInKingZone = 0;
    public bool isKing = false;
    public int numberOfResourcesCollected = 0;

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
	//returns damage muliplier variable
    public int GetDamageMultiplier()
    {
        //switch based on class
        return character._classType switch
        {
            Character.ClassType.Balanced => 1,
            Character.ClassType.Brute => 2,
            Character.ClassType.Speedy => 3,
            _ => 1
        };
    }
	//spawns character at a position
    public void SpawnCharacter(Vector3 pos)
    {Debug.Log("Creating Character");
        var character = Instantiate(this.character, pos, Quaternion.identity);
        
        character.SetController(Controller);
        _playerUI.DisableInitText();
        
        //get the highest enum value of ClassType
        int maxClassType = (int)(Character.ClassType) Enum.GetValues(typeof(Character.ClassType)).Cast<Character.ClassType>().Max();
        //get a random class type between 0 and the highest enum value
        var randomClassType = (Character.ClassType) Random.Range(0, maxClassType);
        character.SetClass(randomClassType);
    }
	//outputs game UI
    public void InitInGameUI()
    {
        _playerUI.DisableInitText();
        if (HasController && character!= null)
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

    public void IncrementKills() => numberOfKills++;
}