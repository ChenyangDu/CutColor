using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour {

    static GameObject startPoint = null;
    public static void setStartPoint(GameObject gameObject)
    {
        startPoint = gameObject;
    }
    public static GameObject getStartPoint()
    {
        return startPoint;
    }
}
