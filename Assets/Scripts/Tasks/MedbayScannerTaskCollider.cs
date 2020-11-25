using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedbayScannerTaskCollider: MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        MedbaySannerTask._this.OnMedBayEnter();
    }

    private void OnTriggerStay(Collider other) {
        MedbaySannerTask._this.OnMedBayStay();
    }

}
