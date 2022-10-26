using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] WeaponData weaponData;

    float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
    }

    private bool CanShoot() => timeSinceLastShot > 1f / (weaponData.fireRate / 6f);

    public void Shoot()
    {
        Debug.Log("shot");
        if (weaponData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, weaponData.maxDist))
                {
                    Debug.Log("hit Something");
                }
                weaponData.currentAmmo--;
                timeSinceLastShot = 0;
                OnWeaponShot();
            }
        }

    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

   private void OnWeaponShot()
    {
       // throw new NotImplementedException();
    }
}
