using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//switch weapons
public class WeaponManager : MonoBehaviour
{
    public Camera cam;
    [SerializeField] Weapon activedWeapon;

    public void Shoot()
    {
        activedWeapon.Shoot();
    }
}
