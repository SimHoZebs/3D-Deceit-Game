using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    /*
    [SerializeField] private Camera cam;
    [SerializeField] private float interactRange = 3f;

    //event system for all task interactions
    private InputHandler inputHandler;
    private playerTaskHandler playerTaskHandler;

    //public var
    private GameObject targetObj;

    private void Awake() {
        inputHandler = gameObject.GetComponent<InputHandler>();
        playerTaskHandler = gameObject.GetComponent<playerTaskHandler>();
    }

    private void Update() {

        if (inputHandler.isInteracting){
            targetObj = TargetedObj();
            PlayerInteract(targetObj);
        }
        if (inputHandler.hasInterrupted){
            PlayerInterrupt(targetObj);
        }
        
    }

    private GameObject TargetedObj(){

        //ray stores information about a ray, such as its starting position 
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        //Physics.Raycast uses the data stored in ray and casts a ray
        //Physics.Raycast returns a bool for if it hit an object
        //assigns value Vector3 direction to hit
        RaycastHit hit;

        bool rayHit = Physics.Raycast(ray, out hit, interactRange);

        return rayHit && hit.transform.CompareTag("Interactable")? hit.transform.gameObject : null;
    }

    private void PlayerInteract(GameObject task, GameObject player){
        if (task != null && playerTaskHandler.assignedTaskStatus.ContainsKey(task)){
            Debug.Log("Player has interacted with " + task.name);
            playerTaskHandler.taskInteractions?.Invoke(task, gameObject);
        }
    }

    private void PlayerInterrupt(GameObject task, GameObject player){
        if (targetObj != null && playerTaskHandler.assignedTaskStatus.ContainsKey(task)){
            playerTaskHandler.taskInterruptions?.Invoke(task, gameObject);
        }
    }
    */
}
