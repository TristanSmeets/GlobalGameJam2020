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
        [SerializeField] protected float weaponKnockbackAmount = 20;
        [SerializeField] protected float weaponKnockbackSpeed = 40;
        [SerializeField] protected float weaponUnKnockbackSpeed = 20;
        protected Transform cachedTransform = null;
        protected static float cooldownTimer = 0.0f;
        protected ProjectileStats projectileStats;
        protected bool weaponGettingKnockedback = false;
        public float weaponKnockbackAngle = 0;

        public static float TotalDamageIncrease;
        public static float TotalFireRateIncrease;

        protected virtual void OnEnable()
        {
            cachedTransform = gameObject.transform;
            projectileStats = new ProjectileStats(weaponSpecifics.ProjectileSpeed,
                   weaponSpecifics.Damage,
                    weaponSpecifics.Stun,
                    weaponSpecifics.FireRange / weaponSpecifics.ProjectileSpeed);
            cooldownTimer = weaponSpecifics.FireRate / (1 + TotalFireRateIncrease * 0.01f);
        }

        protected virtual void WeaponKnockback(float angle)
        {
            weaponGettingKnockedback = true;
            weaponKnockbackAngle = angle;
        }

        protected virtual void WeaponRotation()
        {
            if(weaponGettingKnockedback)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(weaponKnockbackAngle, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z), Time.deltaTime * weaponKnockbackSpeed);
                if(Quaternion.Angle(Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, 0), Quaternion.Euler(weaponKnockbackAngle, 0, 0)) < 0.1f)
                {
                    weaponGettingKnockedback = false;
                    transform.localRotation = Quaternion.Euler(weaponKnockbackAngle, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
                }
            }
            else
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z), Time.deltaTime * weaponUnKnockbackSpeed);
            }
        }

        public abstract void FireWeapon();
        public abstract WeaponType GetWeaponType();

        protected void PlaySoundEffect(int pClipIndex)
        {
            SoundManagement sm = GameObject.Find("GameManager").GetComponent<SoundManagement>();
            sm.PlayAudioClip(sm.AudioClips[pClipIndex]);
        }
    }
}
