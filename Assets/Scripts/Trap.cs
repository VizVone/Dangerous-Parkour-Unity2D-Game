using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour

{
    [SerializeField] protected float chanceToSpawn = 60;

    protected virtual void Start()
    {
        float chanceToDestroy = Random.Range(0f, 100f);

        if (chanceToDestroy > chanceToSpawn)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collsion)
    {
        
        if (collsion.GetComponent<Player>() != null)
        {
            
            collsion.GetComponent<Player>().Damage();
        }
        
    }
}
