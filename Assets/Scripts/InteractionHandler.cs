using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] Camera cam;

    //event system for all task interactions
    public event Action<string> taskInteractions;
    public static InteractionHandler _this;

    private void Awake() {
        _this = this;
    }

    private void Update() {

        var interactableObj = TargetedInteractableObj();

        if (Input.GetKeyDown(KeyCode.E) && interactableObj != null){
            InteractionHandler._this.TaskInteract(interactableObj);
        }
    }

    private string TargetedInteractableObj(){

        //ray stores information about a ray, such as its starting position 
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        //Physics.Raycast uses the data stored in ray and casts a ray
        //Physics.Raycast returns a bool for if it hit an object
        //outputs any information for type RaycastHit to to hit
        RaycastHit hit;
        string selectedObjTag = Physics.Raycast(ray, out hit)? hit.transform.tag:null;

        return selectedObjTag == "Interactable"? hit.transform.name : null;
    }

    public void TaskInteract(string task){
        Debug.Log("Doing " + task);
        taskInteractions?.Invoke(task);
    }
}
