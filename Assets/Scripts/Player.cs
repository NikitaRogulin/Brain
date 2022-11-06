using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ISoldier
{
    public Movement Movement => movement;
    public Rotation Rotation => rotation;
    public HP GetHealth() => hitPoints;
    [SerializeField] private Movement movement;
    [SerializeField] private Rotation rotation;
    [SerializeField] private HP hitPoints;
    [SerializeField] private Bullet bulletPrefab;
    private TimeSpan reloadTime = TimeSpan.FromSeconds(2f);
    private DateTime lastShoot;
    private bool CanShoot()
    {
        if(DateTime.Now - lastShoot >= reloadTime)
        {
            return true;
        }
        return false;
    }
    
    public void Init()
    {
        lastShoot = DateTime.Now;
        hitPoints.OnDead += Dead;
    }
    private void Dead()
    {
        hitPoints.OnDead -= Dead;
        Destroy(gameObject);
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            var bulet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulet.Init(this);
            bulet.Movement.UpdateDirection(rotation.direction);
            lastShoot = DateTime.Now;
        }
    }
}
