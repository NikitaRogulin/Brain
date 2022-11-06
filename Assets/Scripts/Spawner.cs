using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Action<Player> OnPlayerSpawn;
    public Action<Enemy> OnEnemySpawn;

    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform startPosPlayer;
    [SerializeField] private Transform startPosEnemy;

    private Player player;
    private Enemy enemy;
    public void Spawn()
    {
        if(enemy != null)
            Destroy(enemy.gameObject);
        if (player != null)
            Destroy(player.gameObject);

        SpawnEnemy();
        SpawnPlayer();
    }
    private void SpawnEnemy()
    {
        enemy = Instantiate(enemyPrefab, startPosEnemy.position, Quaternion.identity);
        enemy.Init();
        OnEnemySpawn.Invoke(enemy);
        enemy.GetHealth().OnDead += SpawnEnemy;
    }
    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, startPosPlayer.position, Quaternion.identity);
        player.Init();
        OnPlayerSpawn.Invoke(player);
        player.GetHealth().OnDead += SpawnPlayer;
    }
}
