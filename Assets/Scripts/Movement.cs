using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //Speed Control
    [SerializeField] float walkAccel, runAccel, runMaxSpeed, walkMaxSpeed;

    //object instancing
    [SerializeField] private GameObject cam;
    private Animator animator;
    private CharacterController rigidBody;

    void Start()
    {
        //caching gameObjs
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<CharacterController>();
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

        if (InputHandler._this.isMovingUp){
            targetVel += forward;
        }
        if (InputHandler._this.isMovingDown){
            targetVel -= forward;
        }
        if (InputHandler._this.isMovingRight){
            targetVel += side;
        }
        if (InputHandler._this.isMovingLeft){
            targetVel -= side;
        }

        //account for sprinting
        float currentAccel = InputHandler._this.isSprinting? runAccel : walkAccel;
        //Normalize vector size and multiply to target accel for constant vel
        rigidBody.SimpleMove(Vector3.Normalize(targetVel) * currentAccel);
    }

}