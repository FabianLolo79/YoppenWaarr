
using UnityEngine;

public class AttackState : BossState
{
    private float cooldown = 0.625f;
    private float timer = 0f;

    public AttackState(BossAI boss) : base(boss) { }

    public override void Enter()
    {
        boss.Animator.Play("Attack");
        timer = 0f;
        boss.AllowDamage();
        boss.OrientTowardsPlayer();
        boss.bossManager.esInvulnerable = false;
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            boss.ChangeState(new DisappearState(boss));
        }
    }
}