using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] GameObject DeathPanel;

    [SerializeField]int currentSceneNumber=1;

    private void Awake()
    {
        DeathPanel.SetActive(false);
    }

    public void ShowDeathPanel()
    {
        DeathPanel.SetActive(true);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(currentSceneNumber);
        
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
