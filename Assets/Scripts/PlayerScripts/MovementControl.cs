using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{

    //Speed Control
    [SerializeField] float walkAccel, runAccel, runMaxSpeed, walkMaxSpeed;

    //object instancing
    [SerializeField] private GameObject cam;
    private Animator animator;
    private CharacterController charController;
    private InputHandler inputHandler;

    void Start()
    {
        //caching gameObjs
        animator = gameObject.GetComponent<Animator>();
        charController = gameObject.GetComponent<CharacterController>();
        inputHandler = gameObject.GetComponentInChildren<InputHandler>();
    }

    void FixedUpdate()
    {


        //get cam's Vector3 converted from local Z & X axis to global.
        var side = cam.transform.right;
        var forward = cam.transform.forward;
        //lock their Y axis to 0 because we don't wanna be moving upwards
        forward.y = side.y = 0f;

        //Basic WASD movement using vector arithmetic
        var targetVel = new Vector3(0,0,0);

        if (inputHandler.isMovingUp){
            targetVel += forward;
        }
        if (inputHandler.isMovingDown){
            targetVel -= forward;
        }
        if (inputHandler.isMovingRight){
            targetVel += side;
        }
        if (inputHandler.isMovingLeft){
            targetVel -= side;
        }

        //account for sprinting
        float currentAccel = inputHandler.isSprinting? runAccel : walkAccel;
        //Normalize vector size and multiply to target accel for constant vel
        charController.SimpleMove(Vector3.Normalize(targetVel) * currentAccel);
    }

}