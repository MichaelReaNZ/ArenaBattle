using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MachineGunData : ScriptableObject
    {
        [Header("Info")]
        public new string name = "Machine Gun";

        [Header("Projectile Data")]
        public float damage = 20;
        

        [Header("Gun Data")]
        public int currentAmmo = 200;
        public int magazineSize = 200;
        public float fireRate = 10;
   
        public int timeToPerish = 500;
    }

