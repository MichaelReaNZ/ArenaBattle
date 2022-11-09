using System;
using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;

    private void Update()
    {
        if (Input.GetKeyDown("space")){
            shootInput?.Invoke();
        }
    }
}