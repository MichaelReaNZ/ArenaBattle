using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 5;

void update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Debug.Log("object destroyed");
    }
}
