using System;
using System.Collections.Generic;
using UnityEngine;

public class FuelStation : TaskBase
{
    //Player can pause the task and come back later to
    [Header("Customizing")]
    [SerializeField] public float taskDuration = 5f;

    public Dictionary<GameObject, float> playersFuelContent = new Dictionary<GameObject, float>();
    private float taskingPlayerCurrFuel;
    private InputHandler playerInputHandler;
    private event Action Yeet;

    protected override void Start(){
        base.Start();
    }

    void Update(){

        if (taskOnGoing){
            if (playerInputHandler.isHoldingInteract && taskingPlayerCurrFuel < 100){
                taskingPlayerCurrFuel += 100 * Time.deltaTime/taskDuration;
            }
            else{
                TaskStopRsvp(thisTaskObj);
            }
        }
    }

    private void FillPlayerFuel(){
    }

    protected override void TaskStartRsvpInternal()
    {
        base.TaskStartRsvpInternal();

        if (!playersFuelContent.ContainsKey(taskingPlayerHandler)){
            playersFuelContent.Add(taskingPlayerHandler, 0f);
            Yeet?.Invoke();
        }
        playerInputHandler = taskingPlayerHandler.GetComponent<InputHandler>();
        taskingPlayerCurrFuel = playersFuelContent[taskingPlayerHandler];
    }

    protected override void TaskStopRsvpInternal()
    {
        playersFuelContent[taskingPlayerHandler] = taskingPlayerCurrFuel;
        base.TaskStopRsvpInternal();
    }

    protected override void TaskFinishInternal()
    {
        playersFuelContent.Remove(taskingPlayerHandler);
        base.TaskFinishInternal();
    }

    protected override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();
    }
}
