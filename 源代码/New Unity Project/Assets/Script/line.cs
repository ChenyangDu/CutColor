using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour {
    public void set(Vector3 s,Vector3 t)
    {
        transform.position = s;
        t = t - s;
        transform.localScale = new Vector3(t.magnitude, 1, 1);
        t = Vector3.Normalize(t);
        float theta = Mathf.Acos(t.x);
        if(t.y < 0)
        {
            theta = -theta;
        }
        transform.Rotate(0, 0, theta*180/Mathf.PI);
    }
}
