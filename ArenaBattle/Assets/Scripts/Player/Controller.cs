using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int Index { get; private set; }
    public bool IsAssigned { get; set; }
    public bool shoot;
    public bool shootPressed;
    public float horizontal;
    public float vertical;
    
    private string _shootButton;
    private string _horizontalAxis;
    private string _verticalAxis;
    

    public bool AnyButtonDown()
    {
        return shoot;
    }

    private void Update()
    {
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
    }

    public void SetIndex(int index)
    {
        Index = index;
        _shootButton = "Shoot" + Index;
        _horizontalAxis = "Horizontal" + Index;
        _verticalAxis = "Vertical" + Index;
        gameObject.name = "Controller" + Index;
    }
}