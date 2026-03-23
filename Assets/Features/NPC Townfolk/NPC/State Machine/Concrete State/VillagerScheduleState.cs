using UnityEngine;
public class VillagerScheduleState : VillagerState
{
    private ScheduleStep _currentStep;
    public VillagerScheduleState(Villager villager, VillagerStateMachine villagerStateMachine) : base(villager, villagerStateMachine)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        _currentStep = villager.currentStep;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        //TODO:Create a boolean that checks for panic 
        if (villager.CurrentEmotion == Emotion.Panic || villager.IsCurrentlyCalmingDown)
        {
            villager.StateMachine.ChangeState(villager.DisruptionState);
        }
        switch (_currentStep)
        {
            case ScheduleStep.Sleep:
                HandleScheduleStep(villager.Home, _currentStep);
                break;
            case ScheduleStep.Eat:
                HandleScheduleStep(villager.Home, _currentStep);
                break;
            case ScheduleStep.Work:
                HandleScheduleStep(villager.WorkPlace, _currentStep);
                break;
            case ScheduleStep.Leisure:
                HandleScheduleStep(villager.Tavern, _currentStep);  
                break;
        }
    }

    private void HandleScheduleStep(GameObject destination, ScheduleStep scheduleStep)
    {
        Vector3 reachableDestination;
        reachableDestination = destination.transform.GetChild(2).gameObject.transform.position;
        
        float distanceToDestination = (villager.transform.position - reachableDestination).sqrMagnitude;
        const float minDistance = 2.0f;
        if (distanceToDestination >= minDistance && !villager.IsMoving && !villager.IsCurrentlyCalmingDown)
        {
            villager.MoveVillager(reachableDestination);
        }
        if (distanceToDestination <= minDistance && villager.IsMoving)
        {
            villager.StopVillagerMovement();
            //Debug.Log("Arrived at " + destination.name);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void ChangeScheduleStep(ScheduleStep newStep)
    {
        _currentStep = newStep;
    }
}