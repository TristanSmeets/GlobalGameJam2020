using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Weapon
{
    public enum WeaponType { SHOTGUN, ASSAULT_RIFLE, SNIPER };
    public abstract class AbstractWeapon
    {
        [SerializeField] protected float fireSpeed = 10.0f;
        [SerializeField] protected float damage = 10.0f;
        [SerializeField] protected float fireRange = 10.0f;
        [SerializeField] protected WeaponType weaponType;

        public abstract void Fire();
        public WeaponType GetWeaponType()
        {
            return weaponType;
        }
    }
}
