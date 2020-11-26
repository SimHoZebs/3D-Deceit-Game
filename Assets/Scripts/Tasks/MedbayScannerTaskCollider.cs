using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedbayScannerTaskCollider: MonoBehaviour
{
    //caching
    private MedbaySannerTask medbaySannerTask;

    private void Start() {
        medbaySannerTask = GetComponentInParent<MedbaySannerTask>();
    }

    private void OnTriggerEnter(Collider other) {
        medbaySannerTask.isOnStand = true;
    }

    private void OnTriggerExit(Collider other) {
        medbaySannerTask.isOnStand = false;
    }
}
