using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour {

    protected GameObject thisObj;
    protected GameObject taskingPlayerHandler;
    protected bool isInteracting;

    protected virtual void Start(){

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")){
            var playerInteractHandler = player.GetComponentInChildren<InteractHandler>();
            playerInteractHandler.interactableRsvps += StartInteractRsvp;
        }
        thisObj = gameObject;
    }

    private void StartInteractRsvp(GameObject obj, GameObject playerHandler){
        if (obj == thisObj){
            taskingPlayerHandler = playerHandler;
            isInteracting = true;

            StartInteractRsvpInternal();
        }
    }

    protected virtual void StartInteractRsvpInternal(){

    }
}