using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10.0f;
    CharacterController controller;
    Transform cachedTransform;

    private void OnEnable()
    {
        cachedTransform = gameObject.transform;
        controller = GetComponent<CharacterController>();
    }

    public void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("LeftStickHorizontal"),0,Input.GetAxis("LeftStickVertical"));
        Vector3 lookDirection = new Vector3(Input.GetAxis("RightStickVertical"), 0, Input.GetAxis("RightStickHorizontal"));

        if(lookDirection.sqrMagnitude > .8f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection); 
            cachedTransform.rotation = lookRotation;
        }
        controller.SimpleMove(moveDirection * movementSpeed);
    }
}
