
using UnityEngine;

public class IdleState : BossState
{
    public IdleState(BossAI boss) : base(boss) { }

    public override void Enter()
    {
        boss.Animator.Play("Idle");
        boss.OrientTowardsPlayer();
    }

    public override void Update()
    {
        if (boss.ShouldDisappear())
        {
            boss.ChangeState(new DisappearState(boss));
        }
    }
}
