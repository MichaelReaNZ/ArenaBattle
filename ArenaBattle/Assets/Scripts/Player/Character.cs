using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour, ITakeDamage
{
    public Character(Player player)
    {
        this.player = player;
    }
    
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Transform weaponPoint;
    private float _slerpRatio = 0.0f;
    [SerializeField] private Transform rotator;
    private Controller _controller;
    private Weapon defaultWeapon;
    private Weapon currentWeapon;
    private bool canFire = true;
    private float health = 100f;
    public ClassType _classType;
    private Player player;

    
    //enums for class
    public enum ClassType
    {
        Brute,//More health
        Speedy, //Moves faster
        Balanced, //
    }

    private IEnumerator WeaponPerishRoutine()
    {
        bool hasPerished = false;
    
        while (currentWeapon != null && currentWeapon != defaultWeapon) 
        {
                yield return new WaitForSeconds(currentWeapon.TimeToPerish);
                Destroy(currentWeapon.gameObject);
                currentWeapon = defaultWeapon;
        }
    }
    
    public void SetController(Controller controller)
    {
        _controller = controller;
    }
    

    private void Update()
    {
        Vector3 dir = _controller.GetMovementDirection();
        Vector3 rotationDir = _controller.GetFacingDirection();
        if (dir.magnitude > 0.25f)
        {
            transform.position += dir * Time.deltaTime * movementSpeed;
        }
        rotator.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationDir), _slerpRatio);
        _slerpRatio += Time.deltaTime;
        
        if (_controller.shootPressed && canFire)
        {
            UseWeapon();
        }
    }
    
    //set class
    public void SetClass(ClassType classType)
    {
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

    public void SetWeapon(Weapon weapon)
    {

        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = weapon;

        currentWeapon.SetPlayer(player);
        StartCoroutine(WeaponPerishRoutine());
    }

    private void UseWeapon()
    {
        currentWeapon.Shoot();
    }

    public void TakeDamage(float amount, Player player)
    {
        health -= amount;
        if (health <= 0)
        {
            player.IncrementKills();
            //Kill self
        }
    }
}

