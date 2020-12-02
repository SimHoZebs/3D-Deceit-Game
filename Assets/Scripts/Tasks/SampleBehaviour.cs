using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBehaviour : MonoBehaviour
{
    private InspectSampleTask inspectSampleTask;
    private GameObject thisObj;
    private InteractHandler interactionHandler;
    private Material thisObjMaterial;

    public void Start() {
        //This Start() runs earlier than InspectSampleTask... so far

        //Adding this as an interactable obj in each playerInteractHandler
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")){
            var playerInteractHandler = player.GetComponentInChildren<InteractHandler>();
            playerInteractHandler.interactableRsvps += InteractableRsvp;
        }

        thisObj = gameObject;
        thisObjMaterial = thisObj.GetComponent<MeshRenderer>().material;

        inspectSampleTask = thisObj.GetComponentInParent<InspectSampleTask>();
        ChangeColor(inspectSampleTask.sampleDefaultColor);
        inspectSampleTask.sampleList.Add(gameObject);

    }

    public void ChangeColor(Color color){
        Debug.Log("I'm changing color!");
        thisObjMaterial.color = color;
    }

    private void InteractableRsvp(GameObject playerHandler, GameObject interactable){
        if (thisObj == interactable){

            Debug.Log(playerHandler.name + "Interacted with this");
            inspectSampleTask.chosenSample = thisObj;
        }
    }

}
