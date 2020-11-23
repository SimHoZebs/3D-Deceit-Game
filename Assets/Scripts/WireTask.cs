using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : MonoBehaviour
{
    private string objName;

    private void Start() {
        objName = gameObject.name;
        InteractionHandler._this.taskInteractions += WireTaskStart;
    }

    private void WireTaskStart(string taskName){
        if (taskName == objName){
            Debug.Log("Successfully doing this");
        }
    }
}
