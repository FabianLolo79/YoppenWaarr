using UnityEngine;
public class Enemy_State_Machine
{
    private IEnemy_State currentState;

    public void ChangeState(IEnemy_State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update() => currentState?.Update();
    public void FixedUpdate() => currentState?.FixedUpdate();
}