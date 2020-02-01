using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class Player : MonoBehaviour
{
    Controller controller = null;
    WeaponManager weaponManager = null;
    AbstractWeapon abstractWeapon = null;

    private void OnEnable()
    {
        controller = GetComponent<Controller>();
        weaponManager = GetComponent<WeaponManager>();
        abstractWeapon = weaponManager.GetWeapon(WeaponType.ASSAULT_RIFLE);

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
        abstractWeapon?.FireWeapon();
    }

    private void OnSwitchingWeapon(WeaponType weaponType)
    {
        abstractWeapon = weaponManager.GetWeapon(weaponType);
    }
}
