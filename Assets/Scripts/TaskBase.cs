using System.Collections.Generic;
using UnityEngine;

public class TaskBase : MonoBehaviour {

    public bool taskOnGoing;
    public bool isTaskModeTask = false;
    public CameraControl taskingPlayerCamControl;   //PlayerCamControl
    public GameObject taskingPlayer; 
    public PlayerTaskHandler taskingPlayerTaskHandler;    //PlayerTaskHandler

    //caching
    public GameObject thisTaskObj;
    public string thisTaskName;

    public virtual void Start() {

        var playerList = GameObject.FindGameObjectsWithTag("Player");
        thisTaskObj = gameObject;
        thisTaskName = thisTaskObj.name;

        foreach(GameObject player in playerList){
            var playerTaskHandler = player.GetComponentInChildren<PlayerTaskHandler>();
            playerTaskHandler.taskStartRsvps += TaskStartRsvp;
            playerTaskHandler.taskStopRsvps += TaskStopRsvp;
            GameProperties.allTasks.Add(thisTaskObj);
        }
    }

    public virtual void TaskStartRsvp(GameObject task, GameObject player){
        taskingPlayer = player;
        taskingPlayerTaskHandler = taskingPlayer.GetComponentInChildren<PlayerTaskHandler>();

        if (task == thisTaskObj && IsTaskingPlayerTask("notStarted")){
            Debug.Log(string.Concat("Task ", thisTaskName, " is initiated"));
            taskOnGoing = true;
            TaskingPlayerThisTaskStatus()[thisTaskObj] = GameProperties.taskOnGoing;
            taskingPlayerCamControl = isTaskModeTask? taskingPlayer.GetComponentInChildren<CameraControl>(): null;

            taskingPlayerCamControl?.ChangeCamMode(thisTaskObj);
        }
    }

    public virtual void TaskStopRsvp(GameObject task){

        if (task == thisTaskObj && IsTaskingPlayerTask("onGoing")){
            Debug.Log(string.Concat("Task ", thisTaskName, " is interrupted"));
            taskOnGoing = false;
            TaskingPlayerThisTaskStatus()[thisTaskObj] = GameProperties.taskNotStarted;

            taskingPlayerCamControl?.ChangeCamMode(null);
        }

    }

    public virtual void TaskFinish(GameObject task){

        if (task == thisTaskObj && IsTaskingPlayerTask("onGoing")){
            Debug.Log(string.Concat("Task ", thisTaskName, " is complete"));
            taskOnGoing = false;
            TaskingPlayerThisTaskStatus()[thisTaskObj] = GameProperties.taskComplete;

            taskingPlayerCamControl?.ChangeCamMode(null);
        }
    }

    public virtual void ClearTaskingPlayerInfo(){
        taskingPlayer = null;
        taskingPlayerTaskHandler = null;
        taskingPlayerCamControl = null;

    }

    public bool IsTaskingPlayerTask(string status){
        if (status == "notStarted" && TaskingPlayerThisTaskStatus()[thisTaskObj] == GameProperties.taskOnGoing){
            return true;
        }
        else if (status == "onGoing" && TaskingPlayerThisTaskStatus()[thisTaskObj] == GameProperties.taskOnGoing){
            return true;
        }
        else if (status == "complete" && TaskingPlayerThisTaskStatus()[thisTaskObj] == GameProperties.taskComplete){
            return true;
        }
        else{
            return false;
        }
    }

    public Dictionary<GameObject, int> TaskingPlayerThisTaskStatus() => taskingPlayerTaskHandler.assignedTasks;
}