public class SleepStepInScheduleCondition : Node {
    
    private ScheduleStep scheduleStep;
    public SleepStepInScheduleCondition(ScheduleStep step)
    {
        scheduleStep = step;
    }
    public override bool Execute()
    {
        return scheduleStep == ScheduleStep.Sleep;
    }
}