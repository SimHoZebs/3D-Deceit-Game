using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : TaskBase
{

    public override void Start()
    {
        base.Start();
        isInteractionModeTask = true;
    }

    public override void TaskInteractResponse(GameObject task, GameObject player)
    {
        base.TaskInteractResponse(task, player);

        if (task == thisTaskObj){
        }
    }

    public override void TaskInterruptResponse(GameObject task)
    {
        base.TaskInterruptResponse(task);

    }

    public override void TaskComplete(GameObject task)
    {
        base.TaskComplete(task);
    }

    public override void ClearInteractingPlayerInfo()
    {
        base.ClearInteractingPlayerInfo();
        interactingPlayerCameraControl = null;
    }
}
