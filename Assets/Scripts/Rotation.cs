using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Vector3 target;
    public Vector3 direction => transform.up;
    private void Update()
    {
        Rotate(target);
    }
    private void Rotate(Vector3 target)
    {
        var direction = target - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }
    public void UpdateRotation(Vector3 target)
    {
        this.target = target;
    }
}
