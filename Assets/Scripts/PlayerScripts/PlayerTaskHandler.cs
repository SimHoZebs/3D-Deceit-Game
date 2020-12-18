using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTaskHandler : MonoBehaviour{

    [Header("Customization")]
    [SerializeField] private int assignTaskCount = 3;

    //Player task response collection
    public event Action<GameObject, GameObject> taskStartRsvps;
    public Dictionary<GameObject, int> assignedTasks = new Dictionary<GameObject, int>();
    //scalable data structure for additional information required by specific tasks??

    //Caching

    private void Start() {
        RandomlyAssignTask();
    }

    private void RandomlyAssignTask(){
        int assignedTaskCount = 0;

        while (assignedTaskCount < assignTaskCount){

            var randomTaskIndex = UnityEngine.Random.Range(0, GameProperties.allTasks.Count);
            var selectedRandomTask = GameProperties.allTasks[randomTaskIndex];
            var selectedRandomTaskName = GameProperties.allTasks[randomTaskIndex].name;

            if (AlreadyAssigned(selectedRandomTask)){
                GameProperties.allTasks.Remove(selectedRandomTask);
                continue;
            }
            else{
                Debug.Log("added " + selectedRandomTask.name);
                assignedTasks.Add(selectedRandomTask, GameProperties.taskNotStarted);
                GameProperties.allTasks.Remove(selectedRandomTask);

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

    public void AttemptTask(GameObject targetObj){

        if (assignedTasks.ContainsKey(targetObj) && assignedTasks[targetObj] == GameProperties.taskNotStarted){
            Debug.Log("Player has started Task " + targetObj.name);
            taskStartRsvps?.Invoke(targetObj, gameObject);
        }
        else{
            Debug.Log("Player is either not assigned or not on NotStarted stage of this task");
        }
    }

}
