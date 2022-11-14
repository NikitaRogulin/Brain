using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState 
{
    protected IEnemyStateSwitcher _switcher;
    public EnemyState(IEnemyStateSwitcher switcher)
    {
        _switcher = switcher;
    }
    public abstract void DoSomething();
   
}
public class WalkingEnemyState : EnemyState
{
    public WalkingEnemyState(IEnemyStateSwitcher switcher):base(switcher){}
    public override void DoSomething()
    {
        Debug.Log("Walking!");
    }
    
}
public class LookingForEnemyState : EnemyState
{
    public LookingForEnemyState(IEnemyStateSwitcher switcher) : base(switcher) { }
    public override void DoSomething()
    {
        Debug.Log("Looking For!");
    }
}