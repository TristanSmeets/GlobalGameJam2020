﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10.0f;
    CharacterController controller;
    Transform cachedTransform;

    public event Action FiringWeapon = delegate { };
    public event Action<Weapon.WeaponType> SwitchingWeapon = delegate { };

    private void OnEnable()
    {
        cachedTransform = gameObject.transform;
        controller = GetComponent<CharacterController>();
    }

    public void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("LeftStickHorizontal"), 0, Input.GetAxis("LeftStickVertical"));
        Vector3 lookDirection = new Vector3(Input.GetAxis("RightStickHorizontal"), 0, Input.GetAxis("RightStickVertical"));

        if (lookDirection.sqrMagnitude > .8f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            cachedTransform.rotation = lookRotation;
            FiringWeapon();
        }
        controller.SimpleMove(moveDirection * movementSpeed);
    }

    private void Update()
    {
        CheckWeaponButtons();
    }

    private void CheckWeaponButtons()
    {
        //TODO: Make sure these only fire onces.
        if (Input.GetAxis("Fire1") > 0)
        {
            SwitchingWeapon(Weapon.WeaponType.SHOTGUN);
        }

        if (Input.GetAxis("Fire2") > 0)
        {
            SwitchingWeapon(Weapon.WeaponType.ASSAULT_RIFLE);
        }

        if (Input.GetAxis("Fire3") > 0)
        {
            SwitchingWeapon(Weapon.WeaponType.SNIPER);
        }
    }
}