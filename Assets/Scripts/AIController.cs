using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float viewDistance;
    [SerializeField] private Solder player;
    [SerializeField, Range(0f, 10f)] private float distnace;
    [SerializeField] private Road road;

    private GameObject playerGameobject;
    private Solder enemy;
    private float switchLogicTime = 0.1f;
    public void SetupEnemy(Solder enemy)
    {
        this.enemy = enemy;
    }
    public void SetupPlayer(Solder player)
    {
        this.player = player;
        playerGameobject = player.gameObject;
       
    }
    private bool IsPlayerVisiable()
    {
        if(playerGameobject == null)
        {
            return false;
        }
        var distance = player.transform.position - enemy.transform.position;
        if(distance.magnitude <= viewDistance)
        {
            var hits = Physics2D.RaycastAll(enemy.transform.position, distance.normalized, viewDistance);
            var list = new List<RaycastHit2D>(hits);
            list.RemoveAll(x=>x.collider.gameObject == enemy.gameObject);
            if (list.Count > 0 && list[0].collider.gameObject == player.gameObject)
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator Start()
    {
        while (true)
        {
            
            if (IsPlayerVisiable())
            {
                yield return StartCoroutine(Skirmish());

            }
            else if (IsPlayerVisiable() == false)
            {
                yield return StartCoroutine(Patrol());
            }
            

            yield return new WaitForSeconds(switchLogicTime);
        }
    }
    private IEnumerator Patrol()
    {
        while (IsPlayerVisiable() == false)
        {
            enemy.Movement.UpdateDirection(road.FindWay(enemy.transform.position));
            yield return new WaitForSeconds(switchLogicTime);
        }
    }
   
    private IEnumerator Skirmish()
    {
        while (IsPlayerVisiable())
        {
            var direction = player.transform.position - enemy.transform.position;
            if(direction.magnitude <= distnace)
            {
                enemy.Movement.UpdateDirection(Vector3.zero);
            }
            else
            {
                enemy.Movement.UpdateDirection(direction);
            }
            enemy.Rotation.UpdateRotation(player.transform.position); 
            enemy.Gun.Shoot(direction);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
