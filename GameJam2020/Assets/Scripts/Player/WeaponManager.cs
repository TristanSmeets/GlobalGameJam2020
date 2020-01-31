using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class WeaponManager
    {
        private Dictionary<WeaponType, AbstractWeapon> weapons = new Dictionary<WeaponType, AbstractWeapon>();

        public WeaponManager()
        { 
            // Add Weapon Implementations here.
        }

        public AbstractWeapon GetWeapon(WeaponType weapon)
        {
            return weapons[weapon];
        }
    }
}
