using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour, ITakeDamage
{   //sets player attached to character to this character
    public Character(Player player)
    {
        this.player = player;
        if (this.player == null)
        {
            Debug.Log("character player null");
        }
    }
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
   
    //if canfire == true, allows the weapon to fire a projectile
    private bool canFire = true;
    //sets player health
    private float health = 100f;
    
    public float getHealth =>  health;
    //sets players class type
    public ClassType _classType;
    //sets the player controlling that character
    private Player player;
    
    //death count
    public int deaths = 0;

    
    //enums for class
    public enum ClassType
    {
        Brute,//More health
        Speedy, //Moves faster
        Balanced, //
    }

    private void Start()
    {
        
        currentWeapon = defaultWeapon;
        currentWeapon.SetPlayer(player);
    }

    //Takes in Time to perish from weapon, then destroys weapon if time to perish is equal to zero
    private IEnumerator WeaponPerishRoutine()
    {
        
    
        while (currentWeapon != null && currentWeapon != defaultWeapon) 
        {
                yield return new WaitForSeconds(currentWeapon.TimeToPerish);
                Destroy(currentWeapon.gameObject);
                currentWeapon = defaultWeapon;
                currentWeapon.SetPlayer(player);
                betterWeapon = null;
        }
        Debug.Log(currentWeapon.name + ":  Weapon has perished");
       
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
        if (_controller.changeWeaponPressed)
        {
            if (betterWeapon != null)
            {
                Debug.Log("Switching Weapons");
                if (currentWeapon != defaultWeapon)
                {
                    currentWeapon = defaultWeapon;
                }
                else if (currentWeapon != betterWeapon)
                {
                    currentWeapon = betterWeapon;
                }
                currentWeapon.SetPlayer(player);
            }
        }
    }
    
    //get player 
    public Player GetPlayer()
    {
        return player;
    }
    
    //moves the player if movement if pressed, shoots gun if the shoot button is pressed and canFire == true
    private void Update()
    {
        Vector3 dir = _controller.GetMovementDirection();
        Vector3 rotationDir = _controller.GetFacingDirection();
        if (dir.magnitude > 0.25f)
        {
            transform.position += dir * Time.deltaTime * movementSpeed;
            //Debug.Log("Moving Player");
        }
        //rotator.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationDir), _slerpRatio);
        //_slerpRatio += Time.deltaTime;
        
        if (_controller.shootPressed) //&& canFire)
        {
            Debug.Log("Shot Pressed");
            UseWeapon();
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
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = weapon;
        betterWeapon = weapon;

        currentWeapon.SetPlayer(player);
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
            enemy.IncrementKills();
            //Kill character
            deaths = deaths + 1;
            health = 100f;
            Debug.Log(player.PlayerNumber+ ": Player has died");

        }
    }
}

