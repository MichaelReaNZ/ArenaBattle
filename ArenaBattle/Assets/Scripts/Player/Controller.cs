using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int Index { get; private set; }
    public bool IsAssigned { get; set; }
    public bool shoot;
    public bool shootPressed;
    
    
    public bool changeWeaponPressed;   
    public float horizontal;
    public float vertical;
    public float rHorizontal;
    public float rVertical;
    
    private string _switchButton;
    private string _shootButton;
    private string _horizontalAxis;
    private string _verticalAxis;
    private string _rotationHorizontal;
    private string _rotationVertical;
    

    public bool AnyButtonDown()
    {
        return shoot;
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
        Index = 0;
        IsAssigned = false;
    }

    private void Update()
    {
        
        //switches weapon
        if (!string.IsNullOrEmpty(_switchButton))
        {
            changeWeaponPressed = Input.GetButtonDown(_switchButton);
        }
        
        if (!string.IsNullOrEmpty(_shootButton))
        {
            shoot = Input.GetButton(_shootButton);
            shootPressed = Input.GetButtonDown(_shootButton);
        }

        if (!string.IsNullOrEmpty(_horizontalAxis))
        {
            horizontal = Input.GetAxis(_horizontalAxis);
        }

        if (!string.IsNullOrEmpty(_verticalAxis))
        {
            vertical = Input.GetAxis(_verticalAxis);
        }

        if (!string.IsNullOrEmpty(_rotationHorizontal))
        {
            rHorizontal = Input.GetAxis(_rotationHorizontal);
        }
        
        if (!string.IsNullOrEmpty(_rotationVertical))
        {
            rVertical = Input.GetAxis(_rotationVertical);
        }
    }

    public void SetIndex(int index)
    {
        Index = index;
        _shootButton = "Shoot" + Index;
        _horizontalAxis = "Horizontal" + Index;
        _verticalAxis = "Vertical" + Index;
        _rotationHorizontal = "rHorizontal" + Index;
        _rotationVertical = "rVertical" + Index;
        gameObject.name = "Controller" + Index;
        _switchButton = "Switch" + Index;
    }
    
    

    public Vector3 GetMovementDirection()
    {
         return new Vector3(horizontal, 0,-vertical);
    }

    public Vector3 GetFacingDirection()
    {
        return new Vector3(rHorizontal/5, 0, -rVertical/5);
    }
}