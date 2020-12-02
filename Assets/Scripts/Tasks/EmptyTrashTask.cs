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

    public override void Start()
    {
        base.Start();
        isTaskModeTask = true;
    }

    private void Update() {

        if (taskOnGoing && taskingPlayerInputHandler.holdInteract && onTaskDuartion < taskDuration){
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

    public override void TaskStartRsvp(GameObject taskObj, GameObject playerHandler)
    {
        base.TaskStartRsvp(taskObj, playerHandler);
        if(taskObj == thisTaskObj){

            taskingPlayerInputHandler = taskingPlayerHandler.GetComponent<InputHandler>();
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
        taskStartTime = onTaskDuartion = 0f;
    }

}
