using System;

public interface IEnemyStateSwitcher 
{
    //public void Switch(Type type);
    public void Switch<T>() where T : EnemyState;
}
