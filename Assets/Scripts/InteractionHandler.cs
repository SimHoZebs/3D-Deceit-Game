using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float interactRange = 3f;

    //event system for all task interactions
    public event Action<GameObject> taskInteractions;
    public event Action<GameObject> taskInterruptions;
    public static InteractionHandler _this;

    //public var
    private GameObject interactableObj;

    private void Awake() {
        _this = this;
    }

    private void Update() {

        if (InputHandler._this.isInteracting){
            interactableObj = TargetedInteractableObj();
            InteractionHandler._this.TaskInteract(interactableObj);
        }
        if (InputHandler._this.hasInterrupted){
            _this.TaskInterrupt(interactableObj);
        }
        
    }

    private GameObject TargetedInteractableObj(){

        //ray stores information about a ray, such as its starting position 
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        //Physics.Raycast uses the data stored in ray and casts a ray
        //Physics.Raycast returns a bool for if it hit an object
        //assigns value Vector3 direction to hit
        RaycastHit hit;

        bool rayHit = Physics.Raycast(ray, out hit, interactRange);

        return rayHit && hit.transform.CompareTag("Interactable")? hit.transform.gameObject : null;
    }

    private void TaskInteract(GameObject task){
        if (task != null){
            Debug.Log("Player has interacted with " + task.name);
        }
        taskInteractions?.Invoke(task);
    }

    private void TaskInterrupt(GameObject task){
        if (interactableObj != null){
            taskInterruptions?.Invoke(task);
            CameraControl._this.ChangeCamMode(null);
        }
    }

    public void TaskComplete(){
        CameraControl._this.ChangeCamMode(null);
    }
}
