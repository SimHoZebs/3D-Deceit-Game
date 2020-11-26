using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTaskHandler : MonoBehaviour{

    //Customization
    [SerializeField] private Camera cam;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private int assignedTaskCount = 3;
    private GameObject targetObj;

    //Player task response collection
    public event Action<GameObject, GameObject> taskStartRsvps;
    public event Action<GameObject> taskStopRsvps;

    public Dictionary<GameObject, int> assignedTasks = new Dictionary<GameObject, int>();

    //Caching
    private List<GameObject> allTasks = GameProperties.allTasks;
    private InputHandler inputHandler;

    private void Start() {
        inputHandler = gameObject.GetComponent<InputHandler>();

        RandomlyAssignTask();
    }

    private void Update() {

        if (inputHandler.isInteracting){
            targetObj = TargetedObj();
            PlayerStartTask();
        }
        if (inputHandler.hasInterrupted){
            PlayerStopTask();
        }
        
    }

    private void RandomlyAssignTask(){
        for (int i=0; i < assignedTaskCount; i++){

            var randomTaskIndex = UnityEngine.Random.Range(0, allTasks.Count);
            var selectedRandomTask = allTasks[randomTaskIndex];
            var selectedRandomTaskName = allTasks[randomTaskIndex].name;

            if (TaskIsDuplicate(selectedRandomTask)){
                allTasks.Remove(selectedRandomTask);
                continue;
            }
            else{
                Debug.Log("added " + selectedRandomTask.name);
                assignedTasks.Add(selectedRandomTask, GameProperties.taskNotStarted);
                allTasks.Remove(selectedRandomTask);
            }
        }
    }

    private bool TaskIsDuplicate(GameObject randomTask){

        foreach (GameObject task in assignedTasks.Keys){
            if (task.name == randomTask.name){
                return true;
            }
        }
        return false;
    }


    private GameObject TargetedObj(){
        //TargetObj can be null, and should have no response if it is.

        //ray stores information about how a ray should look
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        //Physics.Raycast casts the ray using that info
        //and returns a bool whether something collided within interactRange
        //assigns value Vector3 direction to hit
        RaycastHit hit;

        bool rayHit = Physics.Raycast(ray, out hit, interactRange);

        return rayHit && hit.transform.CompareTag("Interactable")? hit.transform.gameObject : null;
    }

    private void PlayerStartTask(){

        if (targetObj != null && assignedTasks.ContainsKey(targetObj)){
            Debug.Log("Player has interacted with " + targetObj.name);
            taskStartRsvps?.Invoke(targetObj, gameObject);
        }
    }

    private void PlayerStopTask(){

        if (targetObj != null && assignedTasks.ContainsKey(targetObj)){
            taskStopRsvps?.Invoke(targetObj);
        }
    }

}