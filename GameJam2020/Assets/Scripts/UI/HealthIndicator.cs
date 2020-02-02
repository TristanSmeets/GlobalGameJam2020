using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.Progress;

public class HealthIndicator : MonoBehaviour
{
    private Progressor progressor;
    private Player.Player player;
    private const string playerTag = "Player";

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Player.Player>();
        progressor = GetComponentInChildren<Progressor>();
        progressor.AnimateValue = true;
        progressor.SetMax(player.GetPlayerStats().MaxHealth);
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        player.DamagedPlayer += OnDamagedPlayer;
    }
    private void RemoveListeners()
    {
        player.DamagedPlayer -= OnDamagedPlayer;
    }

    private void OnDamagedPlayer(float currentHealth)
    {
        progressor.SetValue(currentHealth);
    }
}
