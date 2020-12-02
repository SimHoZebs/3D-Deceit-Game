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

    public override void TaskStartRsvp(GameObject taskObj, GameObject playerHandler)
    {
        base.TaskStartRsvp(taskObj, playerHandler);
        if (taskObj == thisTaskObj && isOnStand){

            taskStartTime = Time.time;
        }
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
        taskStartTime = 0f;
        onTaskDuration = 0f;
    }


}
