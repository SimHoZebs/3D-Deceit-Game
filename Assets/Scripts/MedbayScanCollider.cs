using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedbayScanCollider: MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float taskDuration = 10f;
    [SerializeField] private float onTaskDuration;
    [SerializeField] private bool showTaskDuration;
    private float taskStartTime;
    private bool taskComplete = false;

    private void OnTriggerEnter(Collider other) {
        taskStartTime = Time.time;
        Debug.Log("Starting task...");
    }

    private void OnTriggerStay(Collider other) {
        onTaskDuration = Time.time - taskStartTime; 

        if (showTaskDuration && !taskComplete){
            Debug.Log("onTaskDuration " + onTaskDuration);
        }

        //if task isn't considered complete but we stood there long enough
        if (!taskComplete && onTaskDuration >= taskDuration){
            //task is complete
            taskComplete = true;
            Debug.Log("Task complete");
        }
    }

    public void InteractResponse(){
        Debug.Log("interacted with " + gameObject.name);
    }
}
