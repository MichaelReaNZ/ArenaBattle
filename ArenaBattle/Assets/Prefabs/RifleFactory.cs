using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFactory : BasicFactory<Weapon>
{

  
    protected override Weapon CreateInstance()
    {
        Weapon newRifle = Instantiate(prefab_thisItem);
        return newRifle;
    }

}
