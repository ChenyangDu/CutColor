using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverBigger : MonoBehaviour {
    

    private void OnMouseEnter()
    {
        transform.localScale *= 1.2f;
    }
    private void OnMouseExit()
    {
        transform.localScale /= 1.2f;
    }
}
