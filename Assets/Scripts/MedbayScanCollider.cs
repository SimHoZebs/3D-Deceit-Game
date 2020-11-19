using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedbayScanCollider: MonoBehaviour
{
    // Start is called before the first frame update
    private float taskStartTime;
    [SerializeField] private float taskDuration = 10f;

    public bool attemptedInteract = false;
    private bool taskComplete = false;

    private void OnTriggerEnter(Collider other) {
        taskStartTime = Time.time;
        Debug.Log(taskStartTime);
    }

    private void OnTriggerStay(Collider other) {
        var onTaskDuration = Time.time - taskStartTime; 

        if (!taskComplete){
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
