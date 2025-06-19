using UnityEngine;

public abstract class BossState
{
    protected BossAI boss;

    public BossState(BossAI boss)
    {
        this.boss = boss;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
