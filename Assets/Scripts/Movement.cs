using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ContactFilter2D filter;
    [SerializeField] private float minRadius;
    [SerializeField] private Transform normalTransform;
    private Vector3 direction;
    public Vector3 Direction => direction;

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector3 nextPosition;
        var delta = speed * Time.deltaTime;
        var deltaVector = delta * direction;
        nextPosition = transform.position + deltaVector;
        var canWeMove = Detect<Wall>(transform.position) == false;

        if (canWeMove )
        {
            transform.position = nextPosition;
        }
        else
        {
            var isNextPositionFree = Detect<Wall>(nextPosition);
            if (isNextPositionFree == false)
            {
                transform.position = nextPosition;
            }
        }
    }

    private bool Detect<T>(Vector3 position) where T : Object
    {
        var collider = Physics2D.OverlapCircle(position, minRadius, filter.layerMask);
        if (collider != null)
        {
            if (collider.TryGetComponent(out T obj))
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }
}