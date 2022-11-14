using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunFactory : BasicFactory<Weapon>
{
    protected override Weapon CreateInstance()
    {
        Weapon newMachineGun = Instantiate(prefab_thisItem);
            return newMachineGun;
   
    }

}

