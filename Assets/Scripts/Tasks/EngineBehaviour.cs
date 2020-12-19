using System.Collections.Generic;
using UnityEngine;

public class EngineBehaviour : TaskBase
{
    [Header("Customizing")]
    [SerializeField] private float taskDuration = 5f;

    public Dictionary<GameObject, float> filledFuel = new Dictionary<GameObject, float>();

    private InputHandler taskingPlayerInputHandler;
    private float taskingPlayerFuelContent;
    private FuelStation fuelStation;

    protected override void Start(){
        isAssignableTask = false;

        //fuelStation
        base.Start();
    }

    void Update(){
        if (taskOnGoing){
            if (taskingPlayerInputHandler.isHoldingInteract){
                taskingPlayerFuelContent -= 100*Time.deltaTime / taskDuration;
            }
            else{
                TaskStopRsvp(thisTaskObj);
            }
        }
    }

    protected override void TaskStartRsvpInternal()
    {
        taskingPlayerInputHandler = taskingPlayerHandler.GetComponent<InputHandler>();
        taskingPlayerFuelContent = fuelStation.playersFuelContent[taskingPlayerHandler];
    }
}
