using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerStats stats = new PlayerStats(10, 100);
        private Controller controller = null;
        private WeaponManager weaponManager = null;
        private HealthComponent healthComponent = null;
        public event Action<float> DamagedPlayer = delegate { };

        public delegate void PlayerDeath();
        public static event PlayerDeath OnPlayerDeath;

        private bool _shouldDie;

        private void OnEnable()
        {
            controller = GetComponent<Controller>();
            controller.SetMovementSpeed(stats.MovementSpeed);
            weaponManager = GetComponent<WeaponManager>();
            weaponManager.SwitchWeapon(WeaponType.ASSAULT_RIFLE);
            healthComponent = GetComponent<HealthComponent>();
            healthComponent.SetMaxHealth(stats.MaxHealth);
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            controller.FiringWeapon += OnFiringWeapon;
            controller.SwitchingWeapon += OnSwitchingWeapon;
            OnPlayerDeath += PlayPlayerDeathSound;
        }

        private void RemoveListeners()
        {
            controller.FiringWeapon -= OnFiringWeapon;
            controller.SwitchingWeapon -= OnSwitchingWeapon;
            OnPlayerDeath -= PlayPlayerDeathSound;
        }

        private void OnFiringWeapon()
        {
            weaponManager?.GetCurrentWeapon().FireWeapon();
        }

        private void OnSwitchingWeapon(WeaponType weaponType)
        {
            weaponManager.SwitchWeapon(weaponType);
        }

        public void TakeDamage(float damage)
        {
            healthComponent.ChangeHealth(-damage);

            if(healthComponent.GetCurrentHealth() <= 0)
            {
                //Destroy Player
                if(!_shouldDie)
                    OnPlayerDeath?.Invoke();
                _shouldDie = true;
            }

            DamagedPlayer(healthComponent.GetCurrentHealth());
        }

        public PlayerStats GetPlayerStats()
        {
            return stats;
        }

        private void PlayPlayerDeathSound()
        {
            SoundManagement sm = GameObject.Find("GameManager").GetComponent<SoundManagement>();
            sm.PlayAudioClip(sm.AudioClips[2]);
        }

        public void UpgradeHealth(float pTotalPercentUpgrade)
        {
            healthComponent.SetMaxHealth(stats.MaxHealth * (1 + pTotalPercentUpgrade * 0.01f));
            healthComponent.ChangeHealth(99999);
        }

        public void UpgradeDamage(float pTotalPercentUpgrade)
        {
            AbstractWeapon.TotalDamageIncrease = pTotalPercentUpgrade;
        }

        public void UpgradeMovementSpeed(float pTotalPercentUpgrade)
        {
            controller.SetMovementSpeed(stats.MovementSpeed * (1 + pTotalPercentUpgrade * 0.01f));
        }

        public void UpgradeFireRate(float pTotalPercentUpgrade)
        {
            AbstractWeapon.TotalFireRateIncrease = pTotalPercentUpgrade;
        }
    }
}
