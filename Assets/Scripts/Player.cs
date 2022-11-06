using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Solder
{
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
