using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public GameObject gameObject;
    private void OnMouseDown()
    {
        gameObject.GetComponent<line>().set(new Vector3(-1.8f, 0.6f, 0), new Vector3(1.56f, 1.52f, 0));
    }
}
