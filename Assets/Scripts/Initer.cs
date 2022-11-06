using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initer : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private InputController inputController;
    [SerializeField] private AIController aIController;
    private void Awake()
    {

        Init();
        spawner.Spawn();
    }

    private void Init()
    {
        spawner.OnPlayerSpawn+= inputController.Setup;
        spawner.OnEnemySpawn+= aIController.Setup;
    }
}
