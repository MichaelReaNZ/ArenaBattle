using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BasicFactory<Type> : MonoBehaviour where Type : MonoBehaviour
{	//creates prefab
    [SerializeField]
    protected Type prefab_thisItem;
    
    //abstract function, overloaded in the factory
    protected abstract Type CreateInstance();

    public Type getInstance()
    {
        return this.CreateInstance();
    }
}