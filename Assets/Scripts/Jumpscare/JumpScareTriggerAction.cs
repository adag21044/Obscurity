using UnityEngine;

public class JumpScareTriggerAction : IAction
{
    private readonly ITriggerable triggerable;

    public JumpScareTriggerAction(ITriggerable triggerable)
    {
        this.triggerable = triggerable;
    }

    public void Execute()
    {
        triggerable.Trigger();
    }
}