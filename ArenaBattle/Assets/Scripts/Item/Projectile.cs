using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    float flightTime = 0.0f;
    float damage = 0.0f;
    Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

    public float getVelocity
    {
        get
        {
            return velocity.magnitude;
        }
    }

    public float getDamage
    {
        get
        {
            return damage;
        }
    }

    public float getFlightTime
    {
        get
        {
            return flightTime;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
/*    //Something about interacting with objects
    void OnCollisionEnter(Collision collision)
    {
        if (collision.GetComponent<Player>)
        {
            Player.TakeDamage(damage);
        }
    }*/
    //something about destroying itself when hitting objects

    public void Initialise(Vector3 _velocity, float _flightTime, float _damage)
    {
        velocity = _velocity;
        flightTime = _flightTime;
        damage = _damage;
    }
}
