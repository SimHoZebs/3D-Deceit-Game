using System.Collections.Generic;
using UnityEngine;

public class PlayerTaskManager : MonoBehaviour {
    
    [SerializeField] private List<string> taskList = new List<string>();

    [SerializeField] public bool downloadDone = false;

    public static PlayerTaskManager _this;

    private void Start() {
        _this = this;
    }
}