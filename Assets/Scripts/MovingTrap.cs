using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingTrap : Trap
{
    [SerializeField] private float speed;
    [SerializeField] private Transform[] movePoint;
    private int i;


    protected override void Start()
    {
        base.Start();
        transform.position = movePoint[0].position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint[i].position,speed*Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoint[i].position) < .25f) {
            i++;
            if (i >= movePoint.Length)
            {
                i = 0;
            }
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
