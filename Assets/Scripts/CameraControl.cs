using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //customization
    [SerializeField] private GameObject attachTarget;
    [SerializeField] private Vector3 camOffset = new Vector3(0f, 1.6f, 0f);
    [SerializeField] private float camSensitivity = 2.0f;

    //Visualization purpose
    [SerializeField] private float xAxis, yAxis = 0.0f;
    private string currentCamMode;

    private void Update(){

        ChangeCamMode();

        switch (currentCamMode)
        {
            default:
                HeadFollowMode();
                break;
            case "HeadFollowMode":
                HeadFollowMode();
                break;
            case "InteractionMode":
                InteractionMode();
                break;
        }
    }

    private void ChangeCamMode(){

        // T is the default key to change camMode. It must be held.
        if (Input.GetKey(KeyCode.E)){
            currentCamMode = "InteractionMode";
        }
        else{
            currentCamMode = "HeadFollowMode";
        }
    }

    public void HeadFollowMode(){

        //Have same position as the attachTarget with an offset
        transform.position = attachTarget.transform.position + camOffset;

        //get mouse coords
        xAxis += camSensitivity * Input.GetAxis("Mouse X");
        yAxis -= camSensitivity * Input.GetAxis("Mouse Y");

        //convert mouse coords to eulerAngles 
        transform.eulerAngles = new Vector3(yAxis, xAxis, 0.0f);

        //same I too don't know wtf Euler Angles are but it works
        //Thanks StackOverflow
    }

    public void InteractionMode(){

    }
}
