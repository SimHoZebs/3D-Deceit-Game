using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : MonoBehaviour
{

    private void Start() {
        InteractionHandler._this.taskInteractions += WireTaskStart;
    }

    private void WireTaskStart(GameObject task){
        if (task == gameObject){
            Debug.Log(gameObject.name + "detects player interaction");
        }
    }
}
