using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedbaySannerTask : TaskBase
{
    [Header("Customization")]
    [SerializeField] private float taskDuration = 10f;
    [SerializeField] private float onTaskDuration;
    private float taskStartTime;
    public bool isOnStand = false;

    private void Update() {

        if (taskOnGoing){
            if (isOnStand && onTaskDuration < taskDuration){
                onTaskDuration = Time.time - taskStartTime;
            } 
            else if(onTaskDuration >= taskDuration){
                TaskFinish(thisTaskObj);
            }
            else{
                TaskStopRsvp(thisTaskObj);
            }
        }
    }

    protected override void TaskStartRsvpInternal()
    {
        base.TaskStartRsvpInternal();
        taskStartTime = Time.time;
    }

    public override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();
        taskStartTime = 0f;
        onTaskDuration = 0f;
    }
}
