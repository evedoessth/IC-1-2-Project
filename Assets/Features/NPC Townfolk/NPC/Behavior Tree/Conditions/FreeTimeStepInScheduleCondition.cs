public class FreeTimeStepInScheduleCondition : Node {
    private ScheduleStep scheduleStep;
    public FreeTimeStepInScheduleCondition(ScheduleStep step)
    {
        scheduleStep = step;
    }
    public override bool Execute()
    {
        return scheduleStep == ScheduleStep.Leisure;
    }
}