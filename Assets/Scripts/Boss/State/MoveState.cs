using UnityEngine;

public class MoveState : BossState
{
    public MoveState(BossAI boss) : base(boss) { }

    public override void Enter()
    {
        boss.Animator.Play("Move");
        boss.OrientTowardsPlayer();
        boss.bossManager.esInvulnerable = true;


    }

    public override void Update()
    {
        boss.MoveTowardsPlayer();

        if (boss.InAttackRange())
        {
            boss.ChangeState(new AttackState(boss));
        }
    }
}
