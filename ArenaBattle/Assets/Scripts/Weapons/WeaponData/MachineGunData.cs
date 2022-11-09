using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MachineGunData : ScriptableObject
    {
        [Header("Info")]
        public new string name = "Machine Gun";

        [Header("Projectile Data")]
        public float damage = 20;
        public float maxDist = 50;

        [Header("Gun Data")]
        public int currentAmmo = 200;
        public int magazineSize = 200;
        public float fireRate = 10;
   

    }

