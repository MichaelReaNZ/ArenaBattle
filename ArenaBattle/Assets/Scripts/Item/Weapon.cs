using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] WeaponData weaponData;
    
    //time until the weapon is destroyed, starts counting down on pickup
    public float TimeToPerish => weaponData.timeToPerish;
    //time since last shot was taken, regulates how often a bullet can be sot
    float timeSinceLastShot;
    public PooledObj bullet;
    private Player player;

    //sets weapon user as this weapons owner
    public void SetPlayer(Player playerObj) => player = playerObj;
   
    //returns the weapons name
    public string getName()
    {
        return weaponData.name;
    }

    public GameObject attackPoint;

    
    private void Start()
    {
        
        PlayerShoot.shootInput += Shoot;
       
    }
    //returns true if time since last shot is less than fire rate
    private bool CanShoot() => timeSinceLastShot > 1f / (weaponData.fireRate / 6f);
    //shoots a projectile from the gun
    public void Shoot()
    {
        Debug.Log("shot taken");
        if (weaponData.currentAmmo > 0)
        {
            //if (CanShoot())
            //{
               
                //set object to pooled object

                if (player == null)
                {
                    Debug.Log("player null");
                }
                var currentBullet = bullet.Get<Projectile>(false);
                currentBullet.transform.position = transform.position + Vector3.forward;
                currentBullet.gameObject.SetActive(true);
                //currentBullet.GetComponent<Rigidbody>().isKinematic = true;
                currentBullet.SetOwner(player);
                currentBullet.SetDamage(player.GetDamageMultiplier() * weaponData.damage);

                if (currentBullet != null)
                {	
					//sets bullet to have no rotation
					//c//urrentBullet.transform.rotation = Quaternion.identity;
                    //currentBullet.gameObject.SetActive(true);
                    //currentBullet.GetComponent<Rigidbody>().isKinematic = false;
                    Debug.Log("bullet created");
                }else{Debug.Log("bullet not created");}
                //reduces ammo after shot taken
                weaponData.currentAmmo--;
                timeSinceLastShot = 0;

            }
        else
        {
            Debug.Log("out of ammo");

    

        }

    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }


}
