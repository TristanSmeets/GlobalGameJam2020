using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private AbstractWeapon[] weapons = new AbstractWeapon[3];

        public AbstractWeapon GetWeapon(WeaponType weaponType)
        {
            for(int i = 0; i < weapons.Length; ++i)
            {
                if(weapons[i].GetWeaponType() == weaponType)
                {
                    return weapons[i];
                }
            }
            return null;
        }
    }
}
