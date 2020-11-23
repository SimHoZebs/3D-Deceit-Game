using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Head-Follow Mode")]
    [SerializeField] public int headFollowModeId = 0;
    [SerializeField] private GameObject attachTarget;
    [SerializeField] private Vector3 headFollowModeCamOffset = new Vector3(0f, 1.6f, 0f);
    [SerializeField] private float camSensitivity = 2.0f;

    [Header("Interaction Mode")]
    [SerializeField] public int interactionModeId = 1;
    [SerializeField] private GameObject focusObj;
    [SerializeField] private Vector3 interactionModeCamOffset = new Vector3(0f, 0f, -3f);
    //interact with public variables
    public static CameraControl _this;

    //Visualization purpose
    [Header("Debugging data")]
    [SerializeField] private float xAxis, yAxis = 0.0f;
    private int currentCamMode;

    private void Start() {
        _this = this;
        InteractionHandler._this.taskInteractions += ChangeCamMode;
    }

    private void Update(){

        if (currentCamMode == headFollowModeId){
            HeadFollowMode();
        }
        else if(currentCamMode == interactionModeId){
            InteractionMode(focusObj);
        }
    }

    public void ChangeCamMode(GameObject task){

        if (task == null){
            Debug.Log("Normal Mode");
            currentCamMode = headFollowModeId;
            focusObj = null;
        }
        else{
            Debug.Log("Interaction Mode");
            currentCamMode = interactionModeId;
            focusObj = task;
        }
    }

    public void HeadFollowMode(){

        //Have same position as the attachTarget with an offset
        transform.position = attachTarget.transform.position + headFollowModeCamOffset;

        //get mouse coords
        xAxis += camSensitivity * Input.GetAxis("Mouse X");
        yAxis -= camSensitivity * Input.GetAxis("Mouse Y");

        //convert mouse coords to eulerAngles 
        transform.eulerAngles = new Vector3(yAxis, xAxis, 0.0f);

        //same I too don't know wtf Euler Angles are but it works
        //Thanks StackOverflow
    }

    public void InteractionMode(GameObject task){
        transform.position = task.transform.position + interactionModeCamOffset;
    }
}
