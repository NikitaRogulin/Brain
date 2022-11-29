using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    public Vector3 Direction => direction;

    [SerializeField] private float speed;
    [SerializeField] private ContactFilter2D filter;
    [SerializeField] private float minRadius;
    [SerializeField] private Transform normalTransform;

    private Vector3 direction;
    public void UpdateDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }
    protected virtual void ReactToWall(Vector3 normal)
    {

    }

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
        
        if (Detect<Wall>(nextPosition, out Vector3 normal) == false)
        {
            transform.position = nextPosition;
        }
        else
        {
            ReactToWall(normal);
        }
    }
    private bool Detect<T>(Vector3 position,out Vector3 normal) where T : UnityEngine.Object
    {
        var collider = Physics2D.OverlapCircle(position, minRadius, filter.layerMask);
        normal = default;
        if (collider != null)
        {
            if (collider.TryGetComponent(out T obj))
            {
                var point = Physics2D.ClosestPoint(position, collider);
                normal = -(Vector3)point + position;
                return true;
            }
        }
        return false;
    }

   
}
