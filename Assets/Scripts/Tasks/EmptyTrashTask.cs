using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTrashTask : TaskBase
{
    [Header("Customization")]
    [SerializeField] private float taskDuration = 2.5f;

    private float taskStartTime = 0f;
    private float onTaskDuartion = 0f;

    //caching
    private InputHandler taskingPlayerInputHandler;

    protected override void Start()
    {
        base.Start();
        isTaskModeTask = true;
    }

    private void Update() {

        if (taskOnGoing && taskingPlayerInputHandler.holdingInteract && onTaskDuartion < taskDuration){
            Debug.Log("Holding...");
            onTaskDuartion = Time.time - taskStartTime;
        }
        else if (onTaskDuartion >= taskDuration){
            TaskFinish(thisTaskObj);
        }
        else if (taskOnGoing){
            TaskStopRsvp(thisTaskObj);
        }
    }

    protected override void TaskStartRsvpInternal()
    {
        base.TaskStartRsvpInternal();
        taskingPlayerInputHandler = taskingPlayerHandler.GetComponent<InputHandler>();
        taskStartTime = Time.time;
    }

    protected override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();
        taskStartTime = onTaskDuartion = 0f;
    }

}
