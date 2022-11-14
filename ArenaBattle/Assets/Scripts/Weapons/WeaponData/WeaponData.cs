using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public new string name = "Default Gun";

    [Header("Projectile Data")]
    public float damage = 10;

    [Header("Gun Data")]
    public int currentAmmo = 999;
    public int magazineSize = 999;
    public float fireRate = 5;

    public int timeToPerish = -1;


}
