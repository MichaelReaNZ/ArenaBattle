using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IProjectile
{
    float rateOfFire = 100.0f;
    public GameObject Projectile;
    GameObject projectileInstance;
    IProjectile = thisProjectile;
    bool canFire = false;
    float damage = 100.0f;
    float flightTime = 100.0f;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        if (canFire)
        {
            if (Input.GetKeyDown("UpArrow"))
            {
                canFire = false;
                Vector3.forward forward;
                projectileInstance.GetComponent<Projectile>().Init(forward, flightTime, damage);

            }
        }

        if (rateOfFire <= 0)
        {
            if (canFire = false)
            {
                canFire = true;
                projectileInstance = Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
                Projectile = projectileInstance.GetComponent<Projectile>();
                rateOfFire = 100.0f;
            }
        }
        else
        {
            rateOfFire -= 1.0f;
        }
    }


    
}
