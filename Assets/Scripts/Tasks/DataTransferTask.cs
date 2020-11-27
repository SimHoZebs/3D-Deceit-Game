using UnityEngine;

public class DataTransferTask : TaskBase {

    [SerializeField] private float taskDuration = 8.5f;
    [SerializeField] private float onTaskDuration = 0f;
    private float taskStartTime = 0f;

    public override void Start() {
        base.Start();
        isTaskModeTask = true;
    }

    private void Update() {

        if (taskOnGoing && onTaskDuration < taskDuration){
            onTaskDuration = Time.time - taskStartTime;
        }
        else if(onTaskDuration >= taskDuration && taskOnGoing){
            TaskFinish(thisTaskObj);
        }
    }

    public override void TaskStartRsvp(GameObject taskObj, GameObject player){
        base.TaskStartRsvp(taskObj, player);

        if (taskObj == thisTaskObj){

            if (thisTaskName == "DataDownloadTask"){
                DownloadStartRsvp();
            }
            else{
                UploadStartRsvp();
            }

        }
    }

    private void DownloadStartRsvp(){
        taskStartTime = Time.time;
    }

    private void UploadStartRsvp(){

        GameObject downloadTask = null;

        foreach (GameObject assignedTask in taskingPlayerTaskHandler.assignedTasks.Keys){
            if (assignedTask.name == "DataDownloadTask"){
                downloadTask = assignedTask;
                break;
            }
        }

        if (taskingPlayerTaskHandler.assignedTasks[downloadTask] != GameProperties.taskComplete){
            Debug.Log("Download isn't done!");
            TaskStopRsvp(thisTaskObj);
        }
        else{
            Debug.Log("Upload starting");
            taskStartTime = Time.time;
            taskingPlayerCamControl.ChangeCamMode(thisTaskObj);
        }
    }


    public override void TaskStopRsvp(GameObject taskObj){
        base.TaskStopRsvp(taskObj);

        if (taskObj == thisTaskObj){
            taskStartTime = 0f;

            ClearTaskingPlayerInfo();
        }

    }

    public override void TaskFinish(GameObject taskObj)
    {
        base.TaskFinish(taskObj);

        if (taskObj == thisTaskObj){
            taskStartTime = 0f;

            ClearTaskingPlayerInfo();
        }

    }

    public override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();
    }
}