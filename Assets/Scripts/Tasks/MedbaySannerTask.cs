using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedbaySannerTask : TaskBase
{
    [SerializeField] private float taskDuration = 10f;
    [SerializeField] private float onTaskDuration;
    private float taskStartTime;
    public static MedbaySannerTask _this;

    public override void Start() {
        base.Start();

        _this = this;
    }

    public void OnMedBayEnter(){


    }

    public void OnMedBayStay(){        
        if (!taskOnGoing && onTaskDuration < taskDuration){
            onTaskDuration = Time.time - taskStartTime;
            taskOnGoing = true;
        }

        //if task isn't considered complete but we stood there long enough
        else if (taskOnGoing && onTaskDuration >= taskDuration){
            //task is complete
            taskOnGoing = false;
            TaskComplete(thisTaskObj);
        }
    }

    public override void TaskInteractResponse(GameObject task, GameObject player)
    {
        //Medbay scan task is not a interact task.
    }

    public override void TaskInterruptResponse(GameObject task)
    {
        //Medbay scan task is not a interact task.
    }


}
