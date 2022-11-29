using UnityEngine;
[RequireComponent(typeof(HP),typeof(Movement))]
public class Solder : MonoBehaviour
{
    public Gun Gun => gun;
    public Rotation Rotation => rotation;

    public Movement Movement { get; private set; }
    public HP Health { get; private set; }

    [SerializeField] private Gun gun;
    [SerializeField] private Rotation rotation;
    private void Awake()
    {
        gun.Init(this);
        Movement = GetComponent<Movement>();
        Health = GetComponent<HP>();
        Health.OnDead += () => Destroy(gameObject);
    }
}
