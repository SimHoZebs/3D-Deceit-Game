using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedbaySanner : MonoBehaviour
{
    [SerializeField] private float taskDuration = 10f;
    [SerializeField] private float onTaskDuration;
    private float taskStartTime;
    private bool taskComplete = false;
    public static MedbaySanner _this;

    private void Start() {
        _this = this;
    }

    public void OnMedBayEnter(){
        Debug.Log("Starting task...");
        taskStartTime = Time.time;
    }

    public void OnMedBayStay(){        
        if (onTaskDuration < taskDuration){
            onTaskDuration = Time.time - taskStartTime;
        }

        //if task isn't considered complete but we stood there long enough
        if (!taskComplete && onTaskDuration >= taskDuration){
            //task is complete
            taskComplete = true;
            Debug.Log("Task complete");
        }
    }

}
