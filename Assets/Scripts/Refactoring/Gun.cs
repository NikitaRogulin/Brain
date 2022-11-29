using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField, Range(1,59)] private int reloadTime;
    private DateTime lastShoot;
    private Solder owner;
    public void Init(Solder owner)
    {
        this.owner = owner;
    }
    public void Shoot(Vector3 direction)
    {
        if (CanShoot())
        {
            var bulet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulet.Init(owner);
            bulet.Movement.UpdateDirection(direction);
            lastShoot = DateTime.Now;
        }
    }
    private bool CanShoot()
    {
        if (DateTime.Now - lastShoot >= new TimeSpan(0,0, reloadTime))
        {
            return true;
        }
        return false;
    }

}
