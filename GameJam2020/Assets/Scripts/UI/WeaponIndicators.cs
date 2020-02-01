using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
using Weapon;

public class WeaponIndicators : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image BackgroundX = null; //Sniper
    [SerializeField] private Image BackgroundA = null; //Shotgun
    [SerializeField] private Image BackgroundB = null; //AssaultRifle
    [SerializeField] private Color normalColour = new Color(Color.grey.r, Color.grey.g, Color.grey.b, 0.75f);
    [SerializeField] private Color highlightColour = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.75f);
    [SerializeField] private float animationDuration = 0.1f;
    private Controller controller = null;
    private WeaponType currentWeapon = WeaponType.ASSAULT_RIFLE;

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<Controller>();
        ChangeButtonColour(currentWeapon, highlightColour);
        AddListeners();
    }
    private void OnDisable()
    {
        RemoveListeners();
    }
    private void AddListeners()
    {
        controller.SwitchingWeapon += OnSwitchingWeapon;
    }
    private void RemoveListeners()
    {
        controller.SwitchingWeapon -= OnSwitchingWeapon;
    }
    private void OnSwitchingWeapon(Weapon.WeaponType weaponType)
    {
        StopAllCoroutines();
        ChangeButtonColour(currentWeapon, normalColour);
        currentWeapon = weaponType;
        StartCoroutine(AnimateColour(GetButtonImage(weaponType)));
    }

    private void ChangeButtonColour(Weapon.WeaponType weaponType, Color colour)
    {
        switch (weaponType)
        {
            case WeaponType.ASSAULT_RIFLE:
                BackgroundB.color = colour;
                break;
            case WeaponType.SHOTGUN:
                BackgroundA.color = colour;
                break;
            case WeaponType.SNIPER:
                BackgroundX.color = colour;
                break;
        }
    }

    public Image GetButtonImage(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.ASSAULT_RIFLE:
                return BackgroundB;
            case WeaponType.SHOTGUN:
                return BackgroundA;
            case WeaponType.SNIPER:
                return BackgroundX;
        }
        return null;
    }

    private IEnumerator AnimateColour(Image image)
    {
        float elapsedTime = 0;
        while (elapsedTime < animationDuration)
        {
            image.color = Color.Lerp(normalColour, highlightColour, ((elapsedTime / animationDuration) * (elapsedTime / animationDuration)));
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        image.color = highlightColour;
    }
}
