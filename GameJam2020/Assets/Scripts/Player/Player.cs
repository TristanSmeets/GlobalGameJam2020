using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerStats stats = new PlayerStats(10,100);
        private Controller controller = null;
        private WeaponManager weaponManager = null;
        private HealthComponent healthComponent = null;

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
        }

        private void RemoveListeners()
        {
            controller.FiringWeapon -= OnFiringWeapon;
            controller.SwitchingWeapon -= OnSwitchingWeapon;
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
        }
    }
}
