using System.Collections;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private float spawnCheckInterval;
	//creates weapon
    private Weapon currentWeapon;
    
    [SerializeField] private WeaponData[] potentialWeapons;
    private bool spawnItems = false;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
    //begins spawning items when weaponPickup is started
    private void Start()
    {
        spawnItems = true;
        StartCoroutine(SpawnItemRoutine());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            other.GetComponent<Character>().SetWeapon(currentWeapon);
            //currentWeapon = null;
        }
    }
	//Adds factories to Spawn
    
    [SerializeField] private MachinegunFactory m_factory;
    [SerializeField] private DesertEagleFactory de_factory;
    [SerializeField] private RifleFactory r_factory;
    private IEnumerator SpawnItemRoutine()
    {
        while (spawnItems)
        {
            yield return new WaitForSeconds(spawnCheckInterval);
            if (currentWeapon == null)
            {
                //int rand = Random.Range(0, potentialWeapons.Length - 1);
                int rand = Random.Range(0, 3);
                //spawns weapons randomly
                switch (rand)
                {//creates weapon based on rand number
                    case 0:
							Debug.Log("Assigned Machine Gun");
                        currentWeapon = m_factory.getInstance();
                        break;
                    case 1:
Debug.Log("Assigned Desert Eagle");
                        currentWeapon = de_factory.getInstance();
                        break;
                    case 2:
Debug.Log("Assigned Rifle");
                        currentWeapon = r_factory.getInstance();
                        break;
                    default: 
                        Debug.Log("default case");
                        currentWeapon = currentWeapon;
                        break;
                }
                Debug.Log(currentWeapon.getName());
                
              
            }
        }
    }
}