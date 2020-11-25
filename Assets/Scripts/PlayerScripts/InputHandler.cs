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

    public bool isMovingUp, isMovingDown, isMovingLeft, isMovingRight, isSprinting;
    public bool isInteracting, hasInterrupted;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement set
        keyCheck(out isMovingUp, moveUp);
        keyCheck(out isMovingDown, moveDown);
        keyCheck(out isMovingLeft, moveLeft);
        keyCheck(out isMovingRight, moveRight);
        keyCheck(out isSprinting, sprint);

        //interaction set
        keyCheck(out isInteracting, interact, "Down");
        keyCheck(out hasInterrupted, interrupt, "Down");

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
