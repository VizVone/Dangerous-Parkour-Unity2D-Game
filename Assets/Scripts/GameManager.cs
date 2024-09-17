using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    [HideInInspector] public float coins;
    [HideInInspector] public float distance;


    private void Awake()
    {
        Instance = this;
        
    }

    public void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }


    public void Update()
    {
        if (player.transform.position.x > distance)
        {
            distance = player.transform.position.x;
        }
    }


    public void UnlockPlayer()
    {
        
        player.playerUnlocked = true;
    }

    public void RestartLevel()
    {
        if (distance > PlayerPrefs.GetInt("Previous Score", 0))
        {
            PlayerPrefs.SetInt("Previous Score", (int) distance);
        }
        SceneManager.LoadScene(0);
    }
}
