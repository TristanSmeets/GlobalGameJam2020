using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Weapon
{
    public enum WeaponType { SHOTGUN, ASSAULT_RIFLE, SNIPER };
    public abstract class AbstractWeapon : MonoBehaviour
    {
        [SerializeField] protected GameObject projectilePrefab = null;
        [SerializeField] protected Transform projectileSpawn = null;
        [SerializeField] protected WeaponSpecifics weaponSpecifics;
        protected Transform cachedTransform = null;
        protected float cooldownTimer = 0.0f;
        protected ProjectileStats projectileStats;

        protected virtual void OnEnable()
        {
            cachedTransform = gameObject.transform;
            projectileStats = new ProjectileStats(weaponSpecifics.ProjectileSpeed,
                    weaponSpecifics.Damage,
                    weaponSpecifics.Stun,
                    weaponSpecifics.FireRange / weaponSpecifics.ProjectileSpeed);
        }

        public abstract void FireWeapon();
        public abstract WeaponType GetWeaponType();
    }
}
