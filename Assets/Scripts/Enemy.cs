using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float attackRange = 1f;
    public float attackPower = 10f;
    public float recoveringTime = 0.2f;


    [SerializeField]
    EnemyManager m_EnemyManager;
    [SerializeField]
    Animator m_EnemyAnimator;
    [SerializeField]
    float _health = 100f;
    [SerializeField]
    Player target;

    //audio
    public AudioClip[] audioClips;
    AudioSource audio;
    CapsuleCollider collider;
    
    bool isRecovering = false;
    bool isDying = false;

    private void Start()
    {
        m_EnemyManager = FindObjectOfType<EnemyManager>();
        target = FindObjectOfType<Player>();
        collider = GetComponent<CapsuleCollider>();

        AddRandomAudioSource();

        StartCoroutine(PlayZombieAudio());
    }

    public Animator enemyAnimator
    {
        get { return m_EnemyAnimator; }

    }

    public EnemyManager enemyManager {
        get {
            return m_EnemyManager;
        }
    }

   
    private void Update()
    {
        
        if (CheckStartAttack())
        {
            Attack();

        }
        else
        {
            if (!isDying && !isRecovering)
            {
                MoveToCamera();

            }
        }
    }

    #region Enemy Movements
    void MoveToCamera()
    {
        if (enemyManager)
        {
            MoveToLocation(enemyManager.cameraTransform.position);
            enemyAnimator.SetTrigger("Walk");
        }
    }

    void MoveToLocation(Vector3 targetPoint)
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(enemyManager.cameraTransform.position.x-transform.position.x, 0, enemyManager.cameraTransform.position.z-transform.position.z), Vector3.up);

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPoint.x, moveSpeed * Time.deltaTime), transform.position.y, Mathf.Lerp(transform.position.z, targetPoint.z, moveSpeed * Time.deltaTime));
        transform.rotation = rotation;
    }

   
    #endregion

    #region Enemy Receive Attacks
    public void TakeDamage(float damage)
    {

        if (_health-damage > 0)
        {
            _health -= damage;
            if (!isRecovering)
            {
                StartCoroutine(HitRecover());
            }
        }
        else
        {
            _health = 0;
            Die();
        }
    }
    IEnumerator HitRecover()
    {
        isRecovering = true;
        enemyAnimator.enabled = false;
        yield return new WaitForSeconds(recoveringTime);
        isRecovering = false;
        enemyAnimator.enabled = true;
    }

    void Die()
    {
        isDying = true;
        enemyAnimator.SetTrigger("Die");
        Destroy(gameObject,1f);
    }


    #endregion
    #region Enemy Attack Player

    bool CheckStartAttack()
    {
        if ((transform.position - enemyManager.cameraTransform.position).magnitude < attackRange)
        {
            return true;
        }
        return false;
    }

    void Attack()
    {
        //play attack animation
        enemyAnimator.SetTrigger("Attack");

        Quaternion rotation = Quaternion.LookRotation(new Vector3(enemyManager.cameraTransform.position.x - transform.position.x, 0, enemyManager.cameraTransform.position.z - transform.position.z), Vector3.up);
        transform.rotation = rotation;


    }

    public void DamagePlayer()
    {
        //player health drop

        if (target != null)
        {
            target.TakeDamage(attackPower);
        }
        //red screen?

    }


    #endregion
    #region Enemy Audio
    void AddRandomAudioSource()
    {
        int audioIndex = UnityEngine.Random.Range(0, audioClips.Length);
        audio = gameObject.AddComponent<AudioSource>();
        if (audioClips[audioIndex])
        {
            audio.clip = audioClips[audioIndex];
        }
    }

    IEnumerator PlayZombieAudio()
    {
        while (!isDying)
        {

            audio.Play();
            yield return new WaitForSeconds(3f);
        }
    }
    #endregion

}
