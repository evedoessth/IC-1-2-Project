public class EatStepInScheduleCondition : Node {
    private ScheduleStep scheduleStep;
    public EatStepInScheduleCondition(ScheduleStep step)
    {
        scheduleStep = step;
    }
    public override bool Execute()
    {
        return scheduleStep == ScheduleStep.Eat;
    }
    
}