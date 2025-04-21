using UnityEngine;

public class Enemy_Wander_State : IEnemy_State
{
    private readonly Enemy_Controller enemy;
    private float _directionChangeInterval = 2f;
    private float _timer = 0f;
    private Vector2 _moveDirection;

    public Enemy_Wander_State(Enemy_Controller enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        PickNewDirection();
    }

    public void Update()
    {
        if (enemy.CanSeePlayer())
        {
            enemy.ChangeState(enemy.chaseState);
            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= _directionChangeInterval)
        {
            PickNewDirection();
            _timer = 0f;
        }
    }

    public void FixedUpdate()
    {
        enemy.MoveTowards(_moveDirection);
    }

    public void Exit() { }

    private void PickNewDirection()
    {
        _moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
