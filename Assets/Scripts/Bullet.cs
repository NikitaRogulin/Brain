using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField, Range(0, 100)] private int damage;
    private ISoldier owner;

    public Movement Movement => movement;

    public void Init(ISoldier soldier)
    {
        owner = soldier;
    }
    private Vector3 Rebound()
    {
        var newDirection = Vector3.Reflect(movement.Direction, Vector3.right);
        return newDirection;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ISoldier>(out ISoldier solder))
        {
            if (solder == owner)
            {
                return;
            }
            else
            {
                var health = solder.GetHealth();
                // var health = collision.gameobject.GetComponent<HP>();
                health.Hit(damage);
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            movement.UpdateDirection(Rebound());
        }
    }
}
