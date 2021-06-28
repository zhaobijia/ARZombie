using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;
    private float health;

    [SerializeField]
    Slider health_slider;

    [SerializeField]
    PostProcessLayer ppLayer;
    public float redScreenTime=0.1f;

    public DeathHandler deathHandler;

    [SerializeField] EnemyManager enemyManager;
    [SerializeField] EnemySpawn enemySpawn;
    public float MyHealth {
        get
        { return health; }
        set
        {
            if (value > 0)
            {
                health = value;
            }
        }
    }
    private void Start()
    {
        ppLayer.enabled = false;
        health = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (health-damage > 0)
        {
            //player take damage effect
            StartCoroutine(DamageEffect());
            health -= damage;

            UpdateHealthBar();
        }
        else
        {
            health = 0;
            UpdateHealthBar();
            //end of game objective
            deathHandler.ShowDeathPanel();
            //enemy manager destroy all children and stop spawning
            enemyManager.DestroyAllZombies();
            enemySpawn.StopSpawn();

        }
    }

    IEnumerator DamageEffect()
    {
        //screen red? screen cracked?
        ppLayer.enabled = true;
        yield return new WaitForSeconds(redScreenTime);
        if (health > 30f)
        {
            ppLayer.enabled = false;
        }
    }

    void UpdateHealthBar()
    {
        health_slider.value = (float)health / maxHealth;
    }


}
