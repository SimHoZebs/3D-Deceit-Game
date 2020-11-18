using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canInteract = false;
    public string nama;
    private void OnTriggerStay(Collider other) {
        name = other.name;
        other.BroadcastMessage("Interact");
    }
}
