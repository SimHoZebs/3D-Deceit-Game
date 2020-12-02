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
    private InspectSampleTaskSample sample;
    public List<GameObject> sampleList = new List<GameObject>();
    public GameObject chosenSample;

    public override void Start() {
        //This Start() runs later than InspectSampleTaskSample... so far
        base.Start();
        sample = gameObject.GetComponentInChildren<InspectSampleTaskSample>();
    }

    public override void TaskStartRsvp(GameObject taskObj, GameObject playerHandlerObj)
    {
        base.TaskStartRsvp(taskObj, playerHandlerObj);
        if (taskObj == thisTaskObj){

            StartCoroutine(TaskStart());
            StopCoroutine(TaskStart());
        }
    }

    public IEnumerator TaskStart(){

        yield return new WaitForSeconds(taskDuration);

        var randomIndex = Random.Range(0, sampleList.Count);
        sampleList[randomIndex].GetComponent<InspectSampleTaskSample>().ChangeColor(correctSampleColor);

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

    public override void TaskStopRsvp(GameObject taskObj)
    {
        base.TaskStopRsvp(taskObj);

        if (taskObj == thisTaskObj){

            ClearTaskingPlayerInfo();
        }
    }

    public override void TaskFinish(GameObject taskObj)
    {
        base.TaskFinish(taskObj);
        if (taskObj == thisTaskObj){

            ClearTaskingPlayerInfo();
        }
    }

    public override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();

        chosenSample.GetComponent<MeshRenderer>().material.color = sampleDefaultColor;
        chosenSample = null;

    }
}
