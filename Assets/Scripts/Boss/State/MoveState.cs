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
        if (!boss.InAttackRange())
        {
            boss.MoveTowardsPlayer();
        }
        else
        {
            boss.ChangeState(new AttackState(boss));
        }
    }
}
