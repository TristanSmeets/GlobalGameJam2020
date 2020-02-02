using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("\t\tPercentage Upgrades")]
    [SerializeField]
    private float _healthUpgradePercentage;
    [SerializeField]
    private float _damageUpgradePercentage;
    [SerializeField]
    private float _movementSpeedUpgradePercentage;
    [SerializeField]
    private float _fireRateUpgradePercentage;

    [Header("\t\tPercentage Deminishing Returns")]
    [SerializeField]
    private float _healthUpgradeDRPerUpgradePercentage;
    [SerializeField]
    private float _damageUpgradeDRPerUpgradePercentage;
    [SerializeField]
    private float _movementSpeedUpgradeDRPerUpgradePercentage;
    [SerializeField]
    private float _fireRateUpgradeDRPerUpgradePercentage;

    [Header("\t\tMinimum Percentage Upgrades")]
    [SerializeField]
    private float _minimumHealthPercentageUpgrade;
    [SerializeField]
    private float _minimumDamagePercentageUpgrade;
    [SerializeField]
    private float _minimumMovementSpeedPercentageUpgrade;
    [SerializeField]
    private float _minimumFireRatePercentageUpgrade;

    private float _totalHealthIncrease;
    private float _totalDamageIncrease;
    private float _totalMovementSpeedIncrease;
    private float _totalFireRateIncrease;

    private int _timesHealthUpgraded;
    private int _timesDamageUpgraded;
    private int _timesMovementSpeedUpgraded;
    private int _timesFireRateUpgraded;

    private int _upgradeTokens;
    private Player.Player _playerScript;

    public enum UpgradeType
    {
        Health,
        Damage,
        MovementSpeed,
        FireRate
    };

    void Start()
    {
        _playerScript = GameObject.Find("Player").GetComponent<Player.Player>();
        GameStats.OnRoundEnd += AddUpgradeToken;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            int rand = Random.Range(0, 4);
            switch(rand)
            {
                case 0:
                    UpgradePlayer(UpgradeType.Health);
                    break;
                case 1:
                    UpgradePlayer(UpgradeType.Damage);
                    break;
                case 2:
                    UpgradePlayer(UpgradeType.MovementSpeed);
                    break;
                case 3:
                    UpgradePlayer(UpgradeType.FireRate);
                    break;
            }
        }
    }

    public void UpgradePlayer(UpgradeType pTypeOfUpgrade)
    {
        if(!_playerScript)
            return;

        switch(pTypeOfUpgrade)
        {
            case UpgradeType.Health:
                _timesHealthUpgraded++;
                _totalHealthIncrease += _healthUpgradePercentage;
                _playerScript.UpgradeHealth(_totalHealthIncrease);
                _healthUpgradePercentage *= 1 - _healthUpgradeDRPerUpgradePercentage * 0.01f;
                _healthUpgradePercentage = Mathf.Max(_minimumHealthPercentageUpgrade, _healthUpgradePercentage);
                break;

            case UpgradeType.Damage:
                _timesDamageUpgraded++;
                _totalDamageIncrease += _damageUpgradePercentage;
                _playerScript.UpgradeDamage(_totalDamageIncrease);
                _damageUpgradePercentage *= 1 - _damageUpgradeDRPerUpgradePercentage * 0.01f;
                _damageUpgradePercentage = Mathf.Max(_minimumDamagePercentageUpgrade, _damageUpgradePercentage);
                break;

            case UpgradeType.MovementSpeed:
                _timesMovementSpeedUpgraded++;
                _totalMovementSpeedIncrease += _movementSpeedUpgradePercentage;
                _playerScript.UpgradeMovementSpeed(_totalMovementSpeedIncrease);
                _movementSpeedUpgradePercentage *= 1 - _movementSpeedUpgradeDRPerUpgradePercentage * 0.01f;
                _movementSpeedUpgradePercentage = Mathf.Max(_minimumMovementSpeedPercentageUpgrade, _movementSpeedUpgradePercentage);
                break;

            case UpgradeType.FireRate:
                _timesFireRateUpgraded++;
                _totalFireRateIncrease += _fireRateUpgradePercentage;
                _playerScript.UpgradeFireRate(_totalFireRateIncrease);
                _fireRateUpgradePercentage *= 1 - _fireRateUpgradeDRPerUpgradePercentage * 0.01f;
                _fireRateUpgradePercentage = Mathf.Max(_minimumFireRatePercentageUpgrade, _fireRateUpgradePercentage);
                break;
        }

        _upgradeTokens--;
    }

    private void AddUpgradeToken()
    {
        _upgradeTokens++;
    }

    public int UpgradeTokens { get => _upgradeTokens; }
    public float HealthUpgradePercentage { get => _healthUpgradePercentage; }
    public float DamageUpgradePercentage { get => _damageUpgradePercentage; }
    public float MovementSpeedUpgradePercentage { get => _movementSpeedUpgradePercentage; }
    public float FireRateUpgradePercentage { get => _fireRateUpgradePercentage; }
    public int TimesHealthUpgraded { get => _timesHealthUpgraded; }
    public int TimesDamageUpgraded { get => _timesDamageUpgraded; }
    public int TimesMovementSpeedUpgraded { get => _timesMovementSpeedUpgraded; }
    public int TimesFireRateUpgraded { get => _timesFireRateUpgraded; }
    public float TotalHealthIncrease { get => _totalHealthIncrease; }
    public float TotalDamageIncrease { get => _totalDamageIncrease; }
    public float TotalMovementSpeedIncrease { get => _totalMovementSpeedIncrease; }
    public float TotalFireRateIncrease { get => _totalFireRateIncrease; }
}
