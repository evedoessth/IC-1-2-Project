using UnityEngine;

public class VillagerDisruptionState : VillagerState
{
    public VillagerDisruptionState(Villager villager, VillagerStateMachine villagerStateMachine) : base(villager, villagerStateMachine)
    {
    }


    public override void EnterState() 
    {
        base.EnterState(); 
        //Debug.Log("I am panicking");
        villager.StopVillagerMovement();    
    }

    public override void ExitState()
    {
        base.ExitState();
        //Debug.Log("I have calmed down");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (villager.CurrentEmotion == Emotion.Neutral)
        {
            villager.StateMachine.ChangeState(villager.ScheduleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}