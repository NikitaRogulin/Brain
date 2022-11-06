using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform normalTransform;
    private Vector3 direction;

    public Vector3 Direction => direction;
    
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        var delta = speed * Time.deltaTime;
        var deltaVector = delta * direction;
        transform.position += deltaVector;
    }
   
    public void UpdateDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }
}