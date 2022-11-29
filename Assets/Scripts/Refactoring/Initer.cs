using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initer : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private InputController inputController;
    [SerializeField] private AIController aIController;

    [SerializeField] private Score enemyScore;
    [SerializeField] private Score playerScore;
    private void Awake()
    {
        Init();
        spawner.Spawn();
    }

    private void Init()
    {
        spawner.OnPlayerSpawn += inputController.Setup;
        spawner.OnEnemySpawn += aIController.SetupEnemy;
        spawner.OnPlayerSpawn += aIController.SetupPlayer;


        spawner.OnPlayerSpawn += SubcribePlayer;
        spawner.OnEnemySpawn += SubcribeEnemy;
    }
    private void SubcribePlayer(Solder player)
    {
        player.Health.OnDead += enemyScore.AddScore;
    }
    private void SubcribeEnemy(Solder enemy)
    {
        enemy.Health.OnDead += playerScore.AddScore;
    } 
}
