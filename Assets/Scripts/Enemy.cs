using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour, ISoldier
{
    public event Action<Player> OnPlayerDetected;
    public event Action OnStacked;



    public Movement Movement => movement;
    public Rotation Rotation => rotation;
    public HP GetHealth() => hitPoints;

    [SerializeField] private Movement movement;
    [SerializeField] private Rotation rotation;
    [SerializeField] private HP hitPoints;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float tempShoot;
    [SerializeField] private float viewDistance;

    private TimeSpan reloadTime = TimeSpan.FromSeconds(2f);
    private DateTime lastShoot;

    public void Init()
    {
        lastShoot = DateTime.Now;
        hitPoints.OnDead += Dead;
    }
    private bool CanShoot()
    {
        if (DateTime.Now - lastShoot >= reloadTime)
        {
            return true;
        }
        return false;
    }
    private void Dead()
    {
        hitPoints.OnDead -= Dead;
        Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            var direction = player.transform.position - transform.position;
            var hits = Physics2D.RaycastAll(transform.position, direction.normalized, viewDistance);
            var list = new List<RaycastHit2D>(hits);
            list.RemoveAll(x => x.collider.gameObject.TryGetComponent<Enemy>(out Enemy en));
            if (list.Count > 0 && list[0].collider.gameObject == player.gameObject)
            {
                OnPlayerDetected.Invoke(player);
                Shoot(direction);
            }
            else
            {
                OnPlayerDetected.Invoke(null);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            OnStacked?.Invoke();
        }
    }
    private void Shoot(Vector3 direction)
    {
        if (CanShoot())
        {
            var bulet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulet.Init(this);
            bulet.Movement.UpdateDirection(direction);
            lastShoot = DateTime.Now;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            OnPlayerDetected.Invoke(null);
        }
    }
}
