using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenGlitching : MonoBehaviour
{
    [SerializeField]
    private Material glitchMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (glitchMaterial)
            Graphics.Blit(source, glitchMaterial);
        else
            Graphics.Blit(source, destination);
    }
}
