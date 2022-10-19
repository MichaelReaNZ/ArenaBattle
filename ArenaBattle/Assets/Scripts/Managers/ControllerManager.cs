using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    private List<Controller> _controllers;

    private void Awake()
    {
        _controllers = FindObjectsOfType<Controller>().ToList();
        int index = 1;

        foreach (var controller in _controllers)
        {
            controller.SetIndex(index);
            index++;
        }
    }

    private void Update()
    {
        foreach (var controller in _controllers)
        {
            if (!controller.IsAssigned && controller.shoot)
            {
                AssignController(controller);
            }
        }
    }

    private void AssignController(Controller controller)
    {
        controller.IsAssigned = true;
        Debug.Log("Assigned Controller: " +controller.gameObject.name);
        FindObjectOfType<PlayerManager>().AddPlayerToGame(controller);
    }
}
