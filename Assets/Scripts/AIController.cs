using System.Collections;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Enemy enemy;
    private Player target;
    private Vector2 randomDirection;
    private Coroutine randomVector;


    public void Setup(Enemy enemy)
    {
        this.enemy = enemy; 
        enemy.OnPlayerDetected += InstTarget;
        enemy.OnStacked += ReboundVector;
    }
    public void InstTarget(Player player)
    {
        target = player;
    }
    private float switchLogicTime = 0.1f;
    private bool IsEnemyDetected()
    {
        return target != null;
    }

    private IEnumerator Start()
    {
        randomVector = StartCoroutine(UpdateRandomVectorCoroutine());
        while (true)
        {
            if (IsEnemyDetected())
            {
                yield return StartCoroutine(TargetLogic());
            }
            else if (IsEnemyDetected() == false)
            {
                UpdateRandomVector();
                yield return StartCoroutine(PatrolLogic());
            }

            yield return new WaitForSeconds(switchLogicTime);
        }
    }
    private void ReboundVector()
    {
        var newDirection = Vector3.Reflect(randomDirection, Vector3.right);
        randomDirection = newDirection;
        StopCoroutine(randomVector);
        randomVector = StartCoroutine(UpdateRandomVectorCoroutine());
    }

    private IEnumerator PatrolLogic()
    {
        while (IsEnemyDetected() == false)
        {
            enemy.Movement.UpdateDirection(randomDirection);

            yield return new WaitForSeconds(switchLogicTime);
        }
    }
    private IEnumerator UpdateRandomVectorCoroutine()
    {
        while (true)
        {
            UpdateRandomVector();
            yield return new WaitForSeconds(2f);
        }
    }
    private void UpdateRandomVector()
    {
        randomDirection = RandomVector();
    }
    private Vector2 RandomVector()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
    private IEnumerator TargetLogic()
    {
        while (IsEnemyDetected())
        {
            var direction = target.transform.position - enemy.transform.position;
            enemy.Movement.UpdateDirection(direction);
            enemy.Rotation.UpdateRotation(target.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
