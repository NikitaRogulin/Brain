using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletMovement movement;
    [SerializeField, Range(0, 100)] private int damage;
    [SerializeField] private int MaxCountRebound;
    private int countRebound = 0;
    private Solder owner;

    public BulletMovement Movement => movement;

    public void Init(Solder soldier)
    {
        owner = soldier;
        movement.OnRicochet += CheckReboundCount;
    }
    private void CheckReboundCount() 
    {
        countRebound++;
        if (MaxCountRebound+1 == countRebound)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Solder solder))
        {
            if(IsItEnemy(solder))
            {
                solder.Health.Hit(damage);
                Destroy(gameObject);
            }
        }
    }
    private bool IsItEnemy(Solder solder)
    {
        return solder != owner;
    }
    
}
