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

    public override void TaskStartRsvp(GameObject taskObj, GameObject playerHandler)
    {
        base.TaskStartRsvp(taskObj, playerHandler);

    }

    public override void TaskStopRsvp(GameObject taskObj)
    {
        base.TaskStopRsvp(taskObj);

        if (taskObj == thisTaskObj){
            ClearTaskingPlayerInfo();
        }


    }

    public override void TaskFinish(GameObject taskObj)
    {
        base.TaskFinish(taskObj);

        if (taskObj == thisTaskObj){
            ClearTaskingPlayerInfo();
        }

    }

    public override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();
    }
}
