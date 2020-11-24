using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransferTask : MonoBehaviour
{
    [SerializeField] private float taskDuration = 8.5f;

    [Header("Debugging")]
    [SerializeField] private string taskName;
    [SerializeField] private bool downloadTaskStarted, uploadTaskStarted = false;
    [SerializeField] private bool uploadComplete = false;
    [SerializeField] private float onTaskDuration = 0f;

    private float taskStartTime = 0f;
    void Start()
    {
        taskName = gameObject.name;

        InteractionHandler._this.taskInteractions += DataTransferTaskStart;

        InteractionHandler._this.taskInterruptions += TaskInterrupt;
    }

    private void Update() {

        if (downloadTaskStarted && taskName == "Download Task"){
            DownloadPart();
        }
        else if(taskName == "Upload Task" && PlayerTaskManager._this.downloadDone && !uploadComplete){
            UploadPart();
        }
    }

    private void DataTransferTaskStart(GameObject task){
        var taskName = task.name;
        
        if (taskName == "Download Task" && !downloadTaskStarted && !PlayerTaskManager._this.downloadDone){
            Debug.Log("Download starting");
            taskStartTime = Time.time;
            downloadTaskStarted = true;
            CameraControl._this.ChangeCamMode(task);
        }

        if (taskName == "Upload Task" && PlayerTaskManager._this.downloadDone && !uploadTaskStarted && !uploadComplete){
            Debug.Log("Upload starting");
            taskStartTime = Time.time;
            uploadTaskStarted = true;
            CameraControl._this.ChangeCamMode(task);
        }
    }

    private void TaskInterrupt(GameObject task){
        var taskName = task.name;

        if (taskName == gameObject.name){
            Debug.Log(taskName + " interrupted");
            taskStartTime = 0f;
        }
        
        if (taskName == "Download Task"){
            downloadTaskStarted = false;
        }
        else if (taskName == "Upload Task"){
            uploadTaskStarted = false;
        }
    }

    private void DownloadPart(){
        if (downloadTaskStarted && onTaskDuration < taskDuration){
            onTaskDuration = Time.time - taskStartTime;
        }
        else if(onTaskDuration >= taskDuration && !PlayerTaskManager._this.downloadDone){
            Debug.Log("Download complete");
            PlayerTaskManager._this.downloadDone = true;
            InteractionHandler._this.TaskComplete();
        }
    }

    private void UploadPart(){
        if (uploadTaskStarted && onTaskDuration < taskDuration){
            onTaskDuration = Time.time - taskStartTime;
        }
        else if(onTaskDuration >= taskDuration && !uploadComplete){
            Debug.Log("upload complete");
            uploadComplete = true;
            InteractionHandler._this.TaskComplete();
        }
    }
}
