using UnityEngine;

public class TaskBase : MonoBehaviour {

    public bool taskOnGoing;
    public bool isInteractionModeTask = false;
    public CameraControl interactingPlayerCameraControl;
    public GameObject interactingPlayer;
    public PlayerTaskHandler interactingPlayerTaskHandler;

    //caching
    public GameObject thisTaskObj;
    public string thisTaskName;

    public virtual void Start() {

        var playerList = GameObject.FindGameObjectsWithTag("Player");
        thisTaskObj = gameObject;
        thisTaskName = thisTaskObj.name;

        foreach(GameObject player in playerList){
            var playertaskHandler = player.GetComponentInChildren<PlayerTaskHandler>();
            playertaskHandler.taskInteractions += TaskInteractResponse;
            playertaskHandler.taskInterruptions += TaskInterruptResponse;
            GameProperties.allTasks.Add(thisTaskObj);
        }
    }

    public virtual void TaskInteractResponse(GameObject task, GameObject player){
        interactingPlayer = player;
        interactingPlayerTaskHandler = interactingPlayer.GetComponentInChildren<PlayerTaskHandler>();

        if (task == thisTaskObj && interactingPlayerTaskHandler.assignedTasks[thisTaskObj] == GameProperties.taskNotStarted){
            Debug.Log(string.Concat("Task ", thisTaskName, " is initiated"));
            taskOnGoing = true;
            interactingPlayerTaskHandler.assignedTasks[thisTaskObj] = GameProperties.taskOnGoing;

            if (isInteractionModeTask){
                interactingPlayerCameraControl = interactingPlayer.GetComponentInChildren<CameraControl>();
                interactingPlayerCameraControl.ChangeCamMode(thisTaskObj);
            }
        }
    }

    public virtual void TaskInterruptResponse(GameObject task){

        if (task == thisTaskObj && interactingPlayerTaskHandler.assignedTasks[thisTaskObj] == GameProperties.taskOnGoing){
            Debug.Log(string.Concat("Task ", thisTaskName, " is interrupted"));
            taskOnGoing = false;
            interactingPlayerTaskHandler.assignedTasks[thisTaskObj] = GameProperties.taskNotStarted;

            if (isInteractionModeTask){
                interactingPlayerCameraControl.ChangeCamMode(null);
            }
        }

    }

    public virtual void TaskComplete(GameObject task){

        if (task == thisTaskObj && interactingPlayerTaskHandler.assignedTasks[thisTaskObj] == GameProperties.taskOnGoing){
            Debug.Log(string.Concat("Task ", thisTaskName, " is complete"));
            taskOnGoing = false;
            interactingPlayerTaskHandler.assignedTasks[thisTaskObj] = GameProperties.taskComplete;

            if (isInteractionModeTask){
                interactingPlayerCameraControl.ChangeCamMode(null);
            }
        }
    }

    public virtual void ClearInteractingPlayerInfo(){
        interactingPlayer = null;
        interactingPlayerTaskHandler = null;

        if (isInteractionModeTask){
            interactingPlayerCameraControl = null;
        }
    }

}