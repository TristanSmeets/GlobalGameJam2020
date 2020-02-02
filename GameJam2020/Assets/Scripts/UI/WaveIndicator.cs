using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveIndicator : MonoBehaviour
{
    private TextMeshProUGUI waveText;
    private uint currentWave = 0;
    private uint currentRound = 0;
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
        GameStats.OnRoundStart += OnRoundStart;
    }
    private void RemoveListeners()
    {
        GameStats.OnWaveStart -= OnWaveStart;
        GameStats.OnRoundStart -= OnRoundStart;
    }
    private void OnWaveStart()
    {
        ++currentWave;

        waveText.SetText($"Round: {currentRound}\nWave: {currentWave}");
    }
    private void OnRoundStart()
    {
        currentWave = 0;
        ++currentRound;
        waveText.SetText($"Round: {currentRound}\nWave: {currentWave}");
    }
}
