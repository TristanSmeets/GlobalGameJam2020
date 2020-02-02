using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveIndicator : MonoBehaviour
{
    private TextMeshProUGUI waveText;
    private void OnEnable()
    {
        waveText = GetComponentInChildren<TextMeshProUGUI>();
        waveText.SetText("");
        AddListeners();
    }
    private void OnDisable()
    {
        RemoveListeners();
    }
    private void AddListeners()
    {
        GameStats.OnWaveStart += OnWaveStart;
    }
    private void RemoveListeners()
    {
        GameStats.OnWaveStart -= OnWaveStart;
    }
    private void OnWaveStart()
    {
        waveText.SetText($"Round: {GameStats.CurrentRound}\nWave: {GameStats.CurrentWave}");
    }
}
