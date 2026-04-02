using UnityEngine;

public class VillagerState
{
    protected Villager villager;
    protected VillagerStateMachine stateMachine;

    public VillagerState(Villager villager, VillagerStateMachine stateMachine)
    {
        this.villager = villager;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
}
