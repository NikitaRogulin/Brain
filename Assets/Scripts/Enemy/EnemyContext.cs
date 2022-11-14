using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContext : MonoBehaviour, IEnemyStateSwitcher
{
    private List<EnemyState> _states;
    private EnemyState _actualState;

    private void Awake()
    {
        _states = new List<EnemyState>() 
        {
            new WalkingEnemyState(this),
            new LookingForEnemyState(this)
        };
        Switch<WalkingEnemyState>();
    }


    //Частная реализация
    public void Switch<T>() where T : EnemyState
    {
        var nextState = _states.Find(x => x is T);
        _actualState = nextState;
    }


    public void Update()
    {
        _actualState.DoSomething();
    }
    //Частная реализация


}
