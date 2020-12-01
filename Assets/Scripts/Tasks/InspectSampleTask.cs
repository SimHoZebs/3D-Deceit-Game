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

    public override void TaskStartRsvp(GameObject task, GameObject player)
    {
        base.TaskStartRsvp(task, player);

        if (task == thisTaskObj){
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

    public override void TaskStopRsvp(GameObject task)
    {
        base.TaskStopRsvp(task);
        ClearTaskingPlayerInfo();
    }

    public override void TaskFinish(GameObject task)
    {
        base.TaskFinish(task);
        ClearTaskingPlayerInfo();

    }

    public override void ClearTaskingPlayerInfo()
    {
        base.ClearTaskingPlayerInfo();

        chosenSample.GetComponent<MeshRenderer>().material.color = sampleDefaultColor;
        chosenSample = null;

    }
}
