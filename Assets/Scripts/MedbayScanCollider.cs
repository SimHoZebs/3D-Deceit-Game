using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedbayScanCollider: MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        MedbaySanner._this.OnMedBayEnter();
    }

    private void OnTriggerStay(Collider other) {
        MedbaySanner._this.OnMedBayStay();
    }

}
