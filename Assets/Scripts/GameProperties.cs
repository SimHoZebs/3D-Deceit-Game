using System.Collections.Generic;
using UnityEngine;

public class GameProperties : MonoBehaviour
{
    public static List<GameObject> allTasks = new List<GameObject>();
    public static int taskNotStarted = 0;
    public static int taskOnGoing = 1;
    public static int taskFinished = 2;

}
