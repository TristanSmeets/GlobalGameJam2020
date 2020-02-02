using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShifter : MonoBehaviour
{
    private Renderer renderer;
    float val = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void FadeToRed()
    {
        StartCoroutine(RedFader());
    }

    private IEnumerator RedFader()
    {
        while(val < 1)
        {
            val += 0.01f;
            renderer.sharedMaterial.SetFloat("_Shift", val);

            yield return null;
        }

        val = 1;
        yield return null;
    }

    private void FadeToBlue()
    {
        StartCoroutine(BlueFader());
    }

    private IEnumerator BlueFader()
    {
        while(val > 0)
        {
            val -= 0.01f;
            renderer.sharedMaterial.SetFloat("_Shift", val);

            yield return null;
        }

        val = 0;
        yield return null;
    }

    private void OnEnable()
    {
        GameStats.OnWaveStart += FadeToRed;
        GameStats.OnWaveEnd += FadeToBlue;
    }

    private void OnDisable()
    {
        renderer.sharedMaterial.SetFloat("_Shift", 0);

        GameStats.OnWaveStart -= FadeToRed;
        GameStats.OnWaveEnd -= FadeToBlue;
    }
}
