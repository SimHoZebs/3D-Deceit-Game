using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    public event Action<GameObject, GameObject> interactableRsvps;
    private GameObject targetObj;
    private InputHandler inputHandler;
    private PlayerTaskHandler playerTaskHandler;

    private void Start() {
        inputHandler = gameObject.GetComponent<InputHandler>();
        playerTaskHandler = gameObject.GetComponent<PlayerTaskHandler>();
    }

    private void Update() {
        
        if (inputHandler.tryInteract && inputHandler.TargetedObj() != null){
            targetObj = inputHandler.TargetedObj();
            var objTag = targetObj.tag;

            if (objTag == "Interactable"){
                interactableRsvps?.Invoke(gameObject, targetObj);
            }
            else if (objTag == "Task"){
                playerTaskHandler.AttemptTask(targetObj);
            }
        }
    }

}
