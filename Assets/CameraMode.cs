using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMode : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CameraOffset cameraOffset;
    
    [System.Serializable]
    private struct CameraOffset{
        public float x; 
        public float y;
        public float z;
    }

    [SerializeField] private float camSensitivity = 2.0f;
    [SerializeField] private float yaw = 0.0f;
    [SerializeField] private float pitch = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var cameraPos = player.transform.position;
        var camTransform = gameObject.transform;

        cameraPos.x += cameraOffset.x;
        cameraPos.y += cameraOffset.y;
        cameraPos.z += cameraOffset.z;

        transform.position = cameraPos;

        yaw += camSensitivity * Input.GetAxis("Mouse X");
        pitch -= camSensitivity * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    }
}
