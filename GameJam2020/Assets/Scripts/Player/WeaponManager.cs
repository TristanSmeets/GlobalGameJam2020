using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Player
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] weapons = new GameObject[0];
        [SerializeField] private Transform WeaponSpawn = null;
        private GameObject currentWeapon = null;

        public void SwitchWeapon(WeaponType weaponType)
        {
            if(GetCurrentWeapon() != null && GetCurrentWeapon().GetWeaponType() == weaponType)
                return;

            if(currentWeapon != null)
            {
                Destroy(currentWeapon);
            }

            if(GetWeapon(weaponType) == null)
            {
                return;
            }

            currentWeapon = Instantiate(GetWeapon(weaponType), WeaponSpawn.position, WeaponSpawn.rotation, WeaponSpawn);
        }

        public GameObject GetWeapon(WeaponType weaponType)
        {
            for(int i = 0; i < weapons.Length; ++i)
            {
                if(weapons[i].GetComponent<AbstractWeapon>()?.GetWeaponType() == weaponType)
                {
                    return weapons[i];
                }
            }
            return null;
        }

        public AbstractWeapon GetCurrentWeapon()
        {
            return currentWeapon?.GetComponent<AbstractWeapon>();
        }
    }
}
