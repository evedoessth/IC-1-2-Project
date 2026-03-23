using UnityEngine;

public class VillagerState
{
    protected Villager villager;
    protected VillagerStateMachine villagerStateMachine;

    public VillagerState(Villager villager, VillagerStateMachine villagerStateMachine)
    {
        this.villager = villager;
        this.villagerStateMachine = villagerStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
}
