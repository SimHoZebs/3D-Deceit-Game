using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Movement set")]
    [SerializeField] private KeyCode moveUp = KeyCode.W;
    [SerializeField] private KeyCode moveDown = KeyCode.S;
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveRight = KeyCode.D;
    [SerializeField] private KeyCode sprint = KeyCode.LeftShift;

    [Header("Interaction set")]
    [SerializeField] private KeyCode interact = KeyCode.E;
    [SerializeField] private KeyCode interrupt = KeyCode.T;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private Camera cam;

    public bool isMovingUp, isMovingDown, isMovingLeft, isMovingRight, isSprinting;
    public bool tryInteract, tryInterrupt;

    private void Update()
    {
        //movement set
        keyCheck(out isMovingUp, moveUp);
        keyCheck(out isMovingDown, moveDown);
        keyCheck(out isMovingLeft, moveLeft);
        keyCheck(out isMovingRight, moveRight);
        keyCheck(out isSprinting, sprint);

        //interaction set
        keyCheck(out tryInteract, interact, "Down");
        keyCheck(out tryInterrupt, interrupt, "Down");

    }

    public GameObject TargetedObj(){
        //TargetObj can be null, and should have no response if it is.

        //ray stores information about how a ray should look
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        //Physics.Raycast casts the ray using that info
        //and returns a bool whether something collided within interactRange
        //assigns value Vector3 direction to hit
        RaycastHit hit;

        bool rayHit = Physics.Raycast(ray, out hit, interactRange);

        return rayHit? hit.transform.gameObject : null;
    }

    private void keyCheck(out bool isPressingKey,KeyCode keyCode, string keyMode=""){

        if (keyMode == "Down"){
            isPressingKey = Input.GetKeyDown(keyCode);
        }
        else if (keyMode == "Up"){
            isPressingKey = Input.GetKeyUp(keyCode);
        }
        else{
            isPressingKey = Input.GetKey(keyCode);
        }
    }

}
