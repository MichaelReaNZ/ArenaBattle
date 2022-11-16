using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //When the player collides with the resource, it will be destroyed and the player's resource count will increase
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            Player playa = other.GetComponent<Character>().GetPlayer();
                
            playa.numberOfResourcesCollected += 1;
        }
        
        //Destroy the resource
        Destroy(gameObject);

    }
}
