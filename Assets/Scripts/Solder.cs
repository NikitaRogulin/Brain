using System;
using UnityEngine;

public abstract class Solder : MonoBehaviour, ISoldier
{
    public Movement Movement => movement;
    public Rotation Rotation => rotation;
    public HP GetHealth() => hitPoints;

    [SerializeField] protected Movement movement;
    [SerializeField] protected Rotation rotation;
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected HP hitPoints;

    protected TimeSpan reloadTime = TimeSpan.FromSeconds(2f);
    protected DateTime lastShoot;
    

    public void Init()
    {
        lastShoot = DateTime.Now;
        hitPoints.OnDead += Dead;
    }
    protected void Dead()
    {
        hitPoints.OnDead -= Dead;
        Destroy(gameObject);
    }
    protected bool CanShoot()
    {
        if (DateTime.Now - lastShoot >= reloadTime)
        {
            return true;
        }
        return false;
    }

}
