
using UnityEngine;

public class DisappearState : BossState
{
    private float timer = 0f;
    private float duration = 1.375f;

    public DisappearState(BossAI boss) : base(boss) { }

    public override void Enter()
    {
        boss.Animator.Play("Disappear");
        timer = 0f;
        boss.OrientTowardsPlayer();
        boss.bossManager.esInvulnerable = false;
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            boss.ChangeState(new MoveState(boss));
        }
    }
}

