using System;
using UnityEngine;

public class VillagerIdleState : VillagerState
{
    private Vector3 _targetPos;
    public VillagerIdleState(Villager villager, VillagerStateMachine villagerStateMachine) : base(villager, villagerStateMachine)
    {
    }

    public override void EnterState() 
    {
        base.EnterState();
        Debug.Log("Entered idle state.");
        _targetPos = GetRandomPointInCircle();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Vector3 groundTarget = new Vector3(_targetPos.x, 0.05f, _targetPos.z);
        villager.MoveVillager(groundTarget);
        if ((villager.transform.position - groundTarget).sqrMagnitude <= 0.01f)
        {
            villager.IsMoving = false;
            _targetPos = GetRandomPointInCircle();
            villager.IsMoving = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
    private Vector3 GetRandomPointInCircle()
    {
        return villager.transform.position + UnityEngine.Random.insideUnitSphere * 0.5f;
    }
    

}
