using System;
using UnityEngine;

public class BulletMovement : Movement
{
    public Action OnRicochet;
    protected override void ReactToWall(Vector3 normal)
    {
        var newDirection = Vector3.Reflect(Direction, normal.normalized);
        UpdateDirection(newDirection);
        OnRicochet?.Invoke();
    }
}