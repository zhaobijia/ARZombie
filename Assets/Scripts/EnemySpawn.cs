using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARPlaneManager))]
public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    ARPlaneManager m_PlaneManager;

    [SerializeField]
    GameObject m_EnemyPrefab;


    [SerializeField]
    EnemyManager m_EnemyManager;

    public int enemySpawnDuration = 5;


    bool isSpawning;


    bool pause;


    public ARPlaneManager planeManager { 
        get { 
            return m_PlaneManager;  
        }
        
    }

    public GameObject enemyPrefab {
        get {
            return m_EnemyPrefab;
        }
        private set { }
    }

    public EnemyManager enemyManager { get { return m_EnemyManager; }  }


    private void Start()
    {
        //EnemySize = enemyPrefab.GetComponent<CapsuleCollider>().bounds.size;
        isSpawning = false;
        pause = false;
    }
    private void Update()
    {
        if (enemyManager.transform.childCount < enemyManager.maxZomebie)
        {
            pause = false;
        }
    }

    public void StartSpawn()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies(enemySpawnDuration));
        }
    }

    public void StopSpawn()
    {
        isSpawning = false;
        StopCoroutine(SpawnEnemies(enemySpawnDuration));
    }

    Vector3 RandomSpawnPosition(ARPlane plane)
    {
        
        Vector3 center = plane.center;
        Vector2 size = transform.TransformPoint(plane.size);

        //check for overlapping on spawning
        //??how

        //apply offsets
        float rx = UnityEngine.Random.Range(center.x - 0.5f * (size.x), center.x + 0.5f*(size.x));
        float rz = UnityEngine.Random.Range(center.z , center.z + (size.y));
        

        Vector3 randomPos = new Vector3(rx, center.y, rz);


        return randomPos;
    }
 
    IEnumerator SpawnEnemies(int spawnDuration)
    {
        while (isSpawning)
        {
            
            
                foreach (var plane in planeManager.trackables)
                {
                    //roll a dice 
                    float chanceToSpawn = UnityEngine.Random.Range(0f, 1f);
                    if (chanceToSpawn > 0.5f &&!pause)
                    {

                        //position
                        Vector3 spawnPos = RandomSpawnPosition(plane);
                        //rotation
                        Quaternion spawnRotation = Quaternion.LookRotation(new Vector3(enemyManager.cameraTransform.position.x, 0, enemyManager.cameraTransform.position.z), Vector3.up);
                        //instantiate
                        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPos, spawnRotation);
                        spawnedEnemy.transform.parent = enemyManager.transform;

                        if (enemyManager.transform.childCount >= enemyManager.maxZomebie)
                        {
                            pause = true;
                        }

                        //yield return new WaitForSeconds(0.5f);
                    }



                }
            
            
            yield return new WaitForSeconds(spawnDuration);
        }

        
    }


}
