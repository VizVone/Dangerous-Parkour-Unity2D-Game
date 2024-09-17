using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Main : MonoBehaviour
{

    [SerializeField] public GameObject WelcomeScreen;
    [SerializeField] public GameObject MainScreen;

    // Start is called before the first frame update
    void Start()
    {
        
        WelcomeScreen.SetActive(true);
        MainScreen.SetActive(false);
    }

    public void SwitchMenu()
    {
        WelcomeScreen.SetActive(false);
        MainScreen.SetActive(true);
    }
}
