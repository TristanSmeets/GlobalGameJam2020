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
        public float FireSpeedInSeconds;
        public float FireRange;

        public WeaponSpecifics(float damage, float fireSpeed, float fireRange)
        {
            Damage = damage;
            FireSpeedInSeconds = fireSpeed;
            FireRange = fireRange;
        }
    }
}
