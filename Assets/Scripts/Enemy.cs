using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject leader; 
    public int frames; 
    public GameObject enemystatus;

    private Queue<Vector3> record = new Queue<Vector3>();
    

    public void Start()
    {
        enemystatus.SetActive(false);
        
    }

    public void DelayInvoke()
    {
        Invoke("SpawnDelay", 3);
    }

    public void SpawnDelay()
    {
        enemystatus.SetActive(true);
    }

    private void FixedUpdate()
    {

        record.Enqueue(leader.transform.position);
        if (record.Count > frames)
        {
            transform.position = record.Dequeue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            
            collision.GetComponent<Player>().Damage();

        } 
    }
}
