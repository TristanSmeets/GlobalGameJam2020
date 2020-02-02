using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] upgradeTexts = new TextMeshProUGUI[4]; 
    private PlayerUpgrades playerUpgrades = null;
    private PlayerUpgrades.UpgradeType selectedUpgrade = PlayerUpgrades.UpgradeType.Damage;
    public static event Action StartNextRound = delegate { };
    
    private void OnEnable()
    {
        playerUpgrades = FindObjectOfType<PlayerUpgrades>();
        SetupUpgradeTexts();
    }
    
    public void UpgradePlayer()
    {
        playerUpgrades.UpgradePlayer(selectedUpgrade);
    }

    private void SetupUpgradeTexts()
    {
        for(int i = 0; i < upgradeTexts.Length; ++i)
        {
            upgradeTexts[i].SetText($"x {UpgradeText((PlayerUpgrades.UpgradeType)i)}");
        }
    }

    public int UpgradeText(PlayerUpgrades.UpgradeType upgradeType)
    {
        switch(upgradeType)
        {
            case PlayerUpgrades.UpgradeType.Damage:
                return playerUpgrades.TimesDamageUpgraded;
            case PlayerUpgrades.UpgradeType.FireRate:
                return playerUpgrades.TimesFireRateUpgraded;
            case PlayerUpgrades.UpgradeType.Health:
                return playerUpgrades.TimesHealthUpgraded;
            case PlayerUpgrades.UpgradeType.MovementSpeed:
                return playerUpgrades.TimesMovementSpeedUpgraded;
        }
        return 0;
    }

    public void SelectUpgrade(int upgrade)
    {
        selectedUpgrade = (PlayerUpgrades.UpgradeType)upgrade;
        upgradeTexts[upgrade].SetText($"x {UpgradeText(selectedUpgrade) + 1}");
        StartNextRound();
    }
}
