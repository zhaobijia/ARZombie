using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera cam;
    WeaponManager manager;
    [SerializeField] float range = 100f;

    [SerializeField] ParticleSystem muzzle;
    [SerializeField] GameObject hitEffectPrefab;
    [SerializeField] AudioSource audio;
    [SerializeField] Animator anim;
    [SerializeField] Ammo ammo;

    bool hasAmmo = true;

    public float damage=30f;

    private void Start()
    {
        manager = GetComponentInParent<WeaponManager>();
        cam = manager.cam;
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        ammo = GetComponent<Ammo>();

        manager.UpdateAmmoText();
        manager.ShowShootButton();
    }


  
    public void Shoot()
    {
        if (hasAmmo)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            PlayGunAudio();
            ReduceAmmo();
            PlayWeaponAnim();
            manager.UpdateAmmoText();
        }
        else
        {
            PlayMuzzleFlash();
        }
    }

    public void Reload()
    {
        ammo.ReloadAmmo();
        hasAmmo = true;
        manager.ShowShootButton();

    }

    

    public void ResetWeaponAnim()
    {
        anim.SetTrigger("Idle");
    }

    public int GetAmmo()
    {
        return ammo.GetCurrentAmmo();
    }
    private void ReduceAmmo() 
    {
        ammo.ReduceCurrentAmmo();
    }
    private void OutOfAmmo()
    {
        Debug.Log("out of ammo");
        hasAmmo = false;
        manager.ShowReloadButton();
    }
    private void PlayWeaponAnim()
    {
        anim.SetTrigger("Shoot");
    }

    private void PlayMuzzleFlash()
    {
        muzzle.Play();
    }
    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    private void PlayGunAudio()
    {
        audio.Play();
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitEffect, 0.1f);
    }
}
