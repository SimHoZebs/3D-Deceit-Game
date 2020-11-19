using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionControls : MonoBehaviour
{
    private bool pressedE;

    private void ReadInteractionInput(){
        pressedE = Input.GetKeyDown(KeyCode.E);
    }

    private void OnTriggerEnter(Collider other) {
        ReadInteractionInput();
        if (pressedE){
            other.SendMessage("InteractResponse");
        }
    }
}
