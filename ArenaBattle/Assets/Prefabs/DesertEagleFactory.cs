using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEagleFactory : BasicFactory<Weapon>
{

  
    protected override Weapon CreateInstance()
    {
        Weapon newDesertEagle = Instantiate(prefab_thisItem);
        return newDesertEagle;
    }

}