using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenGlitching : MonoBehaviour
{
    [SerializeField]
    private Material glitchMaterial = null;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (glitchMaterial)
        {
            Graphics.Blit(source, destination, glitchMaterial);
        }
        else
            Graphics.Blit(source, destination);
    }

    public Material GetMaterial()
    {
        return glitchMaterial;
    }
}
