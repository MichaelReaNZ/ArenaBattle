using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //When the player collides with the block
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            other.GetComponent<Character>().TakeDamage(1000.0f, null);
            
        }
    }
    
}
