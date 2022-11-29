using System;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] private int value;
    public event Action OnDead;

    public void Hit(int damage)
    {
        value -= damage;
        if (value <= 0)
        {
            OnDead.Invoke();
        }
    }
    
}
