using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTaskHandler : MonoBehaviour{

    //stores taskName and status in int
    [SerializeField] private Camera cam;
    [SerializeField] private float interactRange = 3f;
    public event Action<GameObject, GameObject> taskInteractions;
    public event Action<GameObject> taskInterruptions;
    public List<GameObject> allTasks = new List<GameObject>();

    public int assignedTaskCount = 2;
    private GameObject targetObj;
    private GameObject player;

    public Dictionary<GameObject, int> assignedTasks = new Dictionary<GameObject, int>();

    private InputHandler inputHandler;
    private void Start() {
        inputHandler = gameObject.GetComponent<InputHandler>();
        allTasks = GameProperties.allTasks;

        for (int i=0; i < assignedTaskCount; i++){

            var randomTaskIndex = UnityEngine.Random.Range(0, allTasks.Count);
            var selectedRandomTask = allTasks[randomTaskIndex];
            var selectedRandomTaskName = selectedRandomTask.name;

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

    private void Update() {

        if (inputHandler.isInteracting){
            targetObj = TargetedObj();
            PlayerInteract(targetObj, player);
        }
        if (inputHandler.hasInterrupted){
            PlayerInterrupt(targetObj, player);
        }
        
    }

    private bool TaskIsDuplicate(GameObject randomTask){

        foreach (GameObject task in assignedTasks.Keys){
            if (task.name != randomTask.name){
                continue;
            }
            else{
                return true;
            }
        }
        return false;
    }


    private GameObject TargetedObj(){

        //ray stores information about a ray, such as its starting position 
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        //Physics.Raycast uses the data stored in ray and casts a ray
        //Physics.Raycast returns a bool for if it hit an object
        //assigns value Vector3 direction to hit
        RaycastHit hit;

        bool rayHit = Physics.Raycast(ray, out hit, interactRange);

        return rayHit && hit.transform.CompareTag("Interactable")? hit.transform.gameObject : null;
    }

    private void PlayerInteract(GameObject task, GameObject player){
        if (task != null && assignedTasks.ContainsKey(task)){
            Debug.Log("Player has interacted with " + task.name);
            taskInteractions?.Invoke(task, gameObject);
        }
    }

    private void PlayerInterrupt(GameObject task, GameObject player){
        if (targetObj != null && assignedTasks.ContainsKey(task)){
            taskInterruptions?.Invoke(task);
        }
    }

}