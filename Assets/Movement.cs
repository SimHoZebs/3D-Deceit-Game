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
    private Rigidbody rigidBody;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ReadInput();

        var targetVel = new Vector3(0,0,0);
        float currentAccel = heldShift? runAccel : walkAccel;

        var side = cam.transform.right;
        var forward = cam.transform.forward;
        forward.y = side.y = 0f;

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

        rigidBody.velocity = Vector3.Normalize(targetVel) * currentAccel;
    }

    private void ReadInput(){
        heldW = Input.GetKey(KeyCode.W);
        heldS = Input.GetKey(KeyCode.S);
        heldD = Input.GetKey(KeyCode.D);
        heldA = Input.GetKey(KeyCode.A);
        heldShift = Input.GetKey(KeyCode.LeftShift);
    }


    /*

    private void AccelToVector(string axis, string vector){

        //var currentLocalVel = cam.transform.TransformVector(rigidBody.velocity);
        var currentLocalVel = rigidBody.velocity;

        var currentLocalVelX = currentLocalVel.x;
        var currentLocalVelZ = currentLocalVel.z;

        var velSize = axis == "x"? currentLocalVelX:currentLocalVelZ;

        float currentMaxVel;
        float currentAccel;
        float decelVector = decel;

        if (heldShift){
            currentMaxVel = runMaxSpeed;
            currentAccel = runMaxSpeed;
        }
        else{
            currentMaxVel = walkMaxSpeed;
            currentAccel = walkAccel;
        }

        if (vector == "negative"){
            currentAccel = -currentAccel;
            decelVector = - decelVector;
        }

        if (velSize > currentMaxVel){
            Debug.Log("Deccelerating");
            velSize -= decelVector*Time.deltaTime;
        }
        else if ((vector == "negative" && velSize - 0.05f < currentMaxVel) || (vector == "positive" && velSize + 0.05f > currentMaxVel)){
            Debug.Log("Constant speed");
            velSize = currentMaxVel;
        }
        else{
            Debug.Log("Accelerating");
            velSize += currentAccel*Time.deltaTime;
        }

        if (axis == "x"){
            rigidBody.velocity = new Vector3(velSize, currentLocalVel.y, currentLocalVelZ);
        }
        else{
            rigidBody.velocity = new Vector3(currentLocalVelX, currentLocalVel.y, velSize);
        }

    }
    */


    /*
    private void DirectionDecel(string velAxisName, float axisVel, string veloVector = "positive"){
        if (veloVector == "negative"){
            if (axisVel + 0.05f >= 0){
                model.SetFloat(velAxisName, 0);
            }
            else{
                model.SetFloat(velAxisName, axisVel + decel * Time.deltaTime);
            }
        }

        else{
            if (axisVel - 0.05f <= 0){
                model.SetFloat(velAxisName, 0);
            }
            else{
                model.SetFloat(velAxisName, axisVel- decel * Time.deltaTime);
            }
        }
    }
    */
}