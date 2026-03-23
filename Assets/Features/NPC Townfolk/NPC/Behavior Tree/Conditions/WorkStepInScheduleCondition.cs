public class WorkStepInScheduleCondition : Node {
    private ScheduleStep scheduleStep;
    public WorkStepInScheduleCondition(ScheduleStep step)
    {
        scheduleStep = step;
    }
    public override bool Execute()
    {
        return scheduleStep == ScheduleStep.Work;
    }
}