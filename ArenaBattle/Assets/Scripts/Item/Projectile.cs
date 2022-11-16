using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PooledObj
{
    public void SetDamage(float damage) => this.damage = damage;
    private float damage = 1f;
    float speed = 5f;
    private Player owner;

    public void SetOwner(Player player) => owner = player;

    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * speed;
    }
private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ITakeDamage>( out var damageTaker))
        {
            damageTaker.TakeDamage(damage, owner);
        }
        
        Debug.Log(collision.gameObject.name);

        owner = null;
        ReturnToPool();
        Debug.Log("object inactive");
    }
}