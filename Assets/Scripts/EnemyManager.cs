using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    public int maxZomebie = 10;
    [SerializeField]
    GameObject m_CameraObject;

    public Text enemyText;
    
    
    public Transform cameraTransform {
        get {
            return m_CameraObject.transform;
        }
    }

    private void Update()
    {
        enemyText.text = transform.childCount.ToString();
    }

    public void DestroyAllZombies()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
