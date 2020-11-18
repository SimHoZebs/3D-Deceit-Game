using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //customization
    [SerializeField] private GameObject attachTarget;
    [SerializeField] private Vector3 camOffSet = new Vector3(0f, 1.6f, 0f);
    [SerializeField] private float camSensitivity = 2.0f;

    //Visualization purpose
    [SerializeField] private float xAxis = 0.0f;
    [SerializeField] private float yAxis = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //Have same position as the attachTarget with an offset
        transform.position = attachTarget.transform.position + camOffSet;

        //get mouse coords
        xAxis += camSensitivity * Input.GetAxis("Mouse X");
        yAxis -= camSensitivity * Input.GetAxis("Mouse Y");

        //convert mouse coords to eulerAngles 
        transform.eulerAngles = new Vector3(yAxis, xAxis, 0.0f);

        //same I too don't know wtf Euler Angles are but it works
        //Thanks StackOverflow
    }
}
