using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Action<Solder> OnPlayerSpawn;
    public Action<Solder> OnEnemySpawn;

    [SerializeField] private Solder playerPrefab;
    [SerializeField] private Solder enemyPrefab;
    [SerializeField] private Transform startPosPlayer;
    [SerializeField] private Transform startPosEnemy;

    private Solder player;
    private Solder enemy;
    public void Spawn()
    {
        if(enemy != null)
            Destroy(enemy.gameObject);
        if (player != null)
            Destroy(player.gameObject);

        SpawnPlayer();
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        enemy = Instantiate(enemyPrefab, startPosEnemy.position, Quaternion.identity);
        OnEnemySpawn.Invoke(enemy);
        enemy.Health.OnDead += SpawnEnemy;
    }
    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, startPosPlayer.position, Quaternion.identity);
        OnPlayerSpawn.Invoke(player); 
        player.Health.OnDead += SpawnPlayer;
    }
}
