using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTaskHandler : MonoBehaviour{

    //Customization
    [SerializeField] private int assignTaskCount = 3;

    //Player task response collection
    public event Action<GameObject, GameObject> taskStartRsvps;

    public Dictionary<GameObject, int> assignedTasks = new Dictionary<GameObject, int>();

    //Caching
    private List<GameObject> allTasks = GameProperties.allTasks;

    private void Start() {
        RandomlyAssignTask();
    }

    private void RandomlyAssignTask(){
        int assignedTaskCount = 0;

        while (assignedTaskCount < assignTaskCount){

            var randomTaskIndex = UnityEngine.Random.Range(0, allTasks.Count);
            var selectedRandomTask = allTasks[randomTaskIndex];
            var selectedRandomTaskName = allTasks[randomTaskIndex].name;

            if (AlreadyAssigned(selectedRandomTask)){
                allTasks.Remove(selectedRandomTask);
                continue;
            }
            else{
                Debug.Log("added " + selectedRandomTask.name);
                assignedTasks.Add(selectedRandomTask, GameProperties.taskNotStarted);
                allTasks.Remove(selectedRandomTask);

                assignedTaskCount++;
            }
        }
    }

    private bool AlreadyAssigned(GameObject randomTask){

        foreach (GameObject task in assignedTasks.Keys){
            if (task.name == randomTask.name){
                return true;
            }
        }
        return false;
    }

    public void StartTask(GameObject targetObj){

        if (assignedTasks.ContainsKey(targetObj)){
            Debug.Log("Player has interacted with " + targetObj.name);
            taskStartRsvps?.Invoke(targetObj, gameObject);
        }
        else{
            Debug.Log("Player is not assigned this task");
        }
    }

}