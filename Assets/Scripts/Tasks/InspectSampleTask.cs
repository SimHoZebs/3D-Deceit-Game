using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectSampleTask : TaskBase
{
    [Header("Cusomization")]
    [SerializeField] private float taskDuration = 60f;
    [SerializeField] public Color sampleDefaultColor;
    [SerializeField] private Color correctSampleColor;

    [Header("Samples")]
    private SampleBehaviour sample;
    public List<GameObject> sampleList = new List<GameObject>();
    public GameObject chosenSample;

    protected override void Start() {
        //This Start() runs later than InspectSampleTaskSample... so far
        base.Start();
        sample = gameObject.GetComponentInChildren<SampleBehaviour>();
    }

    protected override void TaskStartRsvpInternal()
    {
        StartCoroutine(TaskStart());
        StopCoroutine(TaskStart());
        base.TaskStartRsvpInternal();
    }

    private IEnumerator TaskStart(){

        yield return new WaitForSeconds(taskDuration);

        var randomIndex = Random.Range(0, sampleList.Count);
        sampleList[randomIndex].GetComponent<SampleBehaviour>().ChangeColor(correctSampleColor);

        yield return new WaitUntil(() => chosenSample != null);

        var chosenSampleColor = chosenSample.GetComponent<MeshRenderer>().material.color;

        if (chosenSampleColor == correctSampleColor){
            Debug.Log("Chose right sample");
            TaskFinish(thisTaskObj);
        }
        else{
            TaskStopRsvp(thisTaskObj);
        }
    }

    protected override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();

        chosenSample.GetComponent<MeshRenderer>().material.color = sampleDefaultColor;
        chosenSample = null;
    }
}
