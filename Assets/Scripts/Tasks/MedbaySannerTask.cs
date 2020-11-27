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

    public override void TaskStartRsvp(GameObject task, GameObject player)
    {
        base.TaskStartRsvp(task, player);
        if (task == thisTaskObj && isOnStand){

            taskStartTime = Time.time;
        }
    }

    public override void TaskStopRsvp(GameObject task)
    {
        base.TaskStopRsvp(task);

        if (task == thisTaskObj){

            taskStartTime = 0f;
            onTaskDuration = 0f;
            ClearTaskingPlayerInfo();
        }

    }

    public override void TaskFinish(GameObject task)
    {
        base.TaskFinish(task);

        if (task == thisTaskObj){

            taskStartTime = 0f;
            onTaskDuration = 0f;
            ClearTaskingPlayerInfo();
        }
    }


}
