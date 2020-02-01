﻿using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    Transform cachedTransform;
    Rigidbody rb;
    Weapon.WeaponSpecifics weaponSpecifics;

    private void OnEnable()
    {
        cachedTransform = transform;
        rb = GetComponent<Rigidbody>();
        rb.velocity = cachedTransform.forward * movementSpeed;
    }

    public void SetDestroyRange(float fireRange)
    {
        float destroyTime = fireRange / movementSpeed;

        Destroy(gameObject, destroyTime);
    }

    public void SetWeaponSpecifics(Weapon.WeaponSpecifics specifics)
    {
        weaponSpecifics = specifics;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<AIBehavior>()?.TakeDamage(weaponSpecifics.Damage, weaponSpecifics.StunDamage);
    }
}
