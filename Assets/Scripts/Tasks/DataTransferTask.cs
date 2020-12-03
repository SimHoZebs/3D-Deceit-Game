using UnityEngine;

public class DataTransferTask : TaskBase {

    [SerializeField] private float taskDuration = 8.5f;
    [SerializeField] private float onTaskDuration = 0f;
    private float taskStartTime = 0f;

    protected override void Start() {
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

    protected override void TaskStartRsvpInternal(){
        base.TaskStartRsvpInternal();

        if (thisTaskName == "DataDownloadTask"){
            DownloadStartRsvp();
        }
        else{
            UploadStartRsvp();
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

        if (downloadTask == null){
            Debug.Log("There is no download/upload task assigned");
        }
        else if (TaskingPlayerTaskIs(GameProperties.taskFinished)){
            Debug.Log("Download isn't done!");
            TaskStopRsvp(thisTaskObj);
        }
        else{
            Debug.Log("Upload starting");
            taskStartTime = Time.time;
        }
    }

    protected override void ClearTaskingPlayerInfo()
    {
        taskStartTime = 0f;
        base.ClearTaskingPlayerInfo();
    }
}