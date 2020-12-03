using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : TaskBase
{

    protected override void Start()
    {
        base.Start();
        isTaskModeTask = true;
    }

    protected override void TaskStartRsvpInternal()
    {
        base.TaskStartRsvpInternal();

    }

    protected override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();
    }
}
