using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField, Range(0, 100)] private int damage;
    [SerializeField] private int countRebound;
    private ISoldier owner;

    public Movement Movement => movement;

    public void Init(ISoldier soldier)
    {
        owner = soldier;
    }
    // Из за функции ниже нормально не работает отскок ... доработать 
    private Vector3 Rebound(Vector3 normal)
    {
        //Vector3 newDirection;
        //if(movement.Direction.y > movement.Direction.x)
        //{
        var newDirection = Vector3.Reflect(movement.Direction, normal);
        //}
        //else
        //{
        //    newDirection = Vector3.Reflect(movement.Direction, Vector3.up);
        //}

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
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            if (countRebound > 0)
            {
                movement.UpdateDirection(Rebound(collision.contacts[0].normal));
                countRebound--;
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
