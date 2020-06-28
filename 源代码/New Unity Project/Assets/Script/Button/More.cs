using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class More : MonoBehaviour {

    private void OnMouseDown()
    {
        string OpenURL = "http://www.baidu.com";
        Application.OpenURL(OpenURL);
    }
}
