using UnityEngine;

public class VillagerStateMachine
{
    public VillagerState CurrentVillagerState { get; set; }

    public void Initialize(VillagerState startingState)
    {
        CurrentVillagerState = startingState;
        CurrentVillagerState.EnterState();
    }

    public void ChangeState(VillagerState newState)
    {
        CurrentVillagerState.ExitState();
        CurrentVillagerState = newState;
        CurrentVillagerState.EnterState();
    }
}
