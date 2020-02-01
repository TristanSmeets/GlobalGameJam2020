﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
        Camera.main.GetComponent<CameraFollowPlayer>().ShakeCamera(0.4f, 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!particleSystem.isPlaying)
            Destroy(this.gameObject);
    }
}
