using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if (collsion.GetComponent<Player>() != null)
        {
            GameManager.Instance.RestartLevel();
        }

    }
}
