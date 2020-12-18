using System.Collections.Generic;
using UnityEngine;

public class TaskBase : MonoBehaviour {

    [Header("Debugging")]
    protected bool taskOnGoing;
    protected bool isTaskModeTask = false;
    protected bool isAssignableTask = true;
    protected CameraControl taskingPlayerCamControl;   //PlayerCamControl
    protected GameObject taskingPlayerHandler; 
    protected PlayerTaskHandler taskingPlayerTaskHandler;    //PlayerTaskHandler

    //caching
    protected GameObject thisTaskObj;
    protected string thisTaskName;

    protected virtual void Start() {

        var playerList = GameObject.FindGameObjectsWithTag("Player");
        thisTaskObj = gameObject;
        thisTaskName = thisTaskObj.name;

        if (isAssignableTask){
            foreach(GameObject player in playerList){
                var playerTaskHandler = player.GetComponentInChildren<PlayerTaskHandler>();
                playerTaskHandler.taskStartRsvps += TaskStartRsvp;
            }
            GameProperties.allTasks.Add(thisTaskObj);
        }

    }

    private void TaskStartRsvp(GameObject taskObj, GameObject playerHandler){
        if (taskObj == thisTaskObj){

            taskingPlayerHandler = playerHandler;
            taskingPlayerTaskHandler = taskingPlayerHandler.GetComponent<PlayerTaskHandler>();

            Debug.Log(string.Concat("Task ", thisTaskName, " is initiated"));
            taskOnGoing = true;
            TaskingPlayerThisTaskStatus()[thisTaskObj] = GameProperties.taskOnGoing;
            taskingPlayerCamControl = isTaskModeTask? taskingPlayerHandler.GetComponent<CameraControl>(): null;

            taskingPlayerCamControl?.ChangeCamMode(thisTaskObj);

            TaskStartRsvpInternal();
        }
    }

    protected virtual void TaskStartRsvpInternal(){

    } 

    protected void TaskStopRsvp(GameObject taskObj){

        if (taskObj == thisTaskObj && TaskingPlayerTaskIs(GameProperties.taskOnGoing)){
            Debug.Log(string.Concat("Task ", thisTaskName, " is interrupted"));
            taskOnGoing = false;
            TaskingPlayerThisTaskStatus()[thisTaskObj] = GameProperties.taskNotStarted;

            taskingPlayerCamControl?.ChangeCamMode(null);

            TaskStopRsvpInternal();
        }
    }

    protected virtual void TaskStopRsvpInternal(){

        ClearTaskingPlayerInfo();
    }

    protected void TaskFinish(GameObject taskObj){

        if (taskObj == thisTaskObj && TaskingPlayerTaskIs(GameProperties.taskOnGoing)){
            Debug.Log(string.Concat("Task ", thisTaskName, " is complete"));
            taskOnGoing = false;
            TaskingPlayerThisTaskStatus()[thisTaskObj] = GameProperties.taskFinished;

            taskingPlayerCamControl?.ChangeCamMode(null);

            TaskFinishInternal();
        }
    }

    protected virtual void TaskFinishInternal(){

        ClearTaskingPlayerInfo();
    }

    protected virtual void ClearTaskingPlayerInfo(){
        taskingPlayerHandler = null;
        taskingPlayerTaskHandler = null;
        taskingPlayerCamControl = null;

    }

    protected bool TaskingPlayerTaskIs(int status) => TaskingPlayerThisTaskStatus()[thisTaskObj] == status? true:false;
    private Dictionary<GameObject, int> TaskingPlayerThisTaskStatus() => taskingPlayerTaskHandler.assignedTasks;
}
