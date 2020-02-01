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
        public float Damage;
        public float FireRate;
        public float FireRange;

        public WeaponSpecifics(float damage, float fireRate, float fireRange)
        {
            Damage = damage;
            FireRate = fireRate;
            FireRange = fireRange;
        }
    }
}
