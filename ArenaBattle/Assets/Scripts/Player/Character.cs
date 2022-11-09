using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Transform weaponPoint;
    private float _slerpRatio = 0.0f;
    [SerializeField] private Transform rotator;
    private Controller _controller;
    private Weapon currentWeapon;
    private bool canFire = true;

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

    public void SetWeapon(Weapon weapon)
    {

        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = weapon;
    }

    private void UseWeapon()
    {
        currentWeapon.Shoot();
    }
    
}

