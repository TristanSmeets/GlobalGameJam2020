using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    Transform cachedTransform;
    Rigidbody rb;

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
}
