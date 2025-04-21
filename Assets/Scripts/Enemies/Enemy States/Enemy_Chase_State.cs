using UnityEngine;

public class Enemy_Chase_State : IEnemy_State
{
    private readonly Enemy_Controller enemy;
    private float attackTimer = 0f;

    public Enemy_Chase_State(Enemy_Controller enemy)
    {
        this.enemy = enemy;
    }

    public void Enter() { }

    public void Update()
    {
        if (!enemy.CanSeePlayer())
        {
            enemy.ChangeState(new Enemy_Wander_State(enemy));
            return;
        }

        if (enemy.IsPlayerInRange())
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= enemy.attackDelay)
            {
                enemy.Attack();
                attackTimer = 0f; // Reiniciar el temporizador después de atacar
            }
        }
        else
        {
            enemy.MoveTowards((enemy.target.position - enemy.transform.position).normalized);
        }
    }

    public void FixedUpdate() { }

    public void Exit() { }
}