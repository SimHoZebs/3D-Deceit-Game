using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        other.BroadcastMessage("Interact", gameObject.name);
    }
}
