using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionControls : MonoBehaviour
{
    public bool pressedE;
    private Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
    }


    private void ReadInput(){
        pressedE = Input.GetKeyDown(KeyCode.E);
    }

    private void Interact(){
        ReadInput();
        if (pressedE){
            Debug.Log("Interacted");
        }
    }
}
