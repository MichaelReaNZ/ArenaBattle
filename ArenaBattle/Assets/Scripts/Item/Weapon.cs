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
    public Camera PlayerSights;
    public Transform attackPoint;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
       
    }

    private bool CanShoot() => timeSinceLastShot > 1f / (weaponData.fireRate / 6f);

    public void Shoot()
    {
        Debug.Log("shot taken");
        Vector3 targetPoint;

        Ray ray = PlayerSights.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (weaponData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.transform.name);
                    targetPoint = hit.point;
                }
                else
                {
                    //fix this later
                    targetPoint = ray.GetPoint(75);
                }

                Vector3 direction = targetPoint - attackPoint.position;
               

                GameObject currentBullet = Instantiate(bullet,
                    attackPoint.position,
                    Quaternion.identity);

                currentBullet.transform.forward = direction.normalized;



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
