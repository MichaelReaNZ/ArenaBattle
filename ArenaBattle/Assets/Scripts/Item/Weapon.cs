using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] WeaponData weaponData;

    float timeSinceLastShot;
    public GameObject bullet;

    
    //ref
    public string getName()
    {
        return weaponData.name;
    }
    public Transform attackPoint;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
       
    }

    private bool CanShoot() => timeSinceLastShot > 1f / (weaponData.fireRate / 6f);

    public void Shoot()
    {
        Debug.Log("shot taken");
        

        
        if (weaponData.currentAmmo > 0)
        {
            if (CanShoot())
            {

                //set object to pooled object
                GameObject currentBullet = Pool.Get<Projectile>();
                if (currentBullet != null)
                {	currentBullet.transform.position = attackPoint.position;
					//sets bullet to have no rotation
					currentBullet.transform.rotation = Quaternion.identity;
                    Debug.Log("bullet created");
                }




                weaponData.currentAmmo--;
                timeSinceLastShot = 0;
               
               
                
            }
        }

    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }


}
