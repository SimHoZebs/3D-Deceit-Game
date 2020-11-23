using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode moveUp = KeyCode.W;
    [SerializeField] private KeyCode moveDown = KeyCode.S;
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveRight = KeyCode.D;
    [SerializeField] private KeyCode sprint = KeyCode.LeftShift;
    [SerializeField] private KeyCode interact = KeyCode.E;
    [SerializeField] private KeyCode interrupt = KeyCode.T;

    public static InputHandler _this ;

    public bool isMovingUp, isMovingDown, isMovingLeft, isMovingRight, isSprinting, isInteracting, hasInterrupted;
    // Start is called before the first frame update
    void Start()
    {
        _this = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        keyCheck(out isMovingUp, moveUp);
        keyCheck(out isMovingDown, moveDown);
        keyCheck(out isMovingLeft, moveLeft);
        keyCheck(out isMovingRight, moveRight);
        keyCheck(out isInteracting, interact);
        keyCheck(out hasInterrupted, interrupt, "KeyDown");

    }

    private void keyCheck(out bool isPressingKey,KeyCode keyCode, string keyMode = "Key"){
        if (keyMode == "Key"){
            isPressingKey = Input.GetKey(keyCode);
        }
        else if (keyMode == "KeyDown"){
            isPressingKey = Input.GetKeyDown(keyCode);
        }
        else{
            isPressingKey = false;
        }
    }

}
