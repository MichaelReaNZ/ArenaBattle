using System.Collections;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private float spawnCheckInterval;
    private WeaponData currentWeapon;
    
    [SerializeField] private WeaponData[] potentialWeapons;
    private bool spawnItems = false;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        spawnItems = true;
        StartCoroutine(SpawnItemRoutine());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>())
        {
            //other.GetComponent<Character>().SetWeapon(currentWeapon);
            currentWeapon = null;
        }
    }

    private IEnumerator SpawnItemRoutine()
    {
        while (spawnItems)
        {
            yield return new WaitForSeconds(spawnCheckInterval);
            if (currentWeapon == null)
            {
                int rand = Random.Range(0, potentialWeapons.Length - 1);
                currentWeapon = potentialWeapons[rand];
            }
        }
    }
}