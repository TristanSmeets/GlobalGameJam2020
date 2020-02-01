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
        public int StunDamage;
        public float FireRate;
        public float FireRange;

        public WeaponSpecifics(int damage, float fireRate, float fireRange, int stunDamage = 10)
        {
            Damage = damage;
            FireRate = fireRate;
            FireRange = fireRange;
            StunDamage = stunDamage;
        }
    }
}
