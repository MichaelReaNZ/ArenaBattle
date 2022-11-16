using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour, ITakeDamage
{   
    //initialises movement speed and weapon point
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Transform weaponPoint;
    private float _slerpRatio = 0.0f;
    [SerializeField] private Transform rotator;
    private Controller _controller;
    //current weapon is weapon being used, default weapon is the weapon the user starts with 
    [SerializeField] private Weapon defaultWeapon;
    private Weapon currentWeapon;
    
    //holds any weapon that isnt the default weapon
    private Weapon betterWeapon = null;
   
    //if canfire == true, allows the player to fire a projectile
    private bool canFire = true;
    //sets player health
    private float health = 100f;
    public void SetPlayer(Player player) => this.player = player;
    public float getHealth =>  health;
    //sets players class type
    public ClassType _classType;
    //sets the player controlling that character
    private Player player;
    
    //death count
    public int deaths = 0;

    public Player GetPlayer()
    {
        return player;
    }
    
    //enums for class
    public enum ClassType
    {
        Brute,//More health
        Speedy, //Moves faster
        Balanced, //
    }

    private void Start()
    {
        if (player == null)
        {
            Debug.Log("null Player in character");
        }
        currentWeapon = defaultWeapon;
        currentWeapon.SetPlayer(player);
        currentWeapon.transform.position = weaponPoint.position;
    }

    //Takes in Time to perish from weapon, then destroys weapon if time to perish is equal to zero
    private IEnumerator WeaponPerishRoutine()
    {
        yield return new WaitForSeconds(betterWeapon.TimeToPerish);
        if (betterWeapon != null)
        {
            Destroy(betterWeapon.gameObject);
            currentWeapon = defaultWeapon;
            currentWeapon.SetPlayer(player);
            betterWeapon = null;
            Debug.Log(currentWeapon.name + ":  Weapon has perished");
        }
    }
    //sets which controller the character is using
    public void SetController(Controller controller)
    {
        Debug.Log("Setting Controller");
        _controller = controller;
    }
    
//changes between weapons 
    public void SwitchWeapon()
    {
        if (currentWeapon != defaultWeapon)
        {
            currentWeapon = defaultWeapon;
        }
        else if (currentWeapon == defaultWeapon && betterWeapon != null)
        {
            currentWeapon = betterWeapon;
        }
        Debug.Log("Switching Weapons to " + currentWeapon.getName());
    
    //get player 
    }
    
    //moves the player if movement if pressed, shoots gun if the shoot button is pressed and canFire == true
    //bool to represent if player has moved
    bool playerMoved = false;
    private void Update()
    {
       
        Vector3 dir = _controller.GetMovementDirection();
        Vector3 rotationDir = _controller.GetFacingDirection();
        if (dir.magnitude > 0.25f)
        {//if player hasnt moved, outputs message, then sets playerMoved to true
            if (playerMoved == false)
            {
                Debug.Log("Player Moved");
                playerMoved = true;
            }
            transform.position += dir * Time.deltaTime * movementSpeed;
        }

        if (_controller.shootPressed)
        {
            Debug.Log("Shoot Pressed");
            UseWeapon();
        }

        if (_controller.changeWeaponPressed)
        {
            Debug.Log("Weapon switch pressed");
            SwitchWeapon();
        }
    }
    
    //sets the players class
    public void SetClass(ClassType classType)
    {
        Debug.Log("Setting Class");
        _classType = classType;
        switch (classType)
        {
            case ClassType.Brute:
                movementSpeed = 3f;
                //change color of character to blue
                transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
                health = 150f;
                break;
            case ClassType.Speedy:
                movementSpeed = 7f;
                health = 50f;
                transform.GetChild(0).GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case ClassType.Balanced:
                health = 100f;
                movementSpeed = 5f;
                transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
                break;
        }
    }
    //sets the player's weapon
    public void SetWeapon(Weapon weapon)
    {
        Debug.Log("Setting Weapon");
        if (betterWeapon != null && currentWeapon != defaultWeapon)
        {
            Destroy(betterWeapon.gameObject);
            betterWeapon = null;
        }

        currentWeapon = weapon;
        betterWeapon = weapon;
        weapon.SetPlayer(player);
        //currentWeapon.SetPlayer(player);
        currentWeapon.transform.position = weaponPoint.position;
        StartCoroutine(WeaponPerishRoutine());
    }
//calls the shoot commands from weapon
    private void UseWeapon()
    {   Debug.Log("Using Weapon");
        currentWeapon.Shoot();
    }
    //reduces character health when character is hit
    public void TakeDamage(float amount, Player enemy)
    {
        health -= amount;
        Debug.Log("Character's Health is: " + health);
        if (health <= 0)
        {
            if(enemy != null)
            {
                enemy.IncrementKills();
            }
            //Kill character
            deaths = deaths + 1;
            health = 100f;
            Debug.Log(player.PlayerNumber+ ": Player has died");

        }
    }
}

