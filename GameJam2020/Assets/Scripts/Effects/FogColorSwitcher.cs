using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogColorSwitcher : MonoBehaviour
{
    private Renderer renderer;
    private float val = 0;

    private void OnEnable()
    {
        renderer = GetComponent<Renderer>();
        AddListeners();
    }

    private void OnDisable()
    {
        DestroyListeners();
    }

    private void AddListeners()
    {
        GameStats.OnWaveStart += FadeToRed;
        GameStats.OnWaveEnd += FadeToBlue;
    }

    private void DestroyListeners()
    {
        GameStats.OnWaveStart -= FadeToRed;
        GameStats.OnWaveEnd -= FadeToBlue;
    }

    private void FadeToRed()
    {
        StartCoroutine(RedFade());
    }

    private IEnumerator RedFade()
    {
        while(val < 1)
        {
            val += 0.01f;
            renderer.material.SetFloat("_Shift", val);
            yield return null;
        }

        val = 1;
        renderer.material.SetFloat("_Shift", val);
        yield return null;
    }

    private void FadeToBlue()
    {
        StartCoroutine(BlueFade());
    }

    private IEnumerator BlueFade()
    {
        while (val > 0)
        {
            val -= 0.01f;
            renderer.material.SetFloat("_Shift", val);
            yield return null;
        }

        val = 0;
        renderer.material.SetFloat("_Shift", val);
        yield return null;
    }
}
