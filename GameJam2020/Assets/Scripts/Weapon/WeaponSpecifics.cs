using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weapon
{
    [Serializable]
    public struct WeaponSpecifics
    {
        public int Damage;
        public int Stun;
        public float FireRate;
        public float FireRange;
        public float ProjectileSpeed;

        public WeaponSpecifics(int damage, float fireRate, float fireRange, int stunDamage = 10, float projectileSpeed = 10.0f)
        {
            Damage = damage;
            FireRate = fireRate;
            FireRange = fireRange;
            Stun = stunDamage;
            ProjectileSpeed = projectileSpeed;
        }
    }
}
