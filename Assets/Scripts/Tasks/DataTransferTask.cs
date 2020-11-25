using UnityEngine;

public class DataTransferTask : TaskBase {

    [SerializeField] private float taskDuration = 8.5f;
    [SerializeField] private float onTaskDuration = 0f;
    private float taskStartTime = 0f;

    private void Update() {

        if (taskOnGoing && onTaskDuration < taskDuration){
            onTaskDuration = Time.time - taskStartTime;
        }
        else if(onTaskDuration >= taskDuration && taskOnGoing){
            Debug.Log("upload complete");
            TaskComplete(thisTaskObj);
        }
    }

    public override void TaskInteractResponse(GameObject taskObj, GameObject player){
        base.TaskInteractResponse(taskObj, player);

        if (taskObj == thisTaskObj){

            if (thisTaskName == "DataDownloadTask"){
                DownloadInteractResponse();
            }
            else{
                UploadInteractResponse();
            }

        }
    }

    private void DownloadInteractResponse(){
        taskStartTime = Time.time;
    }

    private void UploadInteractResponse(){

        GameObject downloadTask = null;

        foreach (GameObject assignedTask in interactingPlayerTaskHandler.assignedTasks.Keys){
            if (assignedTask.name == "DataDownloadTask"){
                downloadTask = assignedTask;
                break;
            }
        }

        if (interactingPlayerTaskHandler.assignedTasks[downloadTask] != GameProperties.taskComplete){
            Debug.Log("Download isn't done!");
            TaskInterruptResponse(thisTaskObj);
        }
        else{
            Debug.Log("Upload starting");
            taskStartTime = Time.time;
            interactingPlayerCameraControl.ChangeCamMode(thisTaskObj);
        }
    }


    public override void TaskInterruptResponse(GameObject taskObj){
        base.TaskInterruptResponse(taskObj);

        if (taskObj == thisTaskObj){
            taskStartTime = 0f;

            ClearInteractingPlayerInfo();
        }

    }

    public override void TaskComplete(GameObject taskObj)
    {
        base.TaskComplete(taskObj);

        if (taskObj == thisTaskObj){
            taskStartTime = 0f;

            ClearInteractingPlayerInfo();
        }

    }

    public override void ClearInteractingPlayerInfo()
    {
        base.ClearInteractingPlayerInfo();
        interactingPlayerCameraControl = null;
    }
}