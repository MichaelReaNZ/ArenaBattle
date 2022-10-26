using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private float _slerpRatio = 0.0f;
    [SerializeField] private Transform rotator;
    private Controller _controller;
    public void SetController(Controller controller)
    {
        _controller = controller;
    }

    private void Update()
    {
        Vector3 dir = _controller.GetMovementDirection();
        //Vector3 rotationDir = _controller.GetFacingDirection();
        if (dir.magnitude > 0.25f)
        {
            transform.position += dir * Time.deltaTime * movementSpeed;
        }
        //rotator.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationDir), _slerpRatio);
        //_slerpRatio += Time.deltaTime;
        /*if (rotationDir.magnitude > 0.1f)
        {
             //Quaternion.LookRotation(rotationDir);
            //rotator.forward = rotationDir * 360f;
        }*/
    }
}

