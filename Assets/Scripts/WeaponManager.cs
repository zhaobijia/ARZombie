using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//switch weapons
public class WeaponManager : MonoBehaviour
{
    public Camera cam;
    [SerializeField] Weapon activedWeapon;

    public TMP_Text ammoText;

    [SerializeField] GameObject ShootButton;
    [SerializeField] GameObject ReloadButton;



    public void Shoot()
    {
        activedWeapon.Shoot();
    }

    public void Reload()
    {
        activedWeapon.Reload();
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
        ammoText.text = activedWeapon.GetAmmo().ToString();
    }

    public void ShowShootButton()
    {
        ReloadButton.SetActive(false);
        ShootButton.SetActive(true);
    }

    public void ShowReloadButton()
    {
        ReloadButton.SetActive(true);
        ShootButton.SetActive(false);
    }
}
