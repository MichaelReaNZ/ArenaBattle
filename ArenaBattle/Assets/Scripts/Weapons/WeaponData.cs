using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Projectile Data")]
    public float damage;
    public float maxDist;

    [Header("Gun Data")]
    public int currentAmmo;
    public int magazineSize;
    public float fireRate;

}
