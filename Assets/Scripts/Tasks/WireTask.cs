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

    public override void TaskStartRsvp(GameObject task, GameObject player)
    {
        base.TaskStartRsvp(task, player);

        if (task == thisTaskObj){
        }
    }

    public override void TaskStopRsvp(GameObject task)
    {
        base.TaskStopRsvp(task);

    }

    public override void TaskFinish(GameObject task)
    {
        base.TaskFinish(task);
    }

    public override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();
    }
}
