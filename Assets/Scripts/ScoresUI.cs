using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoresUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI previousscore;


    // Update is called once per frame
    void Update()
    {
        distance.text = "Score " + GameManager.Instance.distance.ToString("#,#");
        coins.text = "Coins " + GameManager.Instance.coins.ToString("#,#");
        previousscore.text = "Previous Score " + PlayerPrefs.GetInt("Previous Score");

    }
}
