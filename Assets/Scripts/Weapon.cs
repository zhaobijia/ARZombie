using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera cam;

    [SerializeField] float range = 100f;

    [SerializeField] ParticleSystem muzzle;
    [SerializeField] GameObject hitEffectPrefab;
    [SerializeField] AudioSource audio;

    public float damage=30f;

    private void Start()
    {
        cam = GetComponentInParent<WeaponManager>().cam;
        audio = GetComponent<AudioSource>();
        
    }
    public void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
        PlayGunAudio();
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
