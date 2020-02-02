using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScreenGlitching))]
[ExecuteInEditMode]
public class ScreenGlitchController : MonoBehaviour
{
    private ScreenGlitching[] screenGlitchers;

    [SerializeField]
    [Range(0, 1)]
    private float glitch = 0;

    //Default values
    [SerializeField]
    private List<float> defaultStripeAmounts = new List<float>();
    [SerializeField]
    private List<float> defaultSeedSpeeds = new List<float>();
    [SerializeField]
    private List<float> defaultDistances = new List<float>();

    private void OnEnable()
    {
        screenGlitchers = GetComponents<ScreenGlitching>();

        if (screenGlitchers != null && screenGlitchers.Length > 0)
        {
            while (defaultStripeAmounts.Count < screenGlitchers.Length)
            {
                defaultStripeAmounts.Add(0);
            }

            while (defaultSeedSpeeds.Count < screenGlitchers.Length)
            {
                defaultSeedSpeeds.Add(0);
            }

            while (defaultDistances.Count < screenGlitchers.Length)
            {
                defaultDistances.Add(0);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < screenGlitchers.Length; i++)
        {
            UpdateStripeAmount(i, glitch);
            UpdateSeedSpeed(i, glitch);
            UpdateDistances(i, glitch);
        }
    } 

    private void UpdateStripeAmount(int _scriptIndex, float glitchValue)
    {
        float val = 0;
        if (glitchValue < 0.5) 
            val = Mathf.Lerp(0, defaultStripeAmounts[_scriptIndex], glitchValue * 2);
        else
            val = Mathf.Lerp(defaultStripeAmounts[_scriptIndex], 1, (glitchValue - 0.5f) * 2);
        screenGlitchers[_scriptIndex].GetMaterial().SetFloat("_StripesAmount", val);
    }

    private void UpdateSeedSpeed(int _scriptIndex, float glitchValue)
    {
        float val = 0;
        if (glitchValue < 0.5)
            val = Mathf.Lerp(0, defaultSeedSpeeds[_scriptIndex], glitchValue * 2);
        else
            val = Mathf.Lerp(defaultSeedSpeeds[_scriptIndex], 1, (glitchValue - 0.5f) * 2);
        screenGlitchers[_scriptIndex].GetMaterial().SetFloat("_NewSeedSpeed", val);
    }

    private void UpdateDistances(int _scriptIndex, float glitchValue)
    {
        float val = 0;
        if (glitchValue < 0.5)
            val = Mathf.Lerp(0, defaultDistances[_scriptIndex], glitchValue * 2);
        else
            val = defaultDistances[_scriptIndex];
        screenGlitchers[_scriptIndex].GetMaterial().SetFloat("_StripesDistance", val);
    }
}
