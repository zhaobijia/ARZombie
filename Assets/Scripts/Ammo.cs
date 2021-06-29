using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int maxAmmoAmount = 30;
    int ammoAmount;
    private void Start()
    {
        ammoAmount = maxAmmoAmount;
    }
    public int GetCurrentAmmo()
    {
        return ammoAmount;
    }

    public void ReduceCurrentAmmo()
    {
        if (ammoAmount > 1)
        {
            ammoAmount--;
        }
        else
        {
            ammoAmount = 0;
            BroadcastMessage("OutOfAmmo");
        }
    }

    public void ReloadAmmo()
    {
        ammoAmount = maxAmmoAmount;
    }
}
