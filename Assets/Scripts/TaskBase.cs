using System.Collections.Generic;
using UnityEngine;

public class TaskBase : MonoBehaviour {

    [Header("Debugging")]
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
            GameProperties.allTasks.Add(thisTaskObj);
        }
    }

    public virtual void TaskStartRsvp(GameObject task, GameObject player){
        if (task == thisTaskObj){

            taskingPlayer = player;
            taskingPlayerTaskHandler = taskingPlayer.GetComponentInChildren<PlayerTaskHandler>();

            Debug.Log(string.Concat("Task ", thisTaskName, " is initiated"));
            taskOnGoing = true;
            TaskingPlayerThisTaskStatus()[thisTaskObj] = GameProperties.taskOnGoing;
            taskingPlayerCamControl = isTaskModeTask? taskingPlayer.GetComponentInChildren<CameraControl>(): null;

            taskingPlayerCamControl?.ChangeCamMode(thisTaskObj);
        }
    }

    public virtual void TaskStopRsvp(GameObject task){

        if (task == thisTaskObj && IsTaskingPlayerTask(GameProperties.taskOnGoing)){
            Debug.Log(string.Concat("Task ", thisTaskName, " is interrupted"));
            taskOnGoing = false;
            TaskingPlayerThisTaskStatus()[thisTaskObj] = GameProperties.taskNotStarted;

            taskingPlayerCamControl?.ChangeCamMode(null);
        }

    }

    public virtual void TaskFinish(GameObject task){

        if (task == thisTaskObj && IsTaskingPlayerTask(GameProperties.taskOnGoing)){
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

    public bool IsTaskingPlayerTask(int status) => TaskingPlayerThisTaskStatus()[thisTaskObj] == status? true:false;
    public Dictionary<GameObject, int> TaskingPlayerThisTaskStatus() => taskingPlayerTaskHandler.assignedTasks;
}