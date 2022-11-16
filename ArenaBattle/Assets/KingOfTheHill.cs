using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if there is only one player inside the hill collider then set them as the king
        
        //get all the objects that have the script "Character"
        Character[] characters = FindObjectsOfType<Character>();
        
        int charactersInHill = 0;
        
        //loop through all the characters
        foreach(Character character in characters)
        {
            //if the character is inside the hill collider
            if(character.transform.position.x > transform.position.x - transform.localScale.x / 2 && character.transform.position.x < transform.position.x + transform.localScale.x / 2 && character.transform.position.z > transform.position.z - transform.localScale.z / 2 && character.transform.position.z < transform.position.z + transform.localScale.z / 2)
            {
                //add one to the number of characters in the hill
                charactersInHill++;
            }
        }
        
        //if there is only one character in the hill
        if(charactersInHill == 1)
        {
            //set that player as king
            foreach(Character character in characters)
            {
                if(character.transform.position.x > transform.position.x - transform.localScale.x / 2 && character.transform.position.x < transform.position.x + transform.localScale.x / 2 && character.transform.position.z > transform.position.z - transform.localScale.z / 2 && character.transform.position.z < transform.position.z + transform.localScale.z / 2)
                {
                    Player player = character.GetPlayer();
                    GameManager.Instance.currentKingOfTheHillPlayer = player;
                    
                    //start timer
                    if (player != null)
                    {
                        player.currentTimeInKingZone = Time.time;
                        player.isKing = true;
                    }
                }
            }
        }

        if (charactersInHill == 0)
        {
            foreach(Character character in characters)
            {
                Player player = character.GetPlayer();
                if (player != null)
                {
                    player.isKing = false;

                    GameManager.Instance.currentKingOfTheHillPlayer = null;

                    //stop timer
                    player.timeAsKing += player.currentTimeInKingZone - Time.time;
                    player.currentTimeInKingZone = 0;
                    player.isKing = false;
                }
            }
        }
    }
    
    //on enter event for the trigger
    void OnTriggerEnter(Collider other)
    {
        //if the object that enters the trigger that contains the name Character

        //if other.gameObject.name contains "Character"
        if (other.gameObject.name.Contains("Character"))
        {
            //get Character script
            Character character = other.gameObject.GetComponent<Character>();
            Player player = character.GetPlayer();

            if (GameManager.Instance.currentKingOfTheHillPlayer == null)
            {
                GameManager.Instance.currentKingOfTheHillPlayer = player;
                //start timer
                player.currentTimeInKingZone = Time.time;
                player.isKing = true;
            }
        }
    }
    
    //on exit event for the trigger
    void OnTriggerExit(Collider other)
    {
        //if the object that enters the trigger that contains the name Character

        //if other.gameObject.name contains "Character"
        if (other.gameObject.name.Contains("Character"))
        {
            //get Character script
            Character character = other.gameObject.GetComponent<Character>();
            Player player = character.GetPlayer();

            if (GameManager.Instance.currentKingOfTheHillPlayer == player)
            {
                GameManager.Instance.currentKingOfTheHillPlayer = null;

                //stop timer
                player.timeAsKing += player.currentTimeInKingZone - Time.time;
                player.currentTimeInKingZone = 0;
                player.isKing = false;
            }
        }
    }
}
