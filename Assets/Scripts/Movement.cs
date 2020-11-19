using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //Speed Control
    [SerializeField] float walkAccel;
    [SerializeField] float runAccel;
    [SerializeField] float runMaxSpeed;
    [SerializeField] float walkMaxSpeed;

    //Read Input
    private bool heldW;
    private bool heldS;
    private bool heldD;
    private bool heldA;
    private bool heldShift;

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
        ReadMovementInput();

        float currentAccel = heldShift? runAccel : walkAccel;

        //get cam's Vector3 converted from local Z & X axis to global.
        var side = cam.transform.right;
        var forward = cam.transform.forward;
        //lock their Y axis to 0 because we don't wanna be moving upwards
        forward.y = side.y = 0f;

        //Basic WASD movement using vector arithmetic
        var targetVel = new Vector3(0,0,0);

        if (heldW){
            targetVel += forward;
        }
        if (heldS){
            targetVel -= forward;
        }
        if (heldD){
            targetVel += side;
        }
        if (heldA){
            targetVel -= side;
        }

        //Normalize vector size and multiply to target accel for constant vel
        rigidBody.SimpleMove(Vector3.Normalize(targetVel) * currentAccel);
    }

    private void ReadMovementInput(){
        heldW = Input.GetKey(KeyCode.W);
        heldS = Input.GetKey(KeyCode.S);
        heldD = Input.GetKey(KeyCode.D);
        heldA = Input.GetKey(KeyCode.A);
        heldShift = Input.GetKey(KeyCode.LeftShift);
    }

}