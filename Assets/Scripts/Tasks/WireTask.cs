using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : TaskBase
{

    public override void Start()
    {
        base.Start();
        isTaskModeTask = true;
    }

    protected override void TaskStartRsvpInternal()
    {
        base.TaskStartRsvpInternal();

    }

    public override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();
    }
}
